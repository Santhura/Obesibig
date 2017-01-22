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

    private static List<ShopItem> characterItems = new List<ShopItem>();
    private int characterIndex = 0;
    private ShopItem selectedCharacter;

    private static List<ShopItem> upgradeItems = new List<ShopItem>();
    private int upgradeIndex = 0;
    private ShopItem selectedUpgrade;

    //Public variables
    public GameObject inventoryCanvas;

    public Image characterImage, upgradeImage;
    public Text characterTitle, characterDescription,
                upgradeTitle, upgradeDescription;

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
        //SetPlayerPrefs(selectedCharacter, selectedUpgrade);

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
            characterItems.Add(shopItem);
        }
        else
        {
            upgradeItems.Add(shopItem);
        }
    }

    void FillInventory()
    {
        //Set selected character 
        GetSelectedItem("Character_Item");
        DisplayItem("character", characterItems.IndexOf(selectedCharacter));
        SetPlayerPrefs(selectedCharacter, null);

        //Set selected upgrade (if the player has one)
        if (upgradeItems.Count > 0)
        {
            GetSelectedItem("Upgrade_Item");
            DisplayItem("upgrade", upgradeItems.IndexOf(selectedUpgrade));
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }

    void GetSelectedItem(string keyName)
    {
        if (keyName == "Character_Item")
        {
            //Always have a default, in this case the raptor character
            selectedCharacter = characterItems[0];

            //Order list alphabetically (--> Order on cost)
            characterItems = characterItems.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Character_Item"))
            {
                selectedCharacter = characterItems.Where(character => character.prefabName == PlayerPrefs.GetString("Character_Item")).SingleOrDefault();
            }

            //Set (selected) index for the CHARACTERS list
            characterIndex = characterItems.IndexOf(selectedCharacter);
        }

        if (keyName == "Upgrade_Item")
        {
            //Always have a default
            selectedUpgrade = upgradeItems[0];

            //Order list alphabetically
            upgradeItems = upgradeItems.OrderBy(go => go.itemName).ToList();

            if (PlayerPrefs.HasKey("Upgrade_Item"))
            {
                if (PlayerPrefs.GetString("Upgrade_Item") != "null")
                {
                    selectedUpgrade = upgradeItems.Where(upgrade => upgrade.prefabName == PlayerPrefs.GetString("Upgrade_Item")).SingleOrDefault();
                }
            }

            //Set (selected) index for the UPGRADES list
            upgradeIndex = upgradeItems.IndexOf(selectedUpgrade);
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
            characterTitle.text = characterItems[index].itemName;
            characterImage.sprite = characterItems[index].itemSprite;
            characterDescription.text = characterItems[index].itemDesc;
        }

        if (itemType == "upgrade")
        {
            upgradeTitle.text = upgradeItems[index].itemName;
            upgradeImage.sprite = upgradeItems[index].itemSprite;
            upgradeDescription.text = upgradeItems[index].itemDesc;
        }
    }

    void NextItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "character")
        {
            if ((characterIndex + 1) < characterItems.Count)
            {
                characterIndex++;
                DisplayItem(itemType, characterIndex);
            }
            else
            {
                characterIndex = 0;
                DisplayItem(itemType, characterIndex);
            }

            selectedCharacter = characterItems[characterIndex];
            SetPlayerPrefs(selectedCharacter, null);
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 0 again after passing 'Count'
        if (itemType == "upgrade" && upgradeItems.Count > 0)
        {
            if ((upgradeIndex + 1) < upgradeItems.Count)
            {
                upgradeIndex++;
                DisplayItem(itemType, upgradeIndex);
            }
            else
            {
                upgradeIndex = 0;
                DisplayItem(itemType, upgradeIndex);
            }

            selectedUpgrade = upgradeItems[upgradeIndex];
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }

    void PreviousItem(string itemType)
    {
        //CHARACTER: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "character")
        {
            if (characterItems.Count > 0)
            {
                if ((characterIndex - 1) >= 0)
                {
                    characterIndex--;
                    DisplayItem(itemType, characterIndex);
                }
                else
                {
                    characterIndex = (characterItems.Count - 1);
                    DisplayItem(itemType, characterIndex);
                }

                selectedCharacter = characterItems[characterIndex];
                SetPlayerPrefs(selectedCharacter, null);
            }
        }

        //UPGRADE: The list is a cycle list, which means the index starts at 'Count-1' again after passing 0
        if (itemType == "upgrade" && upgradeItems.Count > 0)
        {
            if ((upgradeIndex - 1) >= 0)
            {
                upgradeIndex--;
                DisplayItem(itemType, upgradeIndex);
            }
            else
            {
                upgradeIndex = (upgradeItems.Count - 1);
                DisplayItem(itemType, upgradeIndex);
            }

            selectedUpgrade = upgradeItems[upgradeIndex];
            SetPlayerPrefs(null, selectedUpgrade);
        }
    }
}
