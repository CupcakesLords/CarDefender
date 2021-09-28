using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoMoneyText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    float Timer;

    private void Awake()
    {
        gameObject.SetActive(false);
        Timer = 1.5f;
    }

    public void Activate()
    {
        Timer = 1.5f;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
            Timer = 1.5f;
        }
    }

    public void setText(string txt)
    {
        Text.text = txt;
    }
}
