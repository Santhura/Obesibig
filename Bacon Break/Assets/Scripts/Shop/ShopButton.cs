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
            shopController.PurchaseItem(itemIndex);
        }
    }
}
