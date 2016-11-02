using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    //Public variables
    private Dropdown dd_camSetting;

    void Start()
    {
        dd_camSetting = this.GetComponent<Dropdown>();

        //Create listener.
        dd_camSetting.onValueChanged.AddListener(delegate
        {
            dd_camSettingValueChangedHandler(dd_camSetting);
        });

        SetDropdownIndex(PlayerPrefs.GetInt("CameraView"));
    }

    //Destroy listeners.
    void Destroy()
    {
        dd_camSetting.onValueChanged.RemoveAllListeners();
    }

    //Handler for when a dropdown items is pressed.
    private void dd_camSettingValueChangedHandler(Dropdown target)
    {
        SetCameraView(target.value);
        Debug.Log(target.value);
    }

    //Changes dropdown index.
    public void SetDropdownIndex(int index)
    {
        dd_camSetting.value = index;
    }

    //Setter: Changes camera settings based on the pressed dropdown item.
    public void SetCameraView(int index)
    {
        if (index == 0)
        {
            //Orthographic 1
            PlayerPrefs.SetInt("CameraView", 0);
        }
        else if (index == 1)
        {
            //Perspective 1
            PlayerPrefs.SetInt("CameraView", 1);
        }
        else if (index == 2)
        {
            //Perspective 2
            PlayerPrefs.SetInt("CameraView", 2);
        }
    }
}
