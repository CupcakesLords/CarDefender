using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPopUp : MonoBehaviour
{
    private static ObjectPool StarEffect;
    private static GameObject Effect;
    public ObjectPool ObjectPrefab;
    public GameObject EffectPrefab;

    private void Awake()
    {
        Effect = EffectPrefab;
        StarEffect = ObjectPrefab;
        StarEffect.Init();
    }

    public static void ShowCoinEffect(Vector3 pos)
    {
        string tag = "stars 2";
        GameObject proj = StarEffect.GetFromQueue(tag);
        if (proj == null)
        {
            GameObject EffectObject = Instantiate(Effect, pos, Quaternion.identity);
            EffectObject.GetComponent<StartSlide>().Activate(pos);
        }
        else
        {
            proj.SetActive(true);
            proj.transform.position = pos;
            proj.GetComponent<StartSlide>().Activate(pos);
        }
    }

    public static void AddToPool(GameObject star)
    {
        star.SetActive(false);
        StarEffect.AddToQueue("stars 2", star);
    }
}
