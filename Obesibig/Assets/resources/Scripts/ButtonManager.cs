using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public Dropdown addType0Dropdown, addType1Dropdown, addType2Dropdown;
    public Dropdown changeColorDropdown;
    //public Button btn_addType, btn_addColor;

    //Quit the game
    public void Quit()
    {
        Application.Quit();
    }

    public void GoLab()
    {
        Application.LoadLevel(1);
    }

    public void GoMenu()
    {
        Application.LoadLevel(0);
    }
}
