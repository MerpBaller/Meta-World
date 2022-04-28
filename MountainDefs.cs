using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainDefs
{
    private string name;
    private float health;
    private float rarity;
    private float sizeMax;
    private string dropObject;
    private float minDropAmount;
    private float maxDropAmount;

    public MountainDefs()
    {

    }

    public MountainDefs(string name, float health, float rarity, float sizeMax, string dropObject, float minDropAmount, float maxDropAmount)
    {
        this.name = name;
        this.health = health;
        this.rarity = rarity;
        this.sizeMax = sizeMax;
        this.dropObject = dropObject;
        this.minDropAmount = minDropAmount;
        this.maxDropAmount = maxDropAmount;
    }

    public string getName()
    {
        return name;
    }

    public float getHealth()
    {
        return health;
    }

    public float getRarity()
    {
        return rarity;
    }

    public string getDropObject()
    {
        return dropObject;
    }

    public float getMinDropAmount()
    {
        return minDropAmount;
    }

    public float getMaxDropAmount()
    {
        return maxDropAmount;
    }

    public float getSizeMax()
    {
        return sizeMax;
    }
}
