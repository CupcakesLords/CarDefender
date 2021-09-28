using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private static ObjectPool DamageText;
    private static GameObject ParentHolder;
    public ObjectPool ObjectPrefab;
    public GameObject Parent;

    private void Awake()
    {
        ParentHolder = Parent;
        DamageText = ObjectPrefab;
        DamageText.Init();
    }

    public static void ShowDamageText(Vector3 pos, int dmg)
    {
        string tag = "DamageText";
        GameObject txt = DamageText.GetFromQueue(tag);
        if (txt == null)
        {
            GameObject txtObject = Instantiate(DamageText.ObjectPrefab[0], pos, Quaternion.identity);
            txtObject.transform.SetParent(ParentHolder.transform, false);
            txtObject.GetComponent<DamageText>().SetText(dmg);
            txtObject.GetComponent<DamageText>().StartAnimation(pos);
        }
        else
        {
            txt.SetActive(true);
            txt.transform.position = pos;
            txt.GetComponent<DamageText>().SetText(dmg);
            txt.GetComponent<DamageText>().StartAnimation(pos);
        }
    }

    public static void AddToPool(GameObject txt)
    {
        txt.SetActive(false);
        DamageText.AddToQueue("DamageText", txt);
    }
}
