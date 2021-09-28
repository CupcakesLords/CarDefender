using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthText : MonoBehaviour
{
    GameObject enemy;

    public void SetEnemy(GameObject e)
    {
        enemy = e;
    }

    void Update()
    {
        if (enemy != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position);
        }
    }

    public void SetText(int health)
    {
        GetComponent<TextMeshProUGUI>().text = health.ToString();
    }
}
