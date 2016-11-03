using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    //Keeps track of all the shop items.
    public List<ShopItem> shopItems;
    public Text coinAmount;

    //The actual shop
    public GameObject shopCanvas;

    //For unlocking inventory items
    public InventoryController inventoryController;

    //For opening and closing the shop
    private bool shopOpened;

    void Start()
    {
        if (shopCanvas.activeSelf)
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
        //PlayerPrefs.SetInt("myCoins", 10);
        coinAmount.text = "x" + PlayerPrefs.GetInt("myCoins").ToString();
    }

    //Purchase item if the player has enough coins
    public void PurchaseItem(int itemIndex, int coinAmount, int itemCost)
    {     
        //Update coin amount
        PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
        SetCoinAmount();

        //Unlock item for the player to use
        AddToInventory(itemIndex);

        //"Small Spender" achievement
        UpdateAchievement(GPGSIds.achievement_small_spender);

        //Show on the console what item you have bought
        DebugConsole.Log(shopItems[itemIndex].itemName + " purchased!");
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
        shopItems[itemIndex].isUnlocked = true;

        inventoryController.Add(shopItems[itemIndex], shopItems[itemIndex].isCharacter);
    }
}
