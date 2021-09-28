using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinText : MonoBehaviour
{
    public TextMeshProUGUI number;

    IEnumerator Animation;

    private void Start()
    {

    }

    public void SetText(int coin)
    {
        number.text = "$" + coin.ToString();
    }

    public void StartAnimation(Vector3 start)
    {
        Animation = LerpAnimation(start, 0.2f);
        StartCoroutine(Animation);
    }

    IEnumerator LerpAnimation(Vector3 start, float duration)
    {
        Vector3 finish = new Vector3(start.x, start.y + 20f, start.z);
        yield return PositionLerp(start, finish, duration);

        CoinTextPopUp.AddToPool(gameObject);
    }

    public IEnumerator PositionLerp(Vector3 start, Vector3 finish, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 0.5f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(start, finish, i);
            yield return null;
        }
    }
}
