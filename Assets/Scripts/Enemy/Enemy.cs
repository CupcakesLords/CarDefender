using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, FallEnemy
{
    Vector3 returnPos;
    Rigidbody2D body;
    
    private void Start()
    {
        returnPos = transform.position;
        body = GetComponent<Rigidbody2D>();
        Fall();
    }

    public void Spawn()
    {
        transform.position = returnPos;
    }

    public void Halt()
    {
        body.velocity = Vector2.zero;
        body.angularVelocity = 0;
    }

    public void Fall()
    {
        body.AddForce(new Vector3(0, -1, 0) * 75);
    }
}
