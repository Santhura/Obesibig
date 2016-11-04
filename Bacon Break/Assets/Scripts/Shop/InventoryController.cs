using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    //For opening and closing the shop
    public GameObject inventoryCanvas;
    private bool inventoryOpened;
    public ShopController shopController;
    public ShopItem defaultCharacter;

    //Keeping track of characters / upgrades
    private List<ShopItem> characters;
    private List<ShopItem> upgrades;
    private int charIndex = 0;
    private int upgrIndex = 0;

    //Setting character and upgrade information
    public Image characterImage, upgradeImage;
    public Text characterTitle, upgradeTitle;
    private ShopItem charItem, upgrItem;

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();

        charItem = new ShopItem();
        upgrItem = new ShopItem();

        FillLists();
        SetPreferences(charItem, upgrItem);

        if (inventoryCanvas.activeSelf)
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
        //Open/close inventory
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
        inventoryCanvas.SetActive(true);
        Time.timeScale = 0;
        inventoryOpened = true;
    }

    void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        Time.timeScale = 1;
        inventoryOpened = false;
    }

    public void FillLists()
    {
        characters = new List<ShopItem>();
        upgrades = new List<ShopItem>();

        for (int i = 0; i < shopController.shopItems.Count; i++)
        {
            //The default character is always at index 0
            if (i == 0)
            {
                characters.Add(defaultCharacter);
                charItem = defaultCharacter;
                FillItemInformation("character", 0);
            }

            //Add unlocked characters to list
            if (shopController.shopItems[i].isUnlocked
                && shopController.shopItems[i].isCharacter)
            {
                characters.Add(shopController.shopItems[i]);
            }

            //Add unlocked upgrades to list
            if (shopController.shopItems[i].isUnlocked
               && !shopController.shopItems[i].isCharacter)
            {
                upgrades.Add(shopController.shopItems[i]);
                upgrItem = shopController.shopItems[i];
            }
        }

        //Order items alphabetically
        characters = characters.OrderBy(go => go.itemName).ToList();

        if (PlayerPrefs.HasKey("Character_Item") && PlayerPrefs.HasKey("Upgrade_Item"))
        {
            //Because the easy way out (Linq) didn't work---------------------------------
            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].prefabName == PlayerPrefs.GetString("Character_Item"))
                {
                    charItem = characters[i];
                }
            }

            if (upgrades.Count > 0)
            {
                for (int i = 0; i < upgrades.Count; i++)
                {
                    if (upgrades[i].prefabName == PlayerPrefs.GetString("Upgrade_Item"))
                    {
                        upgrItem = upgrades[i];
                    }
                }
            }
            //Because the easy way out (Linq) didn't work---------------------------------
        }

        //Set selected items as first items to be shown.
        FillItemInformation("character", characters.IndexOf(charItem));
        charIndex = characters.IndexOf(charItem);

        if (upgrades.Count > 0)
        {
            //Order items alphabetically
            upgrades = upgrades.OrderBy(go => go.itemName).ToList();
            FillItemInformation("upgrade", upgrades.IndexOf(upgrItem));
            upgrIndex = upgrades.IndexOf(upgrItem);
        }
    }

    public void NextItem(string itemType)
    {
        //For character list
        if (itemType == "character")
        {
            if (characters.Count > 0)
            {
                if ((charIndex + 1) < characters.Count)
                {
                    charIndex++;
                    FillItemInformation(itemType, charIndex);
                }
                else
                {
                    charIndex = 0;
                    FillItemInformation(itemType, charIndex);
                }

                charItem = characters[charIndex];
                SetPreferences(charItem, null);
            }
            DebugConsole.Log(charItem.itemName);
        }

        //For upgrade list
        if (itemType == "upgrade")
        {
            if (upgrades.Count > 0)
            {
                if ((upgrIndex + 1) < upgrades.Count)
                {
                    upgrIndex++;
                    FillItemInformation(itemType, upgrIndex);
                }
                else
                {
                    upgrIndex = 0;
                    FillItemInformation(itemType, upgrIndex);
                }

                upgrItem = upgrades[upgrIndex];
                SetPreferences(null, upgrItem);
            }
            else
            {
                upgrItem = null;
            }
        }
    }

    public void PreviousItem(string itemType)
    {
        //For character list
        if (itemType == "character")
        {
            if (characters.Count > 0)
            {
                if ((charIndex - 1) >= 0)
                {
                    charIndex--;
                    FillItemInformation(itemType, charIndex);
                }
                else
                {
                    charIndex = (characters.Count - 1);
                    FillItemInformation(itemType, charIndex);
                }

                charItem = characters[charIndex];
                SetPreferences(charItem, null);
            }
        }

        //For upgrade list
        if (itemType == "upgrade")
        {
            if (upgrades.Count > 0)
            {
                if ((upgrIndex - 1) >= 0)
                {
                    upgrIndex--;
                    FillItemInformation(itemType, charIndex);
                }
                else
                {
                    upgrIndex = (upgrades.Count - 1);
                    FillItemInformation(itemType, charIndex);
                }

                upgrItem = upgrades[upgrIndex];
                SetPreferences(null, upgrItem);
            }
            else
            {
                upgrItem = null;
            }
        }

        SetPreferences(charItem, upgrItem);
    }

    void FillItemInformation(string itemType, int index)
    {
        if (itemType == "character")
        {
            characterTitle.text = characters[index].itemName;
            characterImage.sprite = characters[index].itemSprite;
            //charIndex = index;
        }

        if (itemType == "upgrade")
        {
            upgradeTitle.text = upgrades[index].itemName;
            upgradeImage.sprite = upgrades[index].itemSprite;
            //upgrIndex = index;
        }
    }

    public void SetPreferences(ShopItem charItem, ShopItem upgrItem)
    {
        //DebugConsole.Log(charItem.itemName);

        if (charItem != null)
        {
            PlayerPrefs.SetString("Character_Item", charItem.prefabName);
        }

        //Upgrade item can be null
        if (upgrItem != null)
        {
            PlayerPrefs.SetString("Upgrade_Item", upgrItem.prefabName);
        }
    }

}
