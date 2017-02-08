﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager gameManager;                   //singleton

    private string currentSceneName;                        // current scene that is playing 
    private string nextSceneName;                           // next scene that will load
    private AsyncOperation resourceUnloadTask;              // unload all resources
    private AsyncOperation sceneLoadTask;                   // load scene resources
    private enum SceneState                                 // All the states that a scene can be in
    {
        Reset, Preload, Load, Unload,
        Postload, Ready, Run, Count
    };  // all scene states
    private SceneState sceneState;                          // The state that is active
    private delegate void UpdateDelegate();                 // contain the update state functions
    private UpdateDelegate[] updateDelegates;               // has all the state functions

    public static string currentLevelName;                  // what is the current level that is playing

    public string CurrentSceneName {                        // a getter for the scene name
        get { return currentSceneName; }
    }

    private GameObject progressBar;

    #region public static methods
    /// <summary>
    /// Changes scenes
    /// </summary>
    /// <param name="nextSceneName"></param>
    public static void SwitchScene(string nextSceneName, string levelName) {
        Time.timeScale = 1.0f;
        if (gameManager != null) {
            if (gameManager.currentSceneName != nextSceneName) {
                gameManager.nextSceneName = nextSceneName;
                if (levelName != null) {
                    currentLevelName = levelName;
                }
            }
            // this is for when a level is played and pressed the next level button
            else {
                gameManager.nextSceneName = nextSceneName;
                if (levelName != null) {
                    currentLevelName = levelName;
                }
            }
        }
    }
    #endregion

    protected void Awake() {
        //Keep this object alive between scene changes.
        DontDestroyOnLoad(gameObject);

        //setup the signleton instance
        gameManager = this;

        // setup the array of updateDelegates
        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        // set each updateDelegate
        updateDelegates[(int)SceneState.Reset] = ResetState;
        updateDelegates[(int)SceneState.Preload] = PreloadState;
        updateDelegates[(int)SceneState.Load] = LoadState;
        updateDelegates[(int)SceneState.Unload] = UnloadState;
        updateDelegates[(int)SceneState.Postload] = PostloadState;
        updateDelegates[(int)SceneState.Ready] = ReadyState;
        updateDelegates[(int)SceneState.Run] = RunState;

        progressBar = GameObject.Find("Progress bar sides");
        progressBar.SetActive(false);
        nextSceneName = "SplashScreen";
        sceneState = SceneState.Reset;
    }

    protected void OnDestroy() {
        //Clean up all the updateDelegates
        if (updateDelegates != null) {
            for (int i = 0; i < (int)SceneState.Count; i++) {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        //Clean up the singleton instance
        if (gameManager != null) {
            gameManager = null;
        }
    }

    // Use this for initialization
    void Start() {
        if (!PlayerPrefs.HasKey("Character_Item")) {
            PlayerPrefs.SetString("Character_Item", "Raptor");
        }
    }

    // Update is called once per frame
    protected void Update() {
        if (updateDelegates[(int)sceneState] != null) {
            updateDelegates[(int)sceneState]();
        }
    }


    #region Private methods

    // attach the new scene controller to start cascade of loading
    private void ResetState() {

        // run a garbace collecter pass
        progressBar.SetActive(true);
        progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        System.GC.Collect();
        sceneState = SceneState.Preload;
    }

    // handle anything that needs to happen before loading
    private void PreloadState() {
        try {
            sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
        }
        catch (Exception e) {
              Debug.LogException(e, this);
        }
        sceneLoadTask.allowSceneActivation = false;
        sceneState = SceneState.Load;
    }

    //show the loading screen until it's loaded
    private void LoadState() {
        StartCoroutine(SceneLoadProgress());
    }

    /// <summary>
    /// Loading bar
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneLoadProgress() {
        yield return null;
        // done loading ?
            if (!sceneLoadTask.isDone) {
            // keep the loading progress going
                if (progressBar != null) {
                    float progress = Mathf.Clamp01(sceneLoadTask.progress / 0.9f);
                  //  Time.timeScale = .1f;
                    progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = progress;
                }
                if (sceneLoadTask.progress == 0.9f && progressBar.GetComponentInChildren<Image>().fillAmount == 1) {
                    yield return new WaitForSeconds(.1f);
                    sceneLoadTask.allowSceneActivation = true;
                 //   Time.timeScale = 1;
                    sceneState = SceneState.Unload;

                }
            }
        yield return null;
    }

    private void UnloadState() {
        progressBar.SetActive(false);
        try {
            // cleaning up resoucres yet?
            if (resourceUnloadTask == null) {

                resourceUnloadTask = Resources.UnloadUnusedAssets();
            }

            else {
                //done cleaning up, go to next state
                if (resourceUnloadTask.isDone) {
                    resourceUnloadTask = null;
                    sceneState = SceneState.Postload;
                }
            }
        }
        catch (Exception e) {
            Debug.LogException(e, this);
        }
    }

    //handle anything that needs to happen immediately after loading
    private void PostloadState() {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }
    // handle anything that needs to happen immediately before running
    private void ReadyState() {
        System.GC.Collect();
        sceneState = SceneState.Run;
    }

    //wait for scene change
    private void RunState() {
        if (currentSceneName != nextSceneName) {
            sceneState = SceneState.Reset;
        }
        else if (currentSceneName == "TutorialScene" && WinOrLoseScript.pressedNextLevelButtons) {
            sceneState = SceneState.Reset;
            WinOrLoseScript.pressedNextLevelButtons = false;
        }
    }
    #endregion


    public void ReturnToMainMenu() {
        SwitchScene("Main Menu", null);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitButton() {
        Application.Quit();
    }
}