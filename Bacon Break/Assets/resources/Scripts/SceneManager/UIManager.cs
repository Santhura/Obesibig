using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    private static UIManager uiManager;

    private string currentSceneName;                        // current scene that is playing 
    private string nextSceneName;                           // next scene that will load
    private AsyncOperation resourceUnloadTask;              // unload all resources
    private AsyncOperation sceneLoadTask;                   // load scene resources
    private enum SceneState { Reset, Preload, Load, Unload,
                            Postload, Ready, Run, Count };  // all scene states
    private SceneState sceneState;                          // The state dat is active
    private delegate void UpdateDelegate();
    private UpdateDelegate[] updateDelegates;

    private static GameObject level;
    public static string currentLevelName;                 


    #region public static methods
    /// <summary>
    /// Changes scenes
    /// </summary>
    /// <param name="nextSceneName"></param>
    public static void SwitchScene(string nextSceneName, string levelName) {
        if(uiManager != null) {
            if (uiManager.currentSceneName != nextSceneName) {
                uiManager.nextSceneName = nextSceneName;
                if (levelName != null) {
                    currentLevelName = levelName;
                }
            }
        }
    }
    #endregion

    protected void Awake() {
        //Keep this object alive between scene changes.
        Object.DontDestroyOnLoad(gameObject);

        //setup the signleton instance
        uiManager = this;

        // setup the array of updateDelegates
        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        // set each updateDelegate
        updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        nextSceneName = "Main Menu";
        sceneState = SceneState.Reset;
    }

    protected void OnDestroy() {
        //Clean up all the updateDelegates
        if(updateDelegates != null) {
            for (int i = 0; i < (int)SceneState.Count; i++) {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        //Clean up the singleton instance
        if(uiManager != null) {
            uiManager = null;
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
	    if(updateDelegates[(int)sceneState] != null) {
            updateDelegates[(int)sceneState]();
        }
	}


    #region Private methods

    // attach the new scene controller to start cascade of loading
    private void UpdateSceneReset() {
        // run a garbace collecter pass
        System.GC.Collect();
        sceneState = SceneState.Preload;
    }

    // handle anything that needs to happen before loading
    private void UpdateScenePreload() {
        sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
        sceneState = SceneState.Load;
    }

    //show the loading screen until it's loaded
    private void UpdateSceneLoad() {
        // done loading ?
        if (sceneLoadTask.isDone) {
            sceneState = SceneState.Unload;
        } else {
            // update scene loading progress
        }
    }

    private void UpdateSceneUnload() {
        // cleaning up resoucres yet?
        if(resourceUnloadTask == null) {
            resourceUnloadTask = Resources.UnloadUnusedAssets();
        }else {
            //done cleaning up?
            if (resourceUnloadTask.isDone) {
                resourceUnloadTask = null;
                sceneState = SceneState.Postload;
            }
        }
    }

    //handle anything that needs to happen immediately after loading
    private void UpdateScenePostload() {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }
    // handle anything that needs to happen immediately before running
    private void UpdateSceneReady() {
        System.GC.Collect();
        sceneState = SceneState.Run;
    }

    //wait for scene change
    private void UpdateSceneRun() {
        if(currentSceneName != nextSceneName) {
            sceneState = SceneState.Reset;
        }
    }
    #endregion



    /// <summary>
    /// For now play the level we have created.
    /// TODO:: go to select screen instead of play a level.
    /// </summary>
    public void PlayButton(string nexSceneName)
    {
        PlayerMovement.isAbleToMove = true;
        this.nextSceneName = nexSceneName;
      
        sceneState = SceneState.Reset;
    }

    public void ReturnToMainMenu()
    {
        SwitchScene("Main Menu", null);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }
}