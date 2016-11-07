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

    public Button thisButton;

    void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    // Use this for initialization
    void Start()
    {
        //Set onclicklistener for buttons
        thisButton.onClick.AddListener(() => { shopController.SaveShopItem(shopController.filteredItems[itemIndex], 
                                               shopController.shopButtons.IndexOf(this)); });   
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
            shopController.DisableButton(thisButton, true);
        }
        else
        {
            shopController.EnableButton(thisButton, true);
        }
    }
}
