using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinates
{
    public int xPosition;
    public int yPosition;
    public bool walkable;
    public double g;
    public double h;
    public double f;
    public Coordinates previous;

    public Coordinates()
    {

    }

    public Coordinates(int x, int y, bool walkable)
    {
        this.xPosition = x;
        this.yPosition = y;
        this.walkable = walkable;
    }

    public double fCost
    {
        get
        {
            return g + h;
        }
    }
}
