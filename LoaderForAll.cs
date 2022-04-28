using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.Tilemaps;

public class LoaderForAll
{
    public List<Biome> biomesList;
    public List<Tree> treeList;
    public List<MountainDefs> mountainPieceList;
    public List<BottomBarUI> bottomBarUIList;


    public LoaderForAll()
    {
        this.biomesList = new List<Biome>();
        this.treeList = new List<Tree>();
        this.mountainPieceList = new List<MountainDefs>();
        this.bottomBarUIList = new List<BottomBarUI>();
    }

    public List<Biome> LoadBiomes()
    {
        XmlReader reader = XmlReader.Create("Assets/Resources/Biomes.xml");
        string tempName = "";
        string tempTile = "";
        float temptextureleanFloat = 0;
        string tree = "";
        float treeFrequnecy = 0;
        while (reader.Read())
        {
            if((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Biome"))
            {
                tempName = reader.GetAttribute("name");
                tempTile = reader.GetAttribute("tile");
                temptextureleanFloat = float.Parse(reader.GetAttribute("textureLean"));
                tree = reader.GetAttribute("tree");
                treeFrequnecy = float.Parse(reader.GetAttribute("treeFrequency"));
                Biome tempBiome = new Biome(tempName, tempTile, temptextureleanFloat, tree, treeFrequnecy);
                biomesList.Add(tempBiome);
            }
        }
        return biomesList;
    }

    public List<Tree> LoadTrees()
    {
        XmlReader reader = XmlReader.Create("Assets/Resources/Tree.xml");
        string treeName = "";
        float woodDropAmount = 0;
        float daysTillFullGrown = 0;
        int stages = 0;

        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Tree"))
            {
                treeName = reader.GetAttribute("name");
                woodDropAmount = float.Parse(reader.GetAttribute("woodDropAmount"));
                daysTillFullGrown = float.Parse(reader.GetAttribute("daysTillFullGrown"));
                stages = int.Parse(reader.GetAttribute("stages"));
                Tree tempTree = new Tree(treeName, woodDropAmount, daysTillFullGrown, stages);
                treeList.Add(tempTree);
            }
        }
        return treeList;
    }

    public List<MountainDefs> LoadMountains()
    {
        XmlReader reader = XmlReader.Create("Assets/Resources/Mountain.xml");
        string name = "";
        float health = 0;
        float rarity = 0;
        float sizeMax = 0;
        string dropObject = "";
        float minDropAmount = 0;
        float maxDropAmount = 0;


        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Mountain"))
            {
                name = reader.GetAttribute("name");
                health = float.Parse(reader.GetAttribute("health"));
                rarity = float.Parse(reader.GetAttribute("rarity"));
                sizeMax = float.Parse(reader.GetAttribute("sizeMax"));
                dropObject = reader.GetAttribute("dropObject");
                minDropAmount = float.Parse(reader.GetAttribute("minDropAmount"));
                maxDropAmount = float.Parse(reader.GetAttribute("maxDropAmount"));
                MountainDefs mountainPiece = new MountainDefs(name, health, rarity, sizeMax, dropObject, minDropAmount, maxDropAmount);
                mountainPieceList.Add(mountainPiece);
            }
        }
        return mountainPieceList;
    }

    public List<BottomBarUI> LoadBottomBarUI()
    {
        XmlReader reader = XmlReader.Create("Assets/Resources/BottomBarUI.xml");
        string name = "";
    
        while (reader.Read())
        {
            if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "UI"))
            {
                name = reader.GetAttribute("name");
                BottomBarUI bottomBarPiece = new BottomBarUI(name);
                bottomBarUIList.Add(bottomBarPiece);
            }
        }
        return bottomBarUIList;
    }
}
