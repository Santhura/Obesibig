using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ShopItem : ScriptableObject
{
    public string itemName = "Shop item name here.";
    public Sprite itemSprite;
    public int itemCost = 10;
    public string itemDesc = "Shop item description here.";
    public bool isUnique;

    public bool isCharacter = false;
    public bool isUnlocked = false;
    public string prefabName = "Put prefab name here";
}
