using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public static GameUI UISystem;
    private void Awake()
    {
        UISystem = this;
    }

    public GameObject UITextLevel;
    public GameObject UITextLose;
    public GameObject UIShop;
    public void OpenUIShop()
    {
        UIShop.SetActive(true);
        UIShop.GetComponent<ShopUI>().SetData();
    }
    public void CloseUIShop()
    {
        UIShop.SetActive(false);
    }

    public GameObject Purchase;

    public GameObject moneyText;

    public GameObject noMoneyText;

    public GameObject priceText;

    public GameObject levelBoard;
}
