using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.Find("pnl_score").GetComponent<ScoreScript>().ShowScore();
        GameObject.Find("pnl_score").transform.FindChild("btn_resume").GetComponent<Button>().interactable = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameObject.Find("pnl_score").GetComponent<CanvasGroup>().alpha = 0f;
        GameObject.Find("pnl_score").transform.FindChild("btn_resume").GetComponent<Button>().interactable = false;
    }
}
