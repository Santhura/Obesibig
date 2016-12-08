using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour
{
    //Singleton pattern
    private static InventoryController instance;
    public static InventoryController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new InventoryController();
            }
            return instance;
        }
    }

    //Private variables
    private bool inventoryOpened;

    private static List<ShopItem> characters = new List<ShopItem>();
    private int characterIndex = 0;
    private ShopItem selectedCharacter;

    private static List<ShopItem> upgrades = new List<ShopItem>();
    private int upgradeIndex = 0;
    private ShopItem selectedUpgrade;

    //Public variables
    public GameObject inventoryCanvas;

    public Image characterImage, upgradeImage;
    public Text characterName, characterDescription,
                upgradeName, upgradeDescription;

    //Initialization
    void Start()
    {
        //Check if the inventory is open or not
        if (inventoryCanvas.activeSelf)
        {
            inventoryOpened = true;
        }
        else
        {
            inventoryOpened = false;
        }
    }

    void OpenInventory()
    {
        FillInventory();
        SetPlayerPrefs(selectedCharacter, selectedUpgrade);

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

    public void Add(ShopItem shopItem)
    {
        if (shopItem.isCharacter)
        {
            characters.Add(shopItem);
        }
        else
        {
            upgrades.Add(shopItem);
        }
    }

    void FillInventory()
    {
        //Set selected character 
        GetSelectedItem("Character_Item");
        DisplayItem("character", characters.IndexOf(selectedCharacter));
        SetPlayerPrefs(selectedCharacter, null);

        //Set selected upgrade (if the player has one)
        if (upgrades.Count > 0)
        {
            GetSelectedItem("Upgrade_Item");
            DisplayItem("upgrade", upgrades.IndexOf(selectedUpgrade));
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }

    void GetSelectedItem(string keyName)
    {
        if (keyName == "Character_Item")
        {
            //Always have a default, in this case the raptor character
            selectedCharacter = characters[0];

            //Order list alphabetically
            characters = characters.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Character_Item"))
            {
                selectedCharacter = characters.Where(character => character.prefabName == PlayerPrefs.GetString("Character_Item")).SingleOrDefault();
            }

            //Set (selected) index for the CHARACTER list
            characterIndex = characters.IndexOf(selectedCharacter);
        }

        if (keyName == "Upgrade_Item")
        {
            //Always have a default
            selectedUpgrade = upgrades[0];

            //Order list alphabetically
            upgrades = upgrades.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Upgrade_Item"))
            {
                if (PlayerPrefs.GetString("Upgrade_Item") != "null")
                {
                    selectedUpgrade = upgrades.Where(upgrade => upgrade.prefabName == PlayerPrefs.GetString("Upgrade_Item")).SingleOrDefault();
                }
            }

            //Set (selected) index for the UPGRADES list
            upgradeIndex = upgrades.IndexOf(selectedUpgrade);
        }
    }

    public void SetPlayerPrefs(ShopItem selCharacter, ShopItem selUpgrade)
    {
        if (selCharacter != null)
        {
            PlayerPrefs.SetString("Character_Item", selCharacter.prefabName);
        }

        if (selUpgrade != null)
        {
            PlayerPrefs.SetString("Upgrade_Item", selUpgrade.prefabName);
        }
        else
        {
            PlayerPrefs.SetString("Upgrade_Item", "null");
        }
    }

    void DisplayItem(string itemType, int index)
    {
        if (itemType == "character")
        {
            characterName.text = characters[index].itemName;
            characterImage.sprite = characters[index].itemSprite;
            characterDescription.text = characters[index].itemDesc;
        }

        if (itemType == "upgrade")
        {
            upgradeName.text = upgrades[index].itemName;
            upgradeImage.sprite = upgrades[index].itemSprite;
            upgradeDescription.text = upgrades[index].itemDesc;
        }
    }

    void NextItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "character")
        {
            if ((characterIndex + 1) < characters.Count)
            {
                characterIndex++;
                DisplayItem(itemType, characterIndex);
            }
            else
            {
                characterIndex = 0;
                DisplayItem(itemType, characterIndex);
            }

            selectedCharacter = characters[characterIndex];
            SetPlayerPrefs(selectedCharacter, null);
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "upgrade" && upgrades.Count > 0)
        {
            if ((upgradeIndex + 1) < upgrades.Count)
            {
                upgradeIndex++;
                DisplayItem(itemType, upgradeIndex);
            }
            else
            {
                upgradeIndex = 0;
                DisplayItem(itemType, upgradeIndex);
            }

            selectedUpgrade = upgrades[upgradeIndex];
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }

    void PreviousItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "character")
        {
            if (characters.Count > 0)
            {
                if ((characterIndex - 1) >= 0)
                {
                    characterIndex--;
                    DisplayItem(itemType, characterIndex);
                }
                else
                {
                    characterIndex = (characters.Count - 1);
                    DisplayItem(itemType, characterIndex);
                }

                selectedCharacter = characters[characterIndex];
                SetPlayerPrefs(selectedCharacter, null);
            }
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "upgrade" && upgrades.Count > 0)
        {
            if ((upgradeIndex - 1) >= 0)
            {
                upgradeIndex--;
                DisplayItem(itemType, upgradeIndex);
            }
            else
            {
                upgradeIndex = (upgrades.Count - 1);
                DisplayItem(itemType, upgradeIndex);
            }

            selectedUpgrade = upgrades[upgradeIndex];
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }
}
