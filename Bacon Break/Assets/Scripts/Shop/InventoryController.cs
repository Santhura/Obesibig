using UnityEngine;
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

    // Use this for initialization
    void Start()
    {
        characters = new List<ShopItem>();
        upgrades = new List<ShopItem>();

        FillLists();

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

    public void Add(ShopItem item, bool isCharacter)
    {
        if (isCharacter)
        {
            characters.Add(item);
        }
        else
        {
            upgrades.Add(item);
            FillLists();
        }
    }

    void FillLists()
    {
        for (int i = 0; i < shopController.shopItems.Count; i++)
        {
            //The default character is always at index 0
            if (i == 0)
            {
                characters.Add(defaultCharacter);
                FillItemInformation("character", i);
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
            }
        }

        FillItemInformation("upgrade", 0);
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
            }
        }

        //For upgrade list
        if (itemType == "upgrade")
        {
            if (upgrades.Count > 0)
            {
                if ((upgrIndex + 1) < upgrades.Count)
                {
                    upgrIndex++;
                    FillItemInformation(itemType, charIndex);
                }
                else
                {
                    upgrIndex = 0;
                    FillItemInformation(itemType, charIndex);
                }
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
            }
        }
    }

    void FillItemInformation(string itemType, int index)
    {
        if (itemType == "character")
        {
            characterTitle.text = characters[index].itemName;
            characterImage.sprite = characters[index].itemSprite;
        }

        if (itemType == "upgrade")
        {
            upgradeTitle.text = upgrades[index].itemName;
            upgradeImage.sprite = characters[index].itemSprite;
        }
    }
}
