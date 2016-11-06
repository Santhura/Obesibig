using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    //For opening and closing the shop
    [Header("CANVAS_SETTINGS")]
    public GameObject shopCanvas;
    private bool shopOpened;

    [Header("TRANSACTION_SETTINGS")]
    public List<ShopItem> shopItems;                    //For keeping track of all the (shop) items
    public List<ShopButton> shopButtons;                //For cycling between shop items
    public InventoryController inventoryController;     //Primarily used for making transactions between the shop and the inventory
    public Text coinAmount;                             //For keeping track of the amount of coins
    public Button charFilter, upgrFilter;               //For filtering, obviously

    public List<ShopItem> filteredItems;                 //Temporary list for storing filtered items in the shop

    void Awake()
    {
        shopButtons.Clear();
    }

    void Start()
    {
        SetFilter("characters");

        if (shopCanvas.activeSelf)                      //Check if the shop is open or not
        {
            OpenShop();
        }
        else
        {
            shopOpened = false;
        }
    }

    void Update()
    {
        //Open / close inventory with 'U' (PC)
        if (Input.GetKeyUp(KeyCode.U) && !shopOpened)
        {
            OpenShop();
        }
        else if (Input.GetKeyUp(KeyCode.U) && shopOpened)
        {
            CloseShop();
        }
    }

    void OpenShop()
    {
        SetCoinAmount();
        shopCanvas.SetActive(true);
        Time.timeScale = 0;
        shopOpened = true;
    }

    void CloseShop()
    {
        shopCanvas.SetActive(false);
        Time.timeScale = 1;
        shopOpened = false;
    }

    void SetCoinAmount()
    {
        PlayerPrefs.SetInt("myCoins", 10);
        coinAmount.text = "x" + PlayerPrefs.GetInt("myCoins").ToString();
    }

    public void PurchaseItem(ShopItem item, int coinAmount, int itemCost)
    {
        //Update coin amount
        PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
        SetCoinAmount();

        //Unlock item for the player to use
        AddToInventory(item);

        //"Small Spender" achievement
        UpdateAchievement(GPGSIds.achievement_small_spender);
    }

    void UpdateAchievement(string achievementName)
    {
        // Only unlock achievements if the user is signed in.
        if (Social.localUser.authenticated)
        {
            //Unlock the "Small Spender" achievement
            PlayGamesPlatform.Instance.ReportProgress(
                achievementName,
                100.0f, (bool success) =>
                {
                    Debug.Log("(Bacon Break) Small Spender Unlock: " +
                          success);
                });
        }
    }

    void AddToInventory(ShopItem item)
    {
        //Unlock item, update inventory lists
        item.isUnlocked = true;
        inventoryController.FillInventory();
    }

    public void Next()
    {
        foreach (ShopButton button in shopButtons)
        {
            if (button.gameObject.activeSelf)
            {
                if ((button.itemIndex + 1) < filteredItems.Count)
                {
                    button.itemIndex++;
                }
                else
                {
                    button.itemIndex = 0;
                }

                button.SetButton();
            }
        }
    }

    public void Back()
    {
        foreach (ShopButton button in shopButtons)
        {
            if (button.gameObject.activeSelf)
            {
                if ((button.itemIndex - 1) >= 0)
                {
                    button.itemIndex--;
                }
                else
                {
                    button.itemIndex = (filteredItems.Count - 1);
                }

                button.SetButton();
            }
        }
    }

    public void SetFilter(string filterType)
    {
        //Make sure list is empty
        filteredItems.Clear();
        int buttonCount = 0;

        //Filter objects based on type (character or upgrade)
        if (filterType == "characters")
        {
            filteredItems.Clear();
            EnableButton(upgrFilter);
            DisableButton(charFilter);

            for (int i = 0; i < shopItems.Count; i++)
            {
                if (shopItems[i].isCharacter)
                {
                    filteredItems.Add(shopItems[i]);

                    //Populate the three buttons
                    if (buttonCount < shopButtons.Count)
                    {
                        shopButtons[buttonCount].SetButton();
                        buttonCount++;
                    }
                }
            }
        }
        else if (filterType == "upgrades")
        {
            filteredItems.Clear();
            EnableButton(charFilter);
            DisableButton(upgrFilter);

            for (int i = 0; i < shopItems.Count; i++)
            {
                if (!shopItems[i].isCharacter)
                {
                    filteredItems.Add(shopItems[i]);

                    //Populate the three buttons
                    if (buttonCount < shopButtons.Count)
                    {
                        shopButtons[buttonCount].SetButton();
                        buttonCount++;
                    }
                }
            }
        }

        //Disable the rest of the buttons
        if (buttonCount < shopButtons.Count)
        {
            for (int i = buttonCount; i < shopButtons.Count; i++)
            {
                shopButtons[i].SetButton();
            }
        }
    }

    public void DisableButton(Button button)
    {
        //Set greyish color for the disabled button
        button.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);
        ColorBlock cb = button.colors;
        cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
        button.colors = cb;

        button.interactable = false;
    }

    public void EnableButton(Button button)
    {
        //Set color back to purple and enable the button
        button.GetComponent<Image>().color = new Color(179.0f / 255.0f, 167.0f / 255.0f, 223.0f / 255.0f, 201.0f / 255.0f);
        button.interactable = true;
    }
}
