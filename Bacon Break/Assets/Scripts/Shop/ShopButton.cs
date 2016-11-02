using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace CompleteProject
{
    public class ShopButton : MonoBehaviour
    {
        public ShopController shopController;
        public int itemIndex;
        private Button thisButton;

        //Children of the button.
        public Text itemName;
        public Image itemImage;
        public Text itemCost;
        public Text itemDesc;

        // Use this for initialization
        void Start()
        {
            //Set onclicklistener for button
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(() => { AttemptPurchase(); });

            SetButton();
        }

        void SetButton()
        {
            string costString = shopController.shopItems[itemIndex].itemCost.ToString();

            itemName.text = shopController.shopItems[itemIndex].itemName;
            itemImage.sprite = shopController.shopItems[itemIndex].itemSprite;
            itemCost.text = "Coins: " + costString;
            itemDesc.text = shopController.shopItems[itemIndex].itemDesc;
        }

        void AttemptPurchase()
        {
            int coinAmount = PlayerPrefs.GetInt("myCoins");
            int cost = shopController.shopItems[itemIndex].itemCost;

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
            //thisButton.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);
            cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            thisButton.colors = cb;

            thisButton.interactable = false;
        }
    }
}
