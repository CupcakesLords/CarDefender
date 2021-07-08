using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -4f)
        {
            gameObject.GetComponent<Enemy>().Spawn();
        }
    }
}
