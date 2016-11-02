using UnityEngine;
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

    public void PurchaseItem(int itemIndex)
    {
        int coinAmount = PlayerPrefs.GetInt("myCoins");
        int itemCost = shopItems[itemIndex].itemCost;
        
        if (coinAmount >= itemCost)
        {
            PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
            SetCointAmount();
        }
    }
}
