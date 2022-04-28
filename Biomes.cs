using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Biome
{
    // Start is called before the first frame update
    public string biomeName;

    public string tile;

    public float textureLean;

    public string tree;

    public float treeFrequency;

    public Biome()
    {

    }

    public Biome(string biomeName, string tile, float textureLean, string tree, float treeFrequency)
    {
        this.biomeName = biomeName;

        this.tile = tile;

        this.textureLean = textureLean;

        this.tree = tree;

        this.treeFrequency = treeFrequency;
    }

    public string getBiomeName()
    {
       return biomeName;
    }

    public string getTile()
    {
        return tile;
    }

    public float getTextrueLean()
    {
        return textureLean;
    }

    public string getTree()
    {
        return tree;
    }

    public float getTreeFrequency()
    {
        return treeFrequency;
    }

}
