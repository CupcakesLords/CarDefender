using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour, Item
{
    //private float attackSpeed;
    //private float attackSpeedTimer;
    //private Animator animator;
    //private Rigidbody2D body;
    
    void Start()
    {
        //attackSpeed = 1f;
        //attackSpeedTimer = attackSpeed;
        //animator = GetComponent<Animator>();
        //body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (attackSpeedTimer > 0)
        //{
        //    attackSpeedTimer -= Time.deltaTime;
        //    return;
        //}
        //else
        //{
        //    Attack();
        //    attackSpeedTimer = attackSpeed;
        //}
    }

    public void Attack()
    {
        ItemAttack attack = GetComponent<ItemAttack>();
        if(attack.isActiveAndEnabled)
        {
            attack.Attack();
        }
    }

    public void Inactive()
    {
        GetComponent<ItemAttack>().enabled = false;
    }

    public void Active()
    {
        GetComponent<ItemAttack>().enabled = true;
    }
}
