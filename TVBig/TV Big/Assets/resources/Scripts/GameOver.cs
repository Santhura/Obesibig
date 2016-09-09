using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
