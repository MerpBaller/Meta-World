using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using System.Xml;

public class EverythingManager : MonoBehaviour
{
    public int width = 64;
    public int height = 64;
    private int depth = 0;

    private float soundScale = .05f;

    public Tilemap surface;
    public Tile walkableTile;
    public Tile notWalkableTile;
    //TODO Change this to an xml type thing
    public Sprite grass;
    public Sprite mountain;
    public Sprite rockAndMountain;
    public Sprite sand;
    public Sprite treeSprite;
    public Sprite red;
    public Sprite green;
    public GameObject terrain;
    public GameObject tree;
    public GameObject mountainPieceObject;
    public GameObject fogOfWar;
    public GameObject noWalkBox;
    public GameObject walkBox;
    private Sprite baseSprite;
    public GameObject AIManager;
    public GameObject MoveManager;

    public float[,] noiseMap;
    public bool[,] mountainMap;
    public bool[,] currentObjectMap;
    public int[,] currentMountainMap;

    public Biome biome;
    private Tree baseTree;
    private MountainDefs mountainPiece;
    public List<Biome> biomesList = new List<Biome>();
    public List<Tree> treeList = new List<Tree>();
    public List<MountainDefs> mountainPieceList = new List<MountainDefs>();


    void Start()
    {
        LoaderForAll loader = new LoaderForAll();
        this.biomesList = loader.LoadBiomes();
        this.treeList = loader.LoadTrees();
        this.mountainPieceList = loader.LoadMountains();
        GenerateNoiseMap();
        GenerateTiles();
        GenerateTrees();
        GenerateMountains();
        GenerateNavMesh();
    }

    

