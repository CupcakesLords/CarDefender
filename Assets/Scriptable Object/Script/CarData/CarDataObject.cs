using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Data Object", menuName = "Car Number")]
public class CarDataObject : ScriptableObject 
{
    public string Name;
    public int Level;
    public Sprite Image;
    public float moneyPS;
    public float damagePS;
    public float fireSpeed;
    public float earning;
    public float damage;
    public float buyPrice;
    public float upgradePrice;
}
