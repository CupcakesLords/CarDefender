using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int HealthPoint;
    [HideInInspector]
    public int CurrentHealth;

    void Start()
    {
        CurrentHealth = HealthPoint;
    }

    public void TakeDamage(int dmg)
    {
        CurrentHealth = CurrentHealth - dmg;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = HealthPoint;
            gameObject.GetComponent<Enemy>().Spawn();
            return;
        }
    }
}
