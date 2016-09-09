using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SupercellOptionManager : MonoBehaviour
{
    public Dropdown dd_antigenType0, dd_antigenType1, dd_antigenType2;
    public Dropdown dd_supercellColor;
    public Image img_type0, img_type1, img_type2;
    public Text txt_pointAmount;
    public Sprite img0, img1, img2, img3;
    public GameObject superCell;
    public Button btn_addType, btn_addColor;

    private int typeCounter;
    private int pointAmount = 100;
    private Dropdown[] dd_types;

    void Start()
    {
        //For activating/deactivating dropdown menus
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

        //Add listeners to the dropdown menus-----------------------
        dd_antigenType0.onValueChanged.AddListener(delegate
        {
            typeChanged(dd_antigenType0);
        });

        dd_antigenType1.onValueChanged.AddListener(delegate
        {
            typeChanged(dd_antigenType1);
        });

        dd_antigenType2.onValueChanged.AddListener(delegate
        {
            typeChanged(dd_antigenType2);
        });

        dd_supercellColor.onValueChanged.AddListener(delegate
        {
            colorChanged(dd_supercellColor);
        });
        //----------------------------------------------------------
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
            case 0:
                {
                    if (target.name == "TypeDropdown0")
                    {
                        img_type0.sprite = img1;
                    }
                    else if (target.name == "TypeDropdown1")
                    {
                        img_type1.sprite = img1;
                    }
                    else
                    {
                        img_type2.sprite = img1;
                    }
                    break;
                }
            case 1:
                {
                    if (target.name == "TypeDropdown0")
                    {
                        img_type0.sprite = img2;
                    }
                    else if (target.name == "TypeDropdown1")
                    {
                        img_type1.sprite = img2;
                    }
                    else
                    {
                        img_type2.sprite = img2;
                    }
                    break;
                }
            case 2:
                {
                    if (target.name == "TypeDropdown0")
                    {
                        img_type0.sprite = img3;
                    }
                    else if (target.name == "TypeDropdown1")
                    {
                        img_type1.sprite = img3;
                    }
                    else
                    {
                        img_type2.sprite = img3;
                    }
                    break;
                }
            default:
                break;
        }
    }

    private void colorChanged(Dropdown target)
    {
        switch (target.value)
        {
            case 0:
                superCell.GetComponent<Renderer>().material.color = Color.red;
                break;
            case 1:
                superCell.GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                superCell.GetComponent<Renderer>().material.color = Color.blue;
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
        superCell.GetComponent<Renderer>().material.color = Color.red;

        btn_addColor.interactable = false;
        dd_supercellColor.interactable = true;

        pointAmount -= 10;
        txt_pointAmount.text = "Antigen Points (AP): \n" + pointAmount.ToString();
        //}
    }

    public void addType()
    {
        //Only 3 types can be added
        if (typeCounter < 3)
        {
            if (typeCounter == 0) { img_type0.sprite = img1; }
            if (typeCounter == 1) { img_type1.sprite = img1; }
            if (typeCounter == 2) { img_type2.sprite = img1; }


            dd_types[typeCounter].interactable = true;
            typeCounter++;
            pointAmount -= 30;
            txt_pointAmount.text = "Antigen Points (AP): \n" + pointAmount.ToString();
        }

        if (typeCounter == 3)
        {
            btn_addType.interactable = false;
        }
    }
}
