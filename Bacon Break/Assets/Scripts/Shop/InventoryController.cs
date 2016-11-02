using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryContainer;

    //For opening and closing the shop
    private bool inventoryOpened;

    // Use this for initialization
    void Start()
    {
        if (inventoryContainer.activeSelf)
        {
            inventoryOpened = true;
        }
        else
        {
            inventoryOpened = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && !inventoryOpened)
        {
            OpenInventory();
        }
        else if (Input.GetKeyUp(KeyCode.I) && inventoryOpened)
        {
            CloseInventory();
        }
    }

    void OpenInventory()
    {
        inventoryContainer.SetActive(true);
        Time.timeScale = 0;
        inventoryOpened = true;
    }

    void CloseInventory()
    {
        inventoryContainer.SetActive(false);
        Time.timeScale = 1;
        inventoryOpened = false;
    }
}
