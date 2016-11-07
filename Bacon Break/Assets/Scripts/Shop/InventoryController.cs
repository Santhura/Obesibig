using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    private List<ShopItem> characters;                      //Unlocked shop items of type CHARACTER are added to this list
    private List<ShopItem> upgrades;                        //Unlocked shop items of type UPGRADE are added to this list
    private ShopItem charItem, upgrItem;                    //Used for storing the selected character / upgrade in playerprefs
    private int charIndex = 0;                              //Index specifically used for the CHARACTER list
    private int upgrIndex = 0;                              //Index specifically used for the UPGRADE list

    [Header("CONTROLLER SETTINGS")]
    public GameObject inventoryCanvas;                      //For setting the canvas on active or inactive
    private bool inventoryOpened;                           //Checking if the inventory is opened or not
    public ShopController shopController;                   //Primarily used for retrieving the SHOP ITEM list
    public ShopItem defaultCharacter;                       //There is always one character - the default character

    [Header("INVENTORY CANVAS ITEMS")]
    public Image characterImage;                            //This will display the currently selected character / upgrade image
    public Image upgradeImage;
    public Text characterTitle, characterDescription,       //This will display the title and description of the selected character / upgrade
                upgradeTitle, upgradeDescription;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        characters = new List<ShopItem>();
        upgrades = new List<ShopItem>();

        if (inventoryCanvas.activeSelf)                     //Check if the inventory is open or not
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
        //Open / close inventory with 'I' (PC)
        /*if (Input.GetKeyUp(KeyCode.I) && !inventoryOpened)
        {
            OpenInventory();
        }
        else if (Input.GetKeyUp(KeyCode.I) && inventoryOpened)
        {
            CloseInventory();
        }*/
    }

    public void OpenInventory()
    {
        FillInventory();                                    //Fill the CHARACTERS and UPGRADES list (and display selected items)
        SetPreferences(charItem, upgrItem);                 //Add the currently selected character / upgrade to PlayerPrefs

        inventoryCanvas.SetActive(true);
        Time.timeScale = 0;
        inventoryOpened = true;
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        Time.timeScale = 1;
        inventoryOpened = false;
    }

    public void FillInventory()
    {
        //Make sure both lists are empty before filling
        characters.Clear();
        upgrades.Clear();

        //Add unlocked characters and upgrades to their respective lists
        for (int i = 0; i < shopController.shopItems.Count; i++)
        {
            //Make sure there always is a default character
            if (i == 0)
            {
                characters.Add(defaultCharacter);
            }

            if (shopController.shopItems[i].isUnlocked)
            {
                if (shopController.shopItems[i].isCharacter)
                {
                    characters.Add(shopController.shopItems[i]);
                }
                else
                {
                    upgrades.Add(shopController.shopItems[i]);
                }
            }
        }

        //Set selected character 
        GetSelectedItem("Character_Item");
        DisplayItem("character", characters.IndexOf(charItem));
        SetPreferences(charItem, null);

        //Set selected upgrade (if the player has one)
        if (upgrades.Count > 0)
        {
            GetSelectedItem("Upgrade_Item");
            DisplayItem("upgrade", upgrades.IndexOf(upgrItem));
            SetPreferences(null, upgrItem);
        }
    }

    void GetSelectedItem(string keyName)
    {
        if (keyName == "Character_Item")
        {
            //Always have a default
            charItem = defaultCharacter;

            //Order list alphabetically
            characters = characters.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Character_Item"))
            {
                charItem = characters.Where(character => character.prefabName == PlayerPrefs.GetString("Character_Item")).SingleOrDefault();
            }

            //Set (selected) index for the CHARACTER list
            charIndex = characters.IndexOf(charItem);
        }

        if (keyName == "Upgrade_Item")
        {
            //Always have a default
            upgrItem = upgrades[0];

            //Order list alphabetically
            upgrades = upgrades.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Upgrade_Item"))
            {
                if (PlayerPrefs.GetString("Upgrade_Item") != "null")
                {
                    upgrItem = upgrades.Where(upgrade => upgrade.prefabName == PlayerPrefs.GetString("Upgrade_Item")).SingleOrDefault();
                }
            }

            //Set (selected) index for the UPGRADES list
            upgrIndex = upgrades.IndexOf(upgrItem);
        }
    }

    public void NextItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "character")
        {
            if ((charIndex + 1) < characters.Count)
            {
                charIndex++;
                DisplayItem(itemType, charIndex);
            }
            else
            {
                charIndex = 0;
                DisplayItem(itemType, charIndex);
            }

            charItem = characters[charIndex];
            SetPreferences(charItem, null);
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "upgrade" && upgrades.Count > 0)
        {
            if ((upgrIndex + 1) < upgrades.Count)
            {
                upgrIndex++;
                DisplayItem(itemType, upgrIndex);
            }
            else
            {
                upgrIndex = 0;
                DisplayItem(itemType, upgrIndex);
            }

            upgrItem = upgrades[upgrIndex];
            SetPreferences(null, upgrItem);
        }
    }

    public void PreviousItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "character")
        {
            if (characters.Count > 0)
            {
                if ((charIndex - 1) >= 0)
                {
                    charIndex--;
                    DisplayItem(itemType, charIndex);
                }
                else
                {
                    charIndex = (characters.Count - 1);
                    DisplayItem(itemType, charIndex);
                }

                charItem = characters[charIndex];
                SetPreferences(charItem, null);
            }
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "upgrade" && upgrades.Count > 0)
        {
            if ((upgrIndex - 1) >= 0)
            {
                upgrIndex--;
                DisplayItem(itemType, charIndex);
            }
            else
            {
                upgrIndex = (upgrades.Count - 1);
                DisplayItem(itemType, charIndex);
            }

            upgrItem = upgrades[upgrIndex];
            SetPreferences(null, upgrItem);
        }
    }

    void DisplayItem(string itemType, int index)
    {
        if (itemType == "character")
        {
            characterTitle.text = characters[index].itemName;
            characterImage.sprite = characters[index].itemSprite;
            characterDescription.text = characters[index].itemDesc;
        }

        if (itemType == "upgrade")
        {
            upgradeTitle.text = upgrades[index].itemName;
            upgradeImage.sprite = upgrades[index].itemSprite;
            upgradeDescription.text = upgrades[index].itemDesc;
        }
    }

    public void SetPreferences(ShopItem charItem, ShopItem upgrItem)
    {
        if (charItem != null)
        {
            PlayerPrefs.SetString("Character_Item", charItem.prefabName);
        }

        if (upgrItem != null)
        {
            PlayerPrefs.SetString("Upgrade_Item", upgrItem.prefabName);
        }
        else
        {
            PlayerPrefs.SetString("Upgrade_Item", "null");
        }
    }
}
