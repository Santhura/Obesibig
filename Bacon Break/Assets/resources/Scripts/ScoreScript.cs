using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text txt_baconAmount;    //UI element displaying the amount of bacon collected.
    public Text txt_baconScore;     //UI element (of the score panel) displaying the amount of bacon collected.

    private int baconAmount;        //To keep track of the amount of collected bacon in code.

	// Use this for initialization
	void Start () 
    {
        baconAmount = 0;
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Pauses the game and displays a score panel.
        if (Input.GetKeyUp(KeyCode.P) &&
             gameObject.GetComponent<CanvasGroup>().alpha == 0f)
        {
            Time.timeScale = 0;
            ShowScore();
        }

        //Resumes the game and hides the score panel.
        else if (Input.GetKeyUp(KeyCode.P) &&
            gameObject.GetComponent<CanvasGroup>().alpha == 1f)
        {
            Time.timeScale = 1;
            HideScore();
        }
	}

    //Add bacon score. Show this new score on the GUI.
    public void AddBacon()
    {
        baconAmount++;

        txt_baconAmount.text = baconAmount.ToString();
        txt_baconScore.text = "x " + baconAmount.ToString();
    }

    //Display score panel.
    public void ShowScore()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1f;
    }

    //Hide score panel.
    void HideScore()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }
}
