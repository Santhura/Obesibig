using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// For now play the level we have created.
    /// TODO:: go to select screen instead of play a level.
    /// </summary>
    public void PlayButton()
    {
        PlayerMovement.isAbleToMove = true;
        SceneManager.LoadScene(4);
    }


    /// <summary>
    /// Quit the game
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }
}
