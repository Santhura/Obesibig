using UnityEngine;
using UnityEngine.UI;

public class OptionScript : MonoBehaviour
{
    //Public variables
    public Dropdown myDropdown;

    //Private (static) variables
    private static int c_index;
    private static Vector3 c_position, c_rotation;
    private static bool isOrthographic;

    void Start()
    {
        //Default camera setting.
        c_position = new Vector3(35.0f, 40.0f, -18.0f);
        c_rotation = new Vector3(35.0f, -35.0f, 6.0f);
        isOrthographic = true;

        //Create listener.
        myDropdown.onValueChanged.AddListener(delegate
        {
            myDropdownValueChangedHandler(myDropdown);
        });
    }

    //Destroy listeners.
    void Destroy()
    {
        myDropdown.onValueChanged.RemoveAllListeners();
    }

    //Handler for when a dropdown items is pressed.
    private void myDropdownValueChangedHandler(Dropdown target)
    {
        SetCameraSetting(target.value);
    }

    //Changes dropdown index.
    public void SetDropdownIndex(int index)
    {
        myDropdown.value = index;
    }

    //Setter: Changes camera settings based on the pressed dropdown item.
    public void SetCameraSetting(int index)
    {
        c_index = index;

        switch (index)
        {
            /* Orthographic camera:
             * - The view we normally have, sort of isometric/2.5D view.
             */
            case 0:
                c_position = new Vector3(35.0f, 40.0f, -18.0f);
                c_rotation = new Vector3(43.4f, -27.28f, 4.6f);
                isOrthographic = true;
                break;
            /* Orthographic camera:
             */
            case 1:
                c_position = new Vector3(35.0f, 40.0f, -18.0f);
                c_rotation = new Vector3(43.4f, -169.29f, 4.6f);
                isOrthographic = true;
                break;
            /* Perspective camera:
             * - Seen from the perspective of the pig (sort of);
             * - Gives a better view of the level in general
             */
            case 2:
                c_position = new Vector3(1.95f, 17.0f, -22.5f);
                c_rotation = new Vector3(32.74f, 0, 0);
                isOrthographic = false;
                break;
            default:
                break;
        }
    }

    //Camera getters.
    public static Vector3 GetCameraPosition()
    {
        return c_position;
    }

    public static Vector3 GetCameraRotation()
    {
        return c_rotation;
    }

    public static bool IsCameraOrthographic()
    {
        return isOrthographic;
    }
}
