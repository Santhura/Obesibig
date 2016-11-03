using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectionButtons : MonoBehaviour
{
    private Button thisButton;
    public InventoryController inventoryController;


    // Use this for initialization
    void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() => { SelectItem(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SelectItem()
    {
    }
}
