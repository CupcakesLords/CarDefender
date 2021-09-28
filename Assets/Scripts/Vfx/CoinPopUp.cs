using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPopUp : MonoBehaviour
{
    private static ObjectPool CoinEffect;
    private static GameObject Effect;
    public ObjectPool ObjectPrefab;
    public GameObject EffectPrefab;

    private void Awake()
    {
        Effect = EffectPrefab;
        CoinEffect = ObjectPrefab;
        CoinEffect.Init();
    }

    public static void ShowCoinEffect(Vector3 pos, int i, int j)
    {
        string tag = "CoinParticle";
        GameObject proj = CoinEffect.GetFromQueue(tag);
        if (proj == null)
        {
            GameObject EffectObject = Instantiate(Effect, pos, Quaternion.identity);
            EffectObject.GetComponent<CoinSlide>().Play(i, j);
        }
        else
        {
            proj.SetActive(true);
            proj.transform.position = pos;
            proj.GetComponent<CoinSlide>().Play(i, j);
        }
    }

    public static void AddToPool(GameObject coin)
    {
        coin.GetComponent<ParticleSystem>().Stop();
        coin.SetActive(false);
        CoinEffect.AddToQueue(coin.name.Substring(0, 12), coin);
    }
}
