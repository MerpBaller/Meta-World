using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MoveCommand : MonoBehaviour
{
    public List<Coordinates> coordinates = new List<Coordinates>();
    private Ray ray;
    private Vector3 tempVec;
    private Vector3 vector3;
    private Vector2 tempCurrentVec;
    private int mousePositionX;
    private int mousePositionY;
    public float moveSpeed = 100f;
    public GameObject AImanager;
    public GameObject redBlock;
    private Vector3 currentPosition;
    private Vector3 positionOfCoordinate;
    private int currentWayPoint = 0;
    private Coordinates targetWayPoint;
    public GameObject Paul;
    float tempFloatX;
    float tempFloatY;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Mouse0))
       {
            coordinates = GetMoveOrder();
            foreach(Coordinates coordinateNode in coordinates)
            {
                    Instantiate(redBlock);
                    redBlock.transform.position = new Vector3(coordinateNode.xPosition + 0.5f, coordinateNode.yPosition + 0.5f, 0f);
            }
            
            
       }
       if (currentWayPoint < coordinates.Count)
       {
            if (targetWayPoint == null)
            {
                tempVec = new Vector3(coordinates[currentWayPoint].xPosition, coordinates[currentWayPoint].yPosition, 0);
            }
            Walk();
       }
        else
        {
            coordinates.Clear();
            currentWayPoint = 0;
        }
       
    }

    private void Walk()
    {

        // move towards the target
        Debug.Log(tempVec.x + " " + tempVec.y);
        Debug.Log(Paul.transform.position.x + " " + Paul.transform.position.y);
        Paul.transform.position = Vector3.MoveTowards(Paul.transform.position, tempVec, moveSpeed * Time.deltaTime);

        tempFloatX = Paul.transform.position.x;
        tempFloatY = Paul.transform.position.y;

        if (tempFloatX == tempVec.x && tempFloatY == tempVec.y &&  currentWayPoint != coordinates.Count)
        {
            currentWayPoint++;
            Debug.Log(currentWayPoint);
            try
            {
                tempVec = new Vector3(coordinates[currentWayPoint].xPosition, coordinates[currentWayPoint].yPosition);
            }
            catch (ArgumentOutOfRangeException)
            {
                
            }
        }
    }


    public List<Coordinates> GetMoveOrder()
    {
        int xDiff;
        int yDiff;
        Debug.Log("Move Command Enabled");
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        vector3 = ray.origin;
        mousePositionX = Mathf.RoundToInt(vector3.x);
        mousePositionY = Mathf.RoundToInt(vector3.y);
        BaseAIManager baseAI = AImanager.GetComponent<BaseAIManager>();
        coordinates = baseAI.GetPath(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), mousePositionX, mousePositionY);
        return coordinates;

        /*
        foreach(Coordinates coordinateNode in coordinates)
        {
            try
            {
                moveSpeed = 0.1f * Time.deltaTime;
                Instantiate(redBlock);
                redBlock.transform.position = new Vector3(coordinateNode.xPosition + 0.5f, coordinateNode.yPosition + 0.5f, 0f);

                return positionOfCoordinate = new Vector3(coordinateNode.xPosition, coordinateNode.yPosition, 0);
                /*
                currentPosition = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y), 0);
                while(positionOfCoordinate != currentPosition && counter != 1000)
                {
                    transform.position = Vector3.MoveTowards(currentPosition, positionOfCoordinate, moveSpeed);
                    counter++;
                }
                if(counter == 1000)
                {
                    Debug.Log("Hit 1000");
                }
                */


        /*
        xDiff = coordinateNode.xPosition - Mathf.RoundToInt(this.transform.position.x);
        yDiff = coordinateNode.yPosition - Mathf.RoundToInt(this.transform.position.y);

        if(xDiff >= 1)
        {
            xDiff = 1;
        }
        else if(xDiff <= -1)
        {
            xDiff = -1;
        }

        if (yDiff >= 1)
        {
            yDiff = 1;
        }
        else if (yDiff <= -1)
        {
            yDiff = -1;
        }

        Debug.Log(xDiff);
        Debug.Log(yDiff);

        while (transform.position.x != coordinateNode.xPosition && transform.position.y != coordinateNode.yPosition && counter != 10000)
        {
            switch (xDiff)
            {
                case 1:
                    switch (yDiff)
                    {
                        case 1:
                            transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
                            transform.Translate(Vector3.up * Time.deltaTime, Space.Self);
                            break;

                        case 0:
                            transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
                            break;

                        case -1:
                            transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
                            transform.Translate(Vector3.down * Time.deltaTime, Space.Self);
                            break;
                    }
                    break;

                case 0:
                    switch (yDiff)
                    {
                        case 1:
                            transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
                            break;

                        case -1:
                            transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
                            break;
                    }
                    break;

                case -1:
                    switch (yDiff)
                    {
                        case 1:
                            transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
                            transform.Translate(Vector3.up * Time.deltaTime, Space.Self);
                            break;

                        case 0:
                            transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
                            break;

                        case -1:
                            transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
                            transform.Translate(Vector3.down * Time.deltaTime, Space.Self);
                            break;
                    }
                    break;
            }
            counter++;
        }
        */
    }

    public void DirectionMove()
    {

    }
}