    public void GenerateNoiseMap()
    {
        noiseMap = new float[width, height];
        float offsetX = UnityEngine.Random.Range(0, 1000);
        float offsetY = UnityEngine.Random.Range(0, 1000);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float sample = Mathf.PerlinNoise(((x + offsetX) * soundScale), ((y + offsetY) * soundScale));
                noiseMap[x, y] = sample;
            }
        }
    }

    public void GenerateTiles()
    {
        Tile tile = walkableTile;
        Vector3Int vectorTemp;
        mountainMap = new bool[width, height];
        currentMountainMap = new int[width, height];
        currentObjectMap = new bool[width, height];
        biome = GetBiome("Hilly Plains");
        switch (biome.getTile())
        {
            case "Sand":
                baseSprite = sand;
                break;
            case "Grass":
                baseSprite = grass;
                break;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                vectorTemp = new Vector3Int(x, y, depth);
                float temp = noiseMap[x, y];
                if (temp < 0.3f * biome.textureLean)
                {
                    tile = notWalkableTile;
                    tile.sprite = mountain;
                    mountainMap[x, y] = true;
                    currentMountainMap[x, y] = 1;
                }
                else
                {
                    mountainMap[x, y] = false;
                    currentMountainMap[x, y] = 0;
                }

                if (temp >= 0.3f * biome.textureLean && noiseMap[x, y] <= 0.4f * biome.textureLean)
                {
                    tile = walkableTile;
                    tile.sprite = rockAndMountain;
                }
                if (temp > 0.4f * biome.textureLean)
                {
                    tile = walkableTile;
                    tile.sprite = baseSprite;
                }
                surface.SetTile(vectorTemp, tile);
            }
        }
    }

    public void GenerateTrees()
    {
        TreeScript treeScript;
        Vector3Int tempVec;
        Sprite tempSprite;
        Tile tempTile;
        baseTree = GetTree(biome.getTree());
        float treeFrequency = biome.getTreeFrequency();
        float randomTemp;
        float xFloat;
        float yFloat;
        if (baseTree.getTreeName() != "none")
        {
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    tempVec = new Vector3Int(x, y, depth);
                    tempTile = surface.GetTile<Tile>(tempVec);
                    tempSprite = tempTile.sprite;
                    if (tempSprite == baseSprite)
                    {
                        randomTemp = UnityEngine.Random.Range(0, treeFrequency);
                        if(randomTemp <= 10)
                        {
                            tempTile.gameObject = Instantiate(tree);
                            treeScript = tree.GetComponent<TreeScript>();
                            treeScript.SetTree(baseTree.getTreeName(), baseTree.getDaysTillFullGrown(), baseTree.getWoodDropAmount(), baseTree.getStages());
                            xFloat = x;
                            yFloat = y;
                            tree.transform.position = new Vector3(xFloat + 0.5f, yFloat + 0.5f, depth + 1);
                        }
                    }
                }
            }
        }
    }

    public void GenerateMountains()
    {
        MountainScript mountainScript;
        bool foundPiece = true;
        float sizeOfList;
        int randomNumber;
        sizeOfList =  mountainPieceList.Count;
        float xFloat;
        float yFloat;
        Tile tempTile;
        Vector3Int tempVec;
        int[] tileNumberandRotation = new int[2];
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(mountainMap[x,y] == true)
                {
                    foundPiece = false;
                    while(foundPiece == false)
                    {
                        randomNumber = (int)Mathf.Round(UnityEngine.Random.Range(0f, sizeOfList-1));
                        mountainPiece = mountainPieceList[randomNumber];
                        randomNumber = (int)Mathf.Round(UnityEngine.Random.Range(1f, mountainPiece.getRarity() + 1));
                        if(randomNumber == 1)
                        {
                            randomNumber = (int)Mathf.Round(UnityEngine.Random.Range(1, mountainPiece.getSizeMax()));
                            for(int xSub = x - randomNumber; xSub < x + randomNumber; xSub++)
                            {
                                for (int ySub = y - randomNumber; ySub < y + randomNumber; ySub++)
                                {
                                    try
                                    { 
                                        if (mountainMap[xSub, ySub] == true)
                                        {
                                        xFloat = xSub;
                                        yFloat = ySub;
                                        tempVec = new Vector3Int(xSub, ySub, depth);
                                        tempTile = surface.GetTile<Tile>(tempVec);
                                        tempTile.gameObject = Instantiate(mountainPieceObject);
                                        currentObjectMap[xSub, ySub] = true;
                                        mountainScript = mountainPieceObject.GetComponent<MountainScript>();
                                        tileNumberandRotation = getNumberAndRotation(xSub, ySub, tileNumberandRotation);
                                        mountainScript.setMountain(mountainPiece.getName(), mountainPiece.getHealth(), mountainPiece.getDropObject(), mountainPiece.getMinDropAmount(), mountainPiece.getMaxDropAmount(), tileNumberandRotation[0]);
                                        mountainPieceObject.transform.position = new Vector3(xFloat + .5f, yFloat + .5f, depth + 1);
                                        mountainPieceObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,tileNumberandRotation[1]));
                                            if (tileNumberandRotation[0] == 5)
                                            {
                                                Instantiate(fogOfWar);
                                                fogOfWar.transform.position = new Vector3(xFloat + 0.5f, yFloat + 0.5f, depth + 1);
                                                fogOfWar.GetComponent<SpriteRenderer>().sortingOrder = 3;
                                            }
                                        mountainMap[xSub, ySub] = false;
                                        }
                                        
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        ySub++;
                                    }
                                }
                            }
                            foundPiece = true;
                        }
                    }
                }
            }
        }                
    }

    public void GenerateNavMesh()
    {
        float iSub;
        float jSub;
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                
                    if (currentObjectMap[i, j] == true)
                    {
                        iSub = i;
                        jSub = j;
                        Instantiate(noWalkBox);
                        noWalkBox.transform.position = new Vector3(iSub + 0.5f, jSub + 0.5f, depth + 1);
                        noWalkBox.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    }
                    else
                    {
                        iSub = i;
                        jSub = j;
                        Instantiate(walkBox);
                        walkBox.transform.position = new Vector3(iSub + 0.5f, jSub + 0.5f, depth + 1);
                        walkBox.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    }
                
                
            }
        }
        AIManager.SetActive(true);
        MoveManager.SetActive(true);

    }

    public int[] getNumberAndRotation(int xSub, int ySub, int[] tileNumberandRotation)
    {
        string currentNearbyPieces = "";
        try
        {
            if (currentMountainMap[xSub + 1, ySub] == 1)
            {
                currentNearbyPieces += "1";
            }
            else
            {
                currentNearbyPieces += "0";
            }
        }
        catch (IndexOutOfRangeException)
        {
            currentNearbyPieces += "1";
        }

        try
        {
            if (currentMountainMap[xSub, ySub - 1] == 1)
            {
                currentNearbyPieces += "1";
            }
            else
            {
                currentNearbyPieces += "0";
            }
        }
        catch (IndexOutOfRangeException)
        {
            currentNearbyPieces += "1";
        }

        try
        {

            if (currentMountainMap[xSub - 1, ySub] == 1)
            {
                currentNearbyPieces += "1";
            }
            else
            {
                currentNearbyPieces += "0";
            }
        }
        catch (IndexOutOfRangeException)
        {
            currentNearbyPieces += "1";
        }


        try
        {
            if (currentMountainMap[xSub, ySub + 1] == 1)
            {
                currentNearbyPieces += "1";
            }
            else
            {
                currentNearbyPieces += "0";
            }
        }
        catch (IndexOutOfRangeException)
        {
            currentNearbyPieces += "1";
        }


        switch (currentNearbyPieces)
            {
                case "0000":
                    tileNumberandRotation[0] = 7;
                    tileNumberandRotation[1] = 0;
                    break;
                
                //works
                case "0001":
                    tileNumberandRotation[0] = 3;
                    tileNumberandRotation[1] = -90;
                    break;

                case "0011":
                    tileNumberandRotation[0] = 10;
                    tileNumberandRotation[1] = 0;
                    break;

                case "0111":
                    tileNumberandRotation[0] = 6;
                    tileNumberandRotation[1] = 0;
                    break;

                case "1111":
                    tileNumberandRotation[0] = 5;
                    tileNumberandRotation[1] = 0;
                    break;

                //works
                case "0010":
                    tileNumberandRotation[0] = 3;
                    tileNumberandRotation[1] = 0;
                    break;

                case "0110":
                    tileNumberandRotation[0] = 2;
                    tileNumberandRotation[1] = 0;
                    break;

                case "1110":
                    tileNumberandRotation[0] = 1;
                    tileNumberandRotation[1] = 0;
                    break;

                //work
                case "0100":
                    tileNumberandRotation[0] = 3;
                    tileNumberandRotation[1] = 90;
                    break;

                case "1100":
                    tileNumberandRotation[0] = 0;
                    tileNumberandRotation[1] = 0;
                    break;

                //works
                case "1000":
                    tileNumberandRotation[0] = 3;
                    tileNumberandRotation[1] = 180;
                    break;

                case "1101":
                    tileNumberandRotation[0] = 4;
                    tileNumberandRotation[1] = 0;
                    break;

                case "1011":
                    tileNumberandRotation[0] = 9;
                    tileNumberandRotation[1] = 0;
                    break;

                case "1001":
                    tileNumberandRotation[0] = 8;
                    tileNumberandRotation[1] = 0;
                    break;

                case "1010":
                    tileNumberandRotation[0] = 11;
                    tileNumberandRotation[1] = 0;
                    break;

                case "0101":
                    tileNumberandRotation[0] = 11;
                    tileNumberandRotation[1] = 270;
                    break;
         

        }

        return tileNumberandRotation;
    }

    public Biome GetBiome(string userInput)
    {
        Biome tempBiome = new Biome();
        foreach (Biome biome in biomesList)
        {
            if (biome.getBiomeName() == userInput)
            {
                tempBiome = biome;
            }
        }
        return tempBiome;
    }

    public Tree GetTree(string treeName)
    {
        Tree tempTree = new Tree();
        foreach (Tree tree in treeList)
        {
            if (tree.getTreeName() == treeName)
            {
                tempTree = tree;
            }
        }
        return tempTree;
    }

    public bool[,] GetCurrentObjectMap()
    {
        return currentObjectMap;
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }
}
