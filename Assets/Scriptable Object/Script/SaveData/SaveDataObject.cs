using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Save Data Object", menuName = "Save Data")]
public class SaveDataObject : ScriptableObject
{
    //ShopUI
    public int currentNumber;
    public int currentProgress;
    //Money text
    public int money;
    public int moneyPS;
    //Purchase
    //public int price;
    //public Sprite sprite;
    //Enemies
    public int level;
    //Slots
    public int[] slot;
    //Each enemy
}
