using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gift : MonoBehaviour, IPointerDownHandler
{
    GameObject carUnderneath;
    public Sprite free;
    public Sprite shop;
    int j; int m;

    public void SetSprite(bool buy)
    {
        if (buy)
            GetComponent<SpriteRenderer>().sprite = shop;
        else
            GetComponent<SpriteRenderer>().sprite = free;
    }

    public void SetCarUnderneath(GameObject car, int jj, int mm)
    {
        carUnderneath = car;
        j = jj; m = mm;
    }

    //private void OnMouseDown()
    //{
    //    StopIdleAnimation();
    //    gameObject.SetActive(false);
    //    carUnderneath.SetActive(true);
    //    Car tempCar = carUnderneath.GetComponent<Car>();
    //    if (j == m - 1)
    //        tempCar.Active();
    //    else
    //        tempCar.Inactive();
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        StopIdleAnimation();
        gameObject.SetActive(false);
        carUnderneath.SetActive(true);
        Car tempCar = carUnderneath.GetComponent<Car>();
        if (j == m - 1)
            tempCar.Active();
        else
            tempCar.Inactive();
    }

    IEnumerator Animation;

    public void StartAnimation()
    {
        Animation = LerpAnimation(0.1f);
        StartCoroutine(Animation);
    }

    IEnumerator LerpAnimation(float duration)
    {
        Vector3 start = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Vector3 finish = transform.position;
        transform.position = start;
        yield return PositionLerp(start, finish, duration);
        StartIdleAnimation();
    }

    public IEnumerator PositionLerp(Vector3 start, Vector3 finish, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 0.75f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(start, finish, i);
            yield return null;
        }
    }

    Vector3 originalScale;
    Vector3 minScale; Vector3 maxScale;
    IEnumerator StillAnimation;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void StartIdleAnimation()
    {
        StillAnimation = IdleAnimation(0.5f);
        StartCoroutine(StillAnimation);
    }

    public void StopIdleAnimation()
    {
        StopCoroutine(StillAnimation);
        transform.localScale = originalScale;
    }

    IEnumerator IdleAnimation(float duration)
    {
        while (true)
        {
            minScale = originalScale;
            maxScale = new Vector3(minScale.x + 0.05f, minScale.y + 0.05f, minScale.z);
            yield return ScaleLerp(minScale, maxScale, duration);
            yield return ScaleLerp(maxScale, minScale, duration);

            transform.localScale = originalScale;
        }
    }

    public IEnumerator ScaleLerp(Vector3 start, Vector3 finish, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * 0.5f;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(start, finish, i);
            yield return null;
        }
    }
}
