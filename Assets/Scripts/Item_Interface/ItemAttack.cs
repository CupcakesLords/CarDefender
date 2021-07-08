using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    Rigidbody2D body;
    Animator animator;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        animator.Play("Base Layer.Car_Animation_Attack", 0, 0);
        GameObject projectileObject = Instantiate(projectilePrefab, body.position, Quaternion.identity);
        Bullet projectile = projectileObject.GetComponent<Bullet>();
        if(projectile.isActiveAndEnabled)
        {
            projectile.Launch();
        }
    }
}
