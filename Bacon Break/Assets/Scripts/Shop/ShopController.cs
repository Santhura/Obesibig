﻿using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    private ShopItem item;
    private int btnIndex;

    //For opening and closing the shop
    [Header("CANVAS_SETTINGS")]
    public GameObject shopCanvas;
    private bool shopOpened;

    [Header("TRANSACTION_SETTINGS")]
    public List<ShopItem> shopItems;                    //For keeping track of all the (shop) items
    public List<ShopButton> shopButtons;                //For cycling between shop items
    public InventoryController inventoryController;     //Primarily used for making transactions between the shop and the inventory
    public List<ShopItem> filteredItems;                //Temporary list for storing filtered items in the shop

    [Header("UI_SHIT")]
    public Text coinAmount;                             //For keeping track of the amount of coins
    public Button charFilter, upgrFilter;               //For filtering, obviously
    public Button btn_next, btn_back;                   //Disabling/enabling (if items in the list are less than 4 or more than 3)
    public GameObject pnl_dialog, pnl_alert;
    public Button btn_confirm, btn_cancel, btn_ok;

    void Start()
    {
        //Add button listeners
        btn_confirm.onClick.AddListener(() => { PurchaseItem(item, PlayerPrefs.GetInt("myCoins"), item.itemCost); });
        btn_cancel.onClick.AddListener(() => { HidePanel(pnl_dialog); });
        btn_ok.onClick.AddListener(() => { HidePanel(pnl_alert); });

        if (shopCanvas.activeInHierarchy)              //Check if the shop is open or not
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    void Update()
    {
        //Open / close inventory with 'U' (PC)
        /*if (Input.GetKeyUp(KeyCode.U) && !shopOpened)
        {
            OpenShop();
        }
        else if (Input.GetKeyUp(KeyCode.U) && shopOpened)
        {
            CloseShop();
        }*/
    }

    public void OpenShop()
    {
        shopCanvas.SetActive(true);
        SetCoinAmount();
        shopOpened = true;
        Time.timeScale = 0;
        SetFilter("characters");
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
        shopOpened = false;
        Time.timeScale = 1;
    }

    void SetCoinAmount()
    {
        PlayerPrefs.SetInt("myCoins", 50);
        coinAmount.text = "x" + PlayerPrefs.GetInt("myCoins").ToString();
    }

    public void PurchaseItem(ShopItem item, int coinAmount, int itemCost)
    {
        if (coinAmount >= itemCost)
        {
            HidePanel(pnl_dialog);

            if (item.isUnique)
            {
                DisableButton(shopButtons[btnIndex].thisButton, true);
            }

            //Update coin amount
            PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
            SetCoinAmount();

            //Unlock item for the player to use
            AddToInventory(item);

            //"Small Spender" achievement
            UpdateAchievement(GPGSIds.achievement_small_spender);
        }
        else
        {
            HidePanel(pnl_dialog);
            pnl_alert.SetActive(true);
        }
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

    //For confirming a purchase
    public void SaveShopItem(ShopItem clickedItem, int buttonIndex)
    {
        item = clickedItem;
        btnIndex = buttonIndex;

        ShowDialogPanel();
    }

    //For confirng
    public void ShowDialogPanel()
    {
        pnl_dialog.SetActive(true);
        pnl_dialog.GetComponentInChildren<Text>().text = "Do you really want to buy \""
                                                         + item.itemName + "\"?"
                                                         + "\nCost: " + item.itemCost;
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
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
        //Reset button indices
        int buttonCount = 0;
        filteredItems.Clear();

        shopButtons[0].itemIndex = 0;
        shopButtons[1].itemIndex = 1;
        shopButtons[2].itemIndex = 2;

        //Filter objects based on type (character or upgrade)
        if (filterType == "characters")
        {
            EnableButton(upgrFilter, "grey");
            DisableButton(charFilter, false);

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
            EnableButton(charFilter, "grey");
            DisableButton(upgrFilter, false);

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

        //Enable/disable next/back button
        //There are 3 buttons, if there are more than 3 items, enable next/back
        if (filteredItems.Count > 3)
        {
            EnableButton(btn_back, "white");
            EnableButton(btn_next, "white");
        }
        else
        {
            DisableButton(btn_back, true);
            DisableButton(btn_next, true);
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

    public void DisableButton(Button button, bool isItemButton)
    {
        if (isItemButton)
        {
            //Set greyish color for the disabled button
            button.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);

            ColorBlock cb = button.colors;
            cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            button.colors = cb;
        }
        else
        {
            button.GetComponent<Image>().color = Color.white;

            ColorBlock cb = button.colors;
            cb.normalColor = Color.white;
            button.colors = cb;
        }

        button.interactable = false;
    }

    public void EnableButton(Button button, string color)
    {
        //Set color back to purple and enable the button
        if (color == "orange")
        {
            button.GetComponent<Image>().color = new Color(1.0f, 162.0f / 255.0f, 0.0f, 1.0f);
        }
        else if(color == "grey")
        {
            //Set greyish color for the disabled button
            button.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);

            ColorBlock cb = button.colors;
            cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            button.colors = cb;
        }
        else if (color == "white")
        {
            button.GetComponent<Image>().color = Color.white;

            ColorBlock cb = button.colors;
            cb.normalColor = Color.white;
            button.colors = cb;
        }

        button.interactable = true;
    }
}
