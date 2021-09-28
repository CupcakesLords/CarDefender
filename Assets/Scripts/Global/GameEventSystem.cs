using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem eventSystem;
    private void Awake()
    {
        eventSystem = this;
    }

    public Func<int, int> onGameLost;

    public int GameLost(int number)
    {
        if (onGameLost != null)
        {
            return onGameLost(number);
        }
        return 0;
    }

    public Func<int, int> onLevelUp;

    public int LevelUp(int level)
    {
        if (onLevelUp != null)
        {
            return onLevelUp(level);
        }
        return 0;
    }

    public Func<GameObject, int> onCarAdded;

    public int CarAdded(GameObject car)
    {
        if (onCarAdded != null)
        {
            return onCarAdded(car);
        }
        return 0;
    }

    public Func<GameObject, int> onCarRemoved;

    public int CarRemoved(GameObject car)
    {
        if (onCarRemoved != null)
        {
            return onCarRemoved(car);
        }
        return 0;
    }

    public Func<int, int> onUpgradeDPS;

    public int UpgradeDPS(int index)
    {
        if (onUpgradeDPS != null)
        {
            return onUpgradeDPS(index);
        }
        return 0;
    }

    public Func<int, int, int> onUpgradeMPS;

    public int UpgradeMPS(int index, int increase)
    {
        if (onUpgradeMPS != null)
        {
            return onUpgradeMPS(index, increase);
        }
        return 0;
    }

    public Func<int, int> onGameSave;

    public int GameSave(int data)
    {
        if (onGameSave != null)
        {
            return onGameSave(data);
        }
        return 0;
    }
}
