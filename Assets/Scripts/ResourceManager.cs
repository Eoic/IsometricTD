using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance = null;
    public int Stone { get => stone; }
    public int Wood { get => wood; }
    public int Iron { get => iron; }

    [SerializeField] private int stone;
    [SerializeField] private int wood;
    [SerializeField] private int iron;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Inc.
    public void AddStone(int amount)
    {
        if (!IsValueValid(amount))
            return;

        stone += amount;
    }

    public void AddWood(int amount)
    {
        if (!IsValueValid(amount))
            return;

        wood += amount;
    }

    public void AddIron(int amount)
    {
        if (!IsValueValid(amount))
            return;

        iron += amount;
    }

    //Dec.
    public bool ConsumeStone(int amount)
    {
        if (stone - amount < 0)
            return false;

        stone -= amount;
        return true;
    }

    public bool ConsumeWood(int amount)
    {
        if (stone - amount < 0)
            return false;

        stone -= amount;
        return true;
    }

    public bool ConsumeIron(int amount)
    {
        if (stone - amount < 0)
            return false;

        stone -= amount;
        return true;
    }

    private bool IsValueValid(int amount)
    {
        if (amount <= 0)
            return false;

        return true;
    }
}
