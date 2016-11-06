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
    public Text coinAmount;                             //For keeping track of the amount coins

    void Start()
    {
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

    public void PurchaseItem(int itemIndex, int coinAmount, int itemCost)
    {
        //Update coin amount
        PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
        SetCoinAmount();

        //Unlock item for the player to use
        AddToInventory(itemIndex);

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

    void AddToInventory(int itemIndex)
    {
        //Unlock item, update inventory lists
        shopItems[itemIndex].isUnlocked = true;
        inventoryController.FillInventory();
    }

    public void Next()
    {
        foreach (ShopButton button in shopButtons)
        {
            if ((button.itemIndex + 1) < shopItems.Count)
            {
                button.itemIndex++;
            }
            else
            {
                button.itemIndex = 0;
            }

            button.DisplayButton();
        }
    }

    public void Back()
    {
        foreach (ShopButton button in shopButtons)
        {
            if ((button.itemIndex - 1) >= 0)
            {
                button.itemIndex--;
            }
            else
            {
                button.itemIndex = (shopItems.Count - 1);
            }

            button.DisplayButton();
        }
    }
}
