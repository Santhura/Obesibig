using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{


    private static Button[] pauseButtons = new Button[2];

    void Awake()
    {
        pauseButtons[0] = GameObject.Find("btn_resume").GetComponent<Button>();
        pauseButtons[1] = GameObject.Find("Back To menu").GetComponent<Button>();
    }

    // Use this for initialization
    void Start()
    {
        InteractablePauseButtons(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.Find("pnl_score").GetComponent<ScoreScript>().ShowScore();
        InteractablePauseButtons(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameObject.Find("pnl_score").GetComponent<CanvasGroup>().alpha = 0f;
        InteractablePauseButtons(false);
    }

    public void ExitGame()
    {
        Time.timeScale = 1;
        InteractablePauseButtons(false);
        SceneManager.LoadScene(0);
    }

    private void InteractablePauseButtons(bool isInteractable)
    {
        for (int i = 0; i < pauseButtons.Length; i++)
        {
            pauseButtons[i].interactable = isInteractable;
        }
    }
}
