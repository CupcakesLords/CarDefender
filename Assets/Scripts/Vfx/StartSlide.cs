using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSlide : MonoBehaviour
{
    public void Activate(Vector3 start)
    {
        StartCoroutine(Animation(start, new Vector3(-2f, 4f, 0)));
    }

    IEnumerator Animation(Vector3 start, Vector3 finish)
    {
        yield return PositionLerp(start, finish, 1.5f);
    }

    public IEnumerator PositionLerp(Vector3 start, Vector3 finish, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 2.5f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(start, finish, i);
            yield return null;
        }
        StartPopUp.AddToPool(gameObject);
        GameUI.UISystem.levelBoard.GetComponent<LevelBoard>().setProgress();
    }

    void Update()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.z += 2000f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
