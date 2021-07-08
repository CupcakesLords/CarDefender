using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    private bool isDragging;
    private Vector3 returnPos;
    private int i;
    private int j;

    public void SetPositionInSlot(int x, int y, Vector3 Pos)
    {
        i = x; j = y; returnPos = Pos;
    }

    public void OnMouseDown()
    {
        isDragging = true;
        GameObject slot = GameObject.Find("Slots");
        Slots temp = slot.GetComponent<Slots>();

        if (temp.CheckIfFrontRow(j) == false) //when hover a non-frontline car, set it active
        {
            gameObject.GetComponent<Car>().Active();
        }
    }

    public void OnMouseUp()
    {
        isDragging = false;

        GameObject slot = GameObject.Find("Slots");
        Slots temp = slot.GetComponent<Slots>();

        if (temp.CheckIfFrontRow(j) == false) //when unhover a non-frontline car, set it inactive
        {
            gameObject.GetComponent<Car>().Inactive();
        }

        int result = temp.CheckForMerge(Camera.main.ScreenToWorldPoint(Input.mousePosition), i, j, returnPos);

        if (result == 0) //no swap or merge 
        {
            transform.position = returnPos;
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}
