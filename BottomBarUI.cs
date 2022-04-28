using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBarUI
{
    private string name;

    public BottomBarUI()
    {

    }

    public BottomBarUI(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return name;
    }
}
