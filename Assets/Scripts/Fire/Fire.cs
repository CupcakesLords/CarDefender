using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ChangeFlicker(Sprite spr)
    {
        GetComponent<SpriteRenderer>().sprite = spr;
    }
}
