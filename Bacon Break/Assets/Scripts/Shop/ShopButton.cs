using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    //Public variables.
    public ShopController shopController;
    public int itemIndex;

    public Text itemName;
    public Image itemImage;
    public Text itemCost;
    public Text itemDesc;

    public Button thisButton;

    //Do this before everthing else.
    void Awake()
    {
        thisButton = GetComponent<Button>();
    }

    //Initialization.
    void Start()
    {
        //Set onclicklistener for buttons. SaveSelectedItem: holds selected item for purchase.
        thisButton.onClick.AddListener(() =>
        {
            shopController.SaveSelectedItem(shopController.filteredShopItems[itemIndex],
            shopController.shopButtons.IndexOf(this));
        });
    }

    //Set the three shop buttons with the first three shop items.
    public void SetButton()
    {
        if (itemIndex >= 0 && itemIndex < shopController.filteredShopItems.Count)
        {
            gameObject.SetActive(true);
            DisplayButton();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //Display shop item information on a shop button.
    public void DisplayButton()
    {
        //Name, image and price
        itemName.text = shopController.filteredShopItems[itemIndex].itemName;
        itemImage.sprite = shopController.filteredShopItems[itemIndex].itemSprite;
        itemCost.text = shopController.filteredShopItems[itemIndex].itemCost.ToString();

        //Disable the button if a unique item is already purchased
        if (shopController.filteredShopItems[itemIndex].isUnlocked
            && shopController.filteredShopItems[itemIndex].isUnique)
        {
            shopController.DisableFilterButton(thisButton, true);
        }
        else
        {
            shopController.EnableFilterButton(thisButton, "orange");
        }
    }
}
