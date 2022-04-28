using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class BaseAIManager : MonoBehaviour
{
    public List<Coordinates> coordinatesOpen;
    public List<Coordinates> coordinatesClosed;
    public List<Coordinates> children;
    public GameObject grid;
    private EverythingManager everythingManager;
    private bool[,] currentObjectMap;


    void OnEnable()
    {
        Debug.Log("Got Enabled");
    }

    public List<Coordinates> GetPath(int xStart, int yStart, int xEnd, int yEnd)
    {
        coordinatesOpen = new List<Coordinates>();
        coordinatesClosed = new List<Coordinates>();
        everythingManager = grid.GetComponent<EverythingManager>();
        currentObjectMap = everythingManager.GetCurrentObjectMap();

        Coordinates currentCoordinates;
        Coordinates startCoordinates = new Coordinates(xStart, yStart, currentObjectMap[xStart, yStart]);
        Coordinates endCoordinates = new Coordinates(xEnd, yEnd, currentObjectMap[xStart, yStart]);
        int counterTemp = 0;
        bool flag = false;

        Debug.Log("Got Path");

        coordinatesOpen.Add(startCoordinates);

        if(currentObjectMap[xEnd, yEnd] == true)
        {
            return coordinatesClosed;
        }

        while (coordinatesOpen.Count > 0)
        {
            currentCoordinates = coordinatesOpen[0];
            for(int i = 1; i < coordinatesOpen.Count; i++)
            {
                if(currentCoordinates.fCost > coordinatesOpen[i].fCost || coordinatesOpen[i].fCost == currentCoordinates.fCost)
                {
                    if(coordinatesOpen[i].h < currentCoordinates.h)
                    {
                        currentCoordinates = coordinatesOpen[i];
                    }
                }
            }

            coordinatesOpen.Remove(currentCoordinates);
            coordinatesClosed.Add(currentCoordinates);

            if (currentCoordinates.xPosition == endCoordinates.xPosition && currentCoordinates.yPosition == endCoordinates.yPosition)
            {
                endCoordinates.previous = currentCoordinates.previous;
                startCoordinates.previous = null;
                coordinatesClosed = RetraceCoordinates(coordinatesClosed, startCoordinates, endCoordinates);
                counterTemp = 0;
                return coordinatesClosed;
            }

            

            foreach (Coordinates coordinate in GetNeighbors(currentCoordinates))
            {
                flag = false;
                try
                {
                    if (currentObjectMap[coordinate.xPosition, coordinate.yPosition] == true)
                    {
                        continue;
                    }
                    else
                    {
                        foreach (Coordinates closedChild in coordinatesClosed)
                        {
                            if (closedChild.xPosition == coordinate.xPosition && closedChild.yPosition == coordinate.yPosition)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == true)
                        {
                            continue;
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }

                double newCostToNeighbor = currentCoordinates.g + getDistance(currentCoordinates, coordinate);
                if (newCostToNeighbor < coordinate.g)
                {
                    coordinate.g = newCostToNeighbor;
                    coordinate.h = getDistance(coordinate, endCoordinates);
                    coordinate.previous = currentCoordinates;
                    foreach (Coordinates openChild in coordinatesOpen)
                    {
                        if (openChild.xPosition == coordinate.xPosition && openChild.yPosition == coordinate.yPosition)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        continue;
                    }
                    else
                    {
                        coordinatesOpen.Add(coordinate);
                    }
                }
                else
                {
                    
                    foreach (Coordinates openChild in coordinatesOpen)
                    {
                        if (openChild.xPosition == coordinate.xPosition && openChild.yPosition == coordinate.yPosition)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true)
                    {
                        continue;
                    }
                    else
                    {
                        coordinate.g = newCostToNeighbor;
                        coordinate.h = getDistance(coordinate, endCoordinates);
                        coordinate.previous = currentCoordinates;
                        coordinatesOpen.Add(coordinate);
                    }
                }
                

            }
            

            counterTemp++;
        }
        return coordinatesClosed;
    }

    private List<Coordinates> RetraceCoordinates(List<Coordinates> closedList, Coordinates start, Coordinates end)
    {
        List<Coordinates> backTrackList = new List<Coordinates>();

        Coordinates currentCoordinate = end;

        foreach(Coordinates coordinate in closedList)
        {
            backTrackList.Add(currentCoordinate);
            try
            {
                currentCoordinate = currentCoordinate.previous;

            }
            catch(NullReferenceException)
            {
                continue;
            }
        }

        Debug.Log("Traced");

        backTrackList.Reverse();

        return backTrackList;
    }

    private double getDistance(Coordinates start, Coordinates end)
    {
        double xDistance;
        double yDistance;

        xDistance = Mathf.Abs(start.xPosition - end.xPosition);
        yDistance = Mathf.Abs(start.yPosition - end.yPosition);

        if(xDistance > yDistance)
        {
            return 14 * yDistance + 10 * (xDistance - yDistance);
        }

        return 14 * xDistance + 10 * (yDistance - xDistance);
    }

    private List<Coordinates> GetNeighbors(Coordinates currentCoordinates)
    {
        List<Coordinates> neighbors = new List<Coordinates>();
        Coordinates tempCoordinates;

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = currentCoordinates.xPosition + x;
                int checkY = currentCoordinates.yPosition + y;

                tempCoordinates = new Coordinates(checkX, checkY, currentObjectMap[checkX, checkY]);

                neighbors.Add(tempCoordinates);
            }
        }
        return neighbors;
    }

}
