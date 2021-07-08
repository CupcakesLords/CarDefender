using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slots : MonoBehaviour
{
    float lowestX; float lowestY; float stepX; float stepY;
    int n; int m;
    GameObject[,] slot;

    private float spawnSpeed;
    private float spawnSpeedTimer;

    private float attackSpeed;
    private float attackSpeedTimer;

    public GameObject[] cars;
    public GameObject tile;

    private void Initialize()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                GameObject temp = Instantiate(tile, new Vector3(lowestX + (i * stepX), lowestY + (j * stepY), 0), Quaternion.identity);
            }
        }
    }

    void Start()
    {
        //n = 3; m = 3;
        //lowestX = -1.5f; lowestY = -4f; stepX = 1.5f; stepY = 2f;
        n = 4; m = 4;
        lowestX = -1.5f; lowestY = -3f; stepX = 1f; stepY = 1f;
        slot = new GameObject[n, m];
        Initialize();

        spawnSpeed = 5f;
        spawnSpeedTimer = spawnSpeed;

        attackSpeed = 1f;
        attackSpeedTimer = attackSpeed;
    }

    void Update()
    {
        if (spawnSpeedTimer > 0)
        {
            spawnSpeedTimer -= Time.deltaTime;
        }
        else
        {
            Spawn();
            spawnSpeedTimer = spawnSpeed;
        }
        if (attackSpeedTimer > 0)
        {
            attackSpeedTimer -= Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if(slot[i, j] != null)
                    {
                        slot[i, j].GetComponent<Car>().Attack();
                    }
                }
            }
            attackSpeedTimer = attackSpeed;
        }
    }

    bool CheckIfFull()
    {
        bool result = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (slot[i, j] == null)
                {
                    result = false;
                }
            }
        }
        return result;
    }

    void Spawn()
    {
        if (CheckIfFull())
            return;

        System.Random rand = new System.Random();
        int i; int j;
        do
        {
            i = rand.Next(0, n);
            j = rand.Next(0, m);
        } while (slot[i, j] != null);

        //int k = rand.Next(0, cars.Length);
        GameObject temp = Instantiate(cars[0], new Vector3(lowestX + (i * stepX), lowestY + (j * stepY), 0), Quaternion.identity);

        Car tempCar = temp.GetComponent<Car>(); //if created in the front row, set active
        if (j == m - 1)
            tempCar.Active();
        else
            tempCar.Inactive();

        ItemDrag tempDrag = temp.GetComponent<ItemDrag>();
        tempDrag.SetPositionInSlot(i, j, new Vector3(lowestX + (i * stepX), lowestY + (j * stepY), 0));

        slot[i, j] = temp;
    }

    public int CheckForMerge(Vector2 mousePos, int itemDragX, int itemDragY, Vector3 itemDragPos)
    {
        int itemMergeX = (int)(System.Math.Round((mousePos.x - lowestX) / stepX));
        int itemMergeY = (int)(System.Math.Round((mousePos.y - lowestY) / stepY));

        if (itemDragX == itemMergeX && itemDragY == itemMergeY)
        {   //DRAG INTO THE SAME POSITION
            //Debug.Log("Merge Item [" + itemDragX + "][" + itemDragY + "] into [" + itemMergeX + "][" + itemMergeY + "]");
            //Debug.Log("Drag into the same pos");
            return 0;
        }
        if (itemMergeX < 0 || itemMergeX >= n || itemMergeY < 0 || itemMergeY >= m)
        {   //DRAG OUTSIDE THE SLOT
            //Debug.Log("Merge Item [" + itemDragX + "][" + itemDragY + "] into [" + itemMergeX + "][" + itemMergeY + "]");
            //Debug.Log("Drag outside the slot");
            return 0;
        }
        if (slot[itemMergeX, itemMergeY] == null)
        {   //DRAG INTO A POSSIBLE SPACE BUT IT'S EMPTY => SWAP WITH EMPTY
            //Debug.Log("Merge Item [" + itemDragX + "][" + itemDragY + "] into [" + itemMergeX + "][" + itemMergeY + "]");
            //Debug.Log("Perform move to empty place");

            if (itemDragY == m - 1 && itemMergeY != m - 1) //set active and inactive when swap frontline and non-frontline car!
            {
                slot[itemDragX, itemDragY].GetComponent<Car>().Inactive();
            }
            else if (itemDragY != m - 1 && itemMergeY == m - 1)
            {
                slot[itemDragX, itemDragY].GetComponent<Car>().Active();
            }

            slot[itemDragX, itemDragY].transform.position = new Vector3(lowestX + (itemMergeX * stepX), lowestY + (itemMergeY * stepY), 0);
            ItemDrag tempDrag = slot[itemDragX, itemDragY].GetComponent<ItemDrag>();
            tempDrag.SetPositionInSlot(itemMergeX, itemMergeY, new Vector3(lowestX + (itemMergeX * stepX), lowestY + (itemMergeY * stepY), 0));

            slot[itemMergeX, itemMergeY] = slot[itemDragX, itemDragY];
            slot[itemDragX, itemDragY] = null;

            return 1;
        }
        if (slot[itemDragX, itemDragY].name == slot[itemMergeX, itemMergeY].name)     
        {   //DRAG INTO THE SAME CAR TYPE => MERGE
            //Debug.Log("Merge Item [" + itemDragX + "][" + itemDragY + "] into [" + itemMergeX + "][" + itemMergeY + "]");
            //Debug.Log("Perform merge!");

            string name = slot[itemDragX, itemDragY].GetComponent<SpriteRenderer>().sprite.name;
            int number = int.Parse(name.Substring(7));

            GameObject holder1 = slot[itemDragX, itemDragY]; Destroy(holder1); slot[itemDragX, itemDragY] = null;
            GameObject holder2 = slot[itemMergeX, itemMergeY]; Destroy(holder2); slot[itemMergeX, itemMergeY] = null;

            if (number < cars.Length)
            {
                GameObject mergedCar = Instantiate(cars[number], new Vector3(lowestX + (itemMergeX * stepX), lowestY + (itemMergeY * stepY), 0), Quaternion.identity);

                Car tempCar = mergedCar.GetComponent<Car>(); //if created in the front row, set active
                if (itemMergeY == m - 1)
                    tempCar.Active();
                else
                    tempCar.Inactive();

                ItemDrag tempDrag = mergedCar.GetComponent<ItemDrag>();
                tempDrag.SetPositionInSlot(itemMergeX, itemMergeY, new Vector3(lowestX + (itemMergeX * stepX), lowestY + (itemMergeY * stepY), 0));

                slot[itemMergeX, itemMergeY] = mergedCar;
            }

            return 1;
        }
        else
        {   //DRAG INTO A DIFFERENT CAR TYPE => SWAP
            //Debug.Log("Merge Item [" + itemDragX + "][" + itemDragY + "] into [" + itemMergeX + "][" + itemMergeY + "]");
            //Debug.Log("Perform swap!");

            if (itemDragY == m - 1 && itemMergeY != m - 1) //set active and inactive when swap frontline and non-frontline car!
            {
                slot[itemDragX, itemDragY].GetComponent<Car>().Inactive();
                slot[itemMergeX, itemMergeY].GetComponent<Car>().Active();
            }
            else if (itemDragY != m - 1 && itemMergeY == m - 1)
            {
                slot[itemDragX, itemDragY].GetComponent<Car>().Active();
                slot[itemMergeX, itemMergeY].GetComponent<Car>().Inactive();
            }

            slot[itemDragX, itemDragY].transform.position = slot[itemMergeX, itemMergeY].transform.position;
            slot[itemMergeX, itemMergeY].transform.position = itemDragPos;

            ItemDrag tempDrag = slot[itemDragX, itemDragY].GetComponent<ItemDrag>();
            tempDrag.SetPositionInSlot(itemMergeX, itemMergeY, new Vector3(lowestX + (itemMergeX * stepX), lowestY + (itemMergeY * stepY), 0));

            ItemDrag tempDrag1 = slot[itemMergeX, itemMergeY].GetComponent<ItemDrag>();
            tempDrag1.SetPositionInSlot(itemDragX, itemDragY, new Vector3(lowestX + (itemDragX * stepX), lowestY + (itemDragY * stepY), 0));

            GameObject holder = slot[itemMergeX, itemMergeY];
            slot[itemMergeX, itemMergeY] = slot[itemDragX, itemDragY];
            slot[itemDragX, itemDragY] = holder;

            return 1;
        }
    }

    public bool CheckIfFrontRow(int j)
    {
        return j == m - 1;
    }
}
