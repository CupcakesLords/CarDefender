using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ObjectPool EffectPool;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddToPool()
    {
        gameObject.SetActive(false); 
        EffectPool.AddToQueue(GetComponent<SpriteRenderer>().sprite.name, gameObject); 
    }
}
