using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tree
{
    private string treeName;

    private float woodDropAmount;

    private float daysTillFullGrown;

    private int stages;

    public Tree()
    {

    }

    public Tree(string treeName, float woodDropAmount, float daysTillFullGrown, int stages)
    {
        this.treeName = treeName;

        this.woodDropAmount = woodDropAmount;

        this.daysTillFullGrown = daysTillFullGrown;

        this.stages = stages;
    }

    public string getTreeName()
    {
        return treeName;
    }

    public float getWoodDropAmount()
    {
        return woodDropAmount;
    }

    public float getDaysTillFullGrown()
    {
        return daysTillFullGrown;
    }

    public int getStages()
    {
        return stages;
    }

    public void setTreeName(string treeName)
    {
        this.treeName = treeName;
    }

    public void setWoodDropAmount(float woodDropAmount)
    {
        this.woodDropAmount = woodDropAmount;
    }

    public void setDaysTillFullGrown(float daysTillFullGrown)
    {
        this.daysTillFullGrown = daysTillFullGrown;
    }

    public void setStages(int stages)
    {
        this.stages = stages;
    }
}
