using UnityEngine;
using System.Collections;

public class PlayerCurrency : MonoBehaviour {
    public int myCurrency;
    // Use this for initialization
    public void SaveCurrency()
    {
        PlayerPrefs.SetInt("playerCoins", myCurrency);
    }
}
