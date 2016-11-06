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

        DisplayButton();

        if (!shopController.shopButtons.Contains(this))
        {
            shopController.shopButtons.Add(this);
        }
    }

    public void DisplayButton()
    {
        //Set item information
        itemName.text = shopController.shopItems[itemIndex].itemName;
        itemImage.sprite = shopController.shopItems[itemIndex].itemSprite;
        itemCost.text = "Coins: " + shopController.shopItems[itemIndex].itemCost.ToString();
        itemDesc.text = shopController.shopItems[itemIndex].itemDesc;

        //Disable the button if a unique item is already purchased
        if (shopController.shopItems[itemIndex].isUnlocked
            && shopController.shopItems[itemIndex].isUnique)
        {
            DisableButton();
        }
        else
        {
            EnableButton();
        }
    }

    void AttemptPurchase()
    {
        int coinAmount = PlayerPrefs.GetInt("myCoins");
        int cost = shopController.shopItems[itemIndex].itemCost;

        //Purchase if the player has enough money
        if (coinAmount >= cost)
        {
            shopController.PurchaseItem(itemIndex, coinAmount, cost);

            if (shopController.shopItems[itemIndex].isUnique)
            {
                DisableButton();
            }
        }
        else
        {
            DebugConsole.Log("You don't have enough coins!");
        }
    }

    void DisableButton()
    {
        //Set greyish color for the disabled button
        thisButton.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);
        ColorBlock cb = thisButton.colors;
        cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
        thisButton.colors = cb;

        thisButton.interactable = false;
    }

    void EnableButton()
    {
        //Set color back to purple
        thisButton.GetComponent<Image>().color = new Color(179.0f / 255.0f, 167.0f / 255.0f, 223.0f / 255.0f, 201.0f / 255.0f);
        thisButton.interactable = true;
    }
}
