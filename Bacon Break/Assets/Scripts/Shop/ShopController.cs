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
    public GameObject shopContainer;

    //For unlocking inventory items
    public GameObject inventoryController;

    //For opening and closing the shop
    private bool shopOpened;

    void Start()
    {
        if (shopContainer.activeSelf)
        {
            shopOpened = true;
            SetCointAmount();
        }
        else
        {
            shopOpened = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && !shopOpened)
        {
            OpenShop();
        }
        else if (Input.GetKeyUp(KeyCode.Q) && shopOpened)
        {
            CloseShop();
        }
    }

    void OpenShop()
    {
        shopContainer.SetActive(true);
        Time.timeScale = 0;
        shopOpened = true;
    }

    void CloseShop()
    {
        shopContainer.SetActive(false);
        Time.timeScale = 1;
        shopOpened = false;
    }

    void SetCointAmount()
    {
        //PlayerPrefs.SetInt("myCoins", 10);
        coinAmount.text = "x" + PlayerPrefs.GetInt("myCoins").ToString();
    }

    //Purchase item if the player has enough coins
    public void PurchaseItem(int itemIndex, int coinAmount, int itemCost)
    {     
        //Update coin amount (amount - cost)
        PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
        SetCointAmount();

        //Unlock item for the player to use
        /*
         * 
        */

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
}
