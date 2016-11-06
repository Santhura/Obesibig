using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public ShopController shopController;
    public int itemIndex;

    public Text itemName;
    public Image itemImage;
    public Text itemCost;
    public Text itemDesc;

    private Button thisButton;

    // Use this for initialization
    void Start()
    {
        //Set onclicklistener for this button
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() => { AttemptPurchase(); });

        if (!shopController.shopButtons.Contains(this))
        {
            shopController.shopButtons.Add(this);
        }
    }

    public void SetButton()
    {
        if (itemIndex >= 0 && itemIndex < shopController.filteredItems.Count)
        {
            gameObject.SetActive(true);
            DisplayButton();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void DisplayButton()
    {
        //Set item information
        itemName.text = shopController.filteredItems[itemIndex].itemName;
        itemImage.sprite = shopController.filteredItems[itemIndex].itemSprite;
        itemCost.text = "Coins: " + shopController.filteredItems[itemIndex].itemCost.ToString();
        itemDesc.text = shopController.filteredItems[itemIndex].itemDesc;

        //Disable the button if a unique item is already purchased
        if (shopController.filteredItems[itemIndex].isUnlocked
            && shopController.filteredItems[itemIndex].isUnique)
        {
            shopController.DisableButton(thisButton);
        }
        else
        {
            shopController.EnableButton(thisButton);
        }
    }

    void AttemptPurchase()
    {
        int coinAmount = PlayerPrefs.GetInt("myCoins");
        int cost = shopController.filteredItems[itemIndex].itemCost;
        ShopItem item = shopController.filteredItems[itemIndex];

        //Purchase if the player has enough money
        if (coinAmount >= cost)
        {
            shopController.PurchaseItem(item, coinAmount, cost);

            if (shopController.filteredItems[itemIndex].isUnique)
            {
                shopController.DisableButton(thisButton);
            }
        }
        else
        {
            DebugConsole.Log("You don't have enough coins!");
        }
    }
}
