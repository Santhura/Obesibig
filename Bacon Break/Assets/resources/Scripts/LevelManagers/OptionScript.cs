using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    //Public variables
    public Dropdown myDropdown;

    //Private variables
    //Camera settings
    private static int c_index;
    private static Vector3 c_position, c_rotation;

    void Start()
    {
        //Default values
        //c_position = new Vector3(9.440001f, 24.5f, -3.853333f);
        c_rotation = new Vector3(43.4f, -27.28f, 4.6f);

        myDropdown.onValueChanged.AddListener(delegate
        {
            myDropdownValueChangedHandler(myDropdown);
        });
    }

    void Destroy()
    {
        myDropdown.onValueChanged.RemoveAllListeners();
    }

    private void myDropdownValueChangedHandler(Dropdown target)
    {
        SetCameraSetting(target.value);
        DebugConsole.Log("selected: " + target.value);
    }

    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;
    }

    public void SetCameraSetting(int index)
    {
        c_index = index;

        switch (index)
        {
            case 0:
                //c_position = new Vector3(9.440001f, 24.5f, -3.853333f);
                c_rotation = new Vector3(43.4f, 0, 4.6f);
                break;
            case 1:
                //c_position = new Vector3(9.440001f, 24.5f, -3.853333f);
                c_rotation = new Vector3(43.4f, -169.29f, 4.6f);
                break;
            case 2:
                //c_position = new Vector3(9.440001f, 24.5f, -3.853333f);
                c_rotation = new Vector3(43.4f, -169.29f, 4.6f);
                break;
            default:
                break;
        }
    }

    public static Vector3 GetCameraPosition()
    {
        return c_position;
    }

    public static Vector3 GetCameraRotation()
    {
        return c_rotation;
    }
}
