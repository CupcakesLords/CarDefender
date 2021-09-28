using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScreen : MonoBehaviour
{
    public GameObject levelText;

    public void SetLevel(int lv)
    {
        levelText.GetComponent<TextMeshProUGUI>().text = "Wave " + lv.ToString();
    }
}
