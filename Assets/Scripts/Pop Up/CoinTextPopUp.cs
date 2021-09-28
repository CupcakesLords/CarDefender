using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTextPopUp : MonoBehaviour
{
    private static ObjectPool CoinText;
    private static GameObject ParentHolder;
    public ObjectPool ObjectPrefab;
    public GameObject Parent;

    private void Awake()
    {
        ParentHolder = Parent;
        CoinText = ObjectPrefab;
        CoinText.Init();
    }

    public static void ShowCoinText(Vector3 pos, int coin)
    {
        string tag = "coinText";
        GameObject txt = CoinText.GetFromQueue(tag);
        if (txt == null)
        {
            GameObject txtObject = Instantiate(CoinText.ObjectPrefab[0], pos, Quaternion.identity);
            txtObject.transform.SetParent(ParentHolder.transform, false);
            txtObject.GetComponent<CoinText>().SetText(coin);
            txtObject.GetComponent<CoinText>().StartAnimation(pos);
        }
        else
        {
            txt.SetActive(true);
            txt.transform.position = pos;
            txt.GetComponent<CoinText>().SetText(coin);
            txt.GetComponent<CoinText>().StartAnimation(pos);
        }
    }

    public static void AddToPool(GameObject txt)
    {
        txt.SetActive(false);
        CoinText.AddToQueue("coinText", txt);
    }
}
