using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void Awake()
    {
        CarIsIn = false;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    bool CarIsIn;

    public bool CheckIfCarIsIn()
    {
        return CarIsIn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Car>() != null)
        {
            CarIsIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Car>() != null)
        {
            CarIsIn = false;
        }
    }
}
