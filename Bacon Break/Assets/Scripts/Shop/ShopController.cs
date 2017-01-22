using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    //Singleton pattern
    private static ShopController instance;
    public static ShopController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ShopController();
            }
            return instance;
        }
    }

    //Private variables
    private bool shopOpened;
    private int buttonIndex;
    private ShopItem shopItem;
    private List<ShopItem> characterItems;
    private List<ShopItem> upgradeItems;

    private InventoryController inventoryController = InventoryController.Instance;

    //Public variables
    public ShopItem defaultCharacter;
    public List<ShopItem> shopItems;
    public List<ShopItem> filteredShopItems;
    public GameObject shopCanvas;
    public List<ShopButton> shopButtons;

    public Text txtCoinAmount;
    public Text txtItemCost;
    public Button characterFilter, upgradeFilter;
    public Button btnNext, btnPrevious;
    public Button btnConfirm, btnCancel, btnOK;
    public GameObject pnlDialog, pnlAlert;

    //Initialization
    void Start()
    {
        characterItems = new List<ShopItem>();
        upgradeItems = new List<ShopItem>();

        //Add button listeners
        btnConfirm.onClick.AddListener(() => { PurchaseItem(shopItem, PlayerPrefs.GetInt("myCoins"), shopItem.itemCost); });
        btnCancel.onClick.AddListener(() => { HidePanel(pnlDialog); });
        btnOK.onClick.AddListener(() => { HidePanel(pnlAlert); });

        //Only once
        GetShopItems();

        //Check if the shop is open or not
        if (shopCanvas.activeInHierarchy)
        {
            OpenShop();
        }
        else
        {
            CloseShop();
        }
    }

    void OpenShop()
    {
        shopCanvas.SetActive(true);
        SetCoinAmount();
        shopOpened = true;

        Time.timeScale = 0;

        SetFilter("characters");
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
        Time.timeScale = 1;
        shopOpened = false;
    }

    void GetShopItems()
    {
        inventoryController.Add(defaultCharacter);

        //Filter objects based on type
        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i].isCharacter)
            {
                characterItems.Add(shopItems[i]);
            }
            else
            {
                upgradeItems.Add(shopItems[i]);
            }

            //Also add to inventory list if unlocked
            if (shopItems[i].isUnlocked)
            {
                inventoryController.Add(shopItems[i]);
            }
        }
    }

    void SetCoinAmount()
    {
        PlayerPrefs.SetInt("myCoins", 50);
        txtCoinAmount.text = "x " + PlayerPrefs.GetInt("myCoins").ToString();
    }

    //For confirming dialogs
    void ShowDialogPanel()
    {
        pnlDialog.SetActive(true);
        pnlDialog.GetComponentInChildren<Text>().text = "Do you really want to buy \""
                                                         + shopItem.itemName + "\"?";
        txtItemCost.text = "x " + shopItem.itemCost.ToString();
    }

    void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void DisableButton(Button button, bool isItemButton)
    {
        if (isItemButton)
        {
            //Set greyish color for the disabled button
            button.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);

            ColorBlock cb = button.colors;
            cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            button.colors = cb;
        }
        else
        {
            button.GetComponent<Image>().color = Color.white;

            ColorBlock cb = button.colors;
            cb.normalColor = Color.white;
            button.colors = cb;
        }

        button.interactable = false;
    }

    public void EnableButton(Button button, string color)
    {
        //Set color back to purple and enable the button
        if (color == "orange")
        {
            button.GetComponent<Image>().color = new Color(1.0f, 162.0f / 255.0f, 0.0f, 1.0f);
        }
        else if (color == "grey")
        {
            //Set greyish color for the disabled button
            button.GetComponent<Image>().color = new Color(146.0f / 255.0f, 146.0f / 255.0f, 146.0f / 255.0f, 1.0f);

            ColorBlock cb = button.colors;
            cb.disabledColor = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 1.0f);
            button.colors = cb;
        }
        else if (color == "white")
        {
            button.GetComponent<Image>().color = Color.white;

            ColorBlock cb = button.colors;
            cb.normalColor = Color.white;
            button.colors = cb;
        }

        button.interactable = true;
    }

    void PurchaseItem(ShopItem shopItem, int coinAmount, int itemCost)
    {
        if (coinAmount >= itemCost)
        {
            HidePanel(pnlDialog);

            if (shopItem.isUnique)
            {
                DisableButton(shopButtons[buttonIndex].thisButton, true);
            }

            //Update coin amount
            PlayerPrefs.SetInt("myCoins", coinAmount - itemCost);
            SetCoinAmount();

            //Unlock item for the player to use
            shopItem.isUnlocked = true;
            inventoryController.Add(shopItem);

            //"Small Spender" achievement
            //Achievement.Unlock(GPGSIds.achievement_small_spender);
        }
        else
        {
            HidePanel(pnlDialog);
            pnlAlert.SetActive(true);
        }
    }

    public void SaveSelectedItem(ShopItem clickedItem, int buttonIndex)
    {
        shopItem = clickedItem;
        this.buttonIndex = buttonIndex;

        ShowDialogPanel();
    }

    //Public voor onclick button
    public void NextItem()
    {
        foreach (ShopButton button in shopButtons)
        {
            if (button.gameObject.activeSelf)
            {
                if ((button.itemIndex + 1) < filteredShopItems.Count)
                {
                    button.itemIndex++;
                }
                else
                {
                    button.itemIndex = 0;
                }

                button.SetButton();
            }
        }
    }

    //public voor onclick button
    public void PreviousItem()
    {
        foreach (ShopButton button in shopButtons)
        {
            if (button.gameObject.activeSelf)
            {
                if ((button.itemIndex - 1) >= 0)
                {
                    button.itemIndex--;
                }
                else
                {
                    button.itemIndex = (filteredShopItems.Count - 1);
                }

                button.SetButton();
            }
        }
    }

    public void SetFilter(string filterType)
    {
        //Reset button list because reasons... I <3 Unity
        shopButtons[0].itemIndex = 0;
        shopButtons[1].itemIndex = 1;
        shopButtons[2].itemIndex = 2;

        if (filterType == "characters")
        {
            //Enable filter button for upgrades, disable the character filter
            EnableButton(upgradeFilter, "grey");
            DisableButton(characterFilter, false);

            filteredShopItems = new List<ShopItem>(characterItems);
        }
        else if (filterType == "upgrades")
        {
            //Enable filter button for characters, disable the upgrade filter
            EnableButton(characterFilter, "grey");
            DisableButton(upgradeFilter, false);

            filteredShopItems = new List<ShopItem>(upgradeItems);
        }

        //Populate the first three buttons, if possible
        for (int i = 0; i < 3; i++)
        {
            shopButtons[i].SetButton();
        }

        //Enable or disable the next/back button
        if (filteredShopItems.Count > 3)
        {
            EnableButton(btnPrevious, "white");
            EnableButton(btnNext, "white");
        }
        else
        {
            DisableButton(btnPrevious, true);
            DisableButton(btnNext, true);
        }
    }
}
