using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SupercellOptionManager : MonoBehaviour
{
    public Dropdown dd_antigenType0, dd_antigenType1, dd_antigenType2;
    public Dropdown dd_supercellColor;
    public GameObject superCell;
    public Button btn_addType, btn_addColor;

    private int typeCounter;
    private Dropdown[] dd_types;

    void Start()
    {
        dd_types = new Dropdown[3];
        dd_types[0] = dd_antigenType0;
        dd_types[1] = dd_antigenType1;
        dd_types[2] = dd_antigenType2;

        //Can't interact unless bought
        dd_antigenType0.interactable = false;
        dd_antigenType1.interactable = false;
        dd_antigenType2.interactable = false;
        dd_supercellColor.interactable = false;

        //Buttons
        btn_addColor.onClick.AddListener(() => { addColor(); });
        btn_addType.onClick.AddListener(() => { addType(); });

        //Add listeners to the dropdown menus
        dd_antigenType0.onValueChanged.AddListener(delegate
        {
            typeChanged(dd_antigenType0);
        });

        dd_supercellColor.onValueChanged.AddListener(delegate
        {
            colorChanged(dd_supercellColor);
        });
    }

    //Remove all listeners if value is changed
    void Destroy()
    {
        dd_antigenType0.onValueChanged.RemoveAllListeners();
        dd_supercellColor.onValueChanged.RemoveAllListeners();
    }

    //Get selected dropdown option
    private void typeChanged(Dropdown target)
    {
        switch (target.value)
        {
            default:
                break;
        }
    }

    private void colorChanged(Dropdown target)
    {
        switch (target.value)
        {
            case 1:
                superCell.GetComponent<Renderer>().material.color = Color.red;
                Debug.Log("1");
                break;
            case 2:
                superCell.GetComponent<Renderer>().material.color = Color.green;
                Debug.Log("2");
                break;
            case 3:
                superCell.GetComponent<Renderer>().material.color = Color.blue;
                Debug.Log("3");
                break;
            default:
                superCell.GetComponent<Renderer>().material.color = Color.white;
                break;
        }
    }

    //Update array index
    public void SetDropdownIndex(int index)
    {
        dd_antigenType0.value = index;
        dd_supercellColor.value = index;
    }

    public void addColor()
    {
        //If(amount of points is high enough){
        btn_addColor.interactable = false;
        dd_supercellColor.interactable = true;
        //}
    }

    public void addType()
    {
        //Only 3 types can be added
        if (typeCounter < 3)
        {
            dd_types[typeCounter].interactable = true;

            typeCounter++;
        }
        
        if (typeCounter == 3)
        {
            btn_addType.interactable = false;
        }
    }
}
