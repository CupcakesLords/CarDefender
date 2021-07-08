using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, Projectile
{
    Rigidbody2D body;
    float duration;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        duration = -1;

    }
    void Start()
    {
        
    }

    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            if (duration <= 0 || transform.position.y > 5.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Damage()
    {
        
    }

    public void Launch()
    {
        duration = 5f;
        body.AddForce(new Vector3(0, 1, 0) * 1000);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyHealth temp = collision.GetComponent<EnemyHealth>();
            temp.TakeDamage(GetComponent<DoDamage>().Damage);
            Destroy(gameObject);
        }
    }
}

