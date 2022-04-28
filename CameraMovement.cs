using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject everyThingManager;
    private EverythingManager everyThingManagerScript;
    public float cameraMovementSpeed = 0.20f;
    public float cameraScrollSpeed = 1.0f;
    private float cameraFastMovementSpeed;
    private float cameraMovementSpeedHolder;
    public float cameraX = 0;
    public float cameraY = 0;
    public float cameraZ = -10;
    public Camera camera;
    // Update is called once per frame
    void Start()
    {
        everyThingManagerScript = everyThingManager.GetComponent<EverythingManager>();
        this.cameraFastMovementSpeed = cameraMovementSpeed * 2;
        this.cameraMovementSpeedHolder = cameraMovementSpeed;
        camera.transform.position = new Vector3(everyThingManagerScript.width / 2, everyThingManagerScript.height / 2, -10);
    }

    void Update()
    {
        MoveFaster();
        MoveUp();
        MoveDown();
        MoveLeft();
        MoveRight();
        MoveFoward();
        MoveBack();
    }

    private void MoveFaster()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            cameraMovementSpeed = cameraFastMovementSpeed;
        }
        else
        {
            cameraMovementSpeed = cameraMovementSpeedHolder;
        }
        
    }

    private void MoveBack()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            camera.orthographicSize = camera.orthographicSize + cameraScrollSpeed;
        }
        if (camera.orthographicSize >= 80)
        {
            camera.orthographicSize = 80;
        }
    }

    private void MoveFoward()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            camera.orthographicSize = camera.orthographicSize - cameraScrollSpeed;
        }
        if (camera.orthographicSize <= 2)
        {
            camera.orthographicSize = 2;
        }
    }

    private void MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(cameraX + cameraMovementSpeed, cameraY, cameraZ));
        }
        
    }

    private void MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(cameraX - cameraMovementSpeed, cameraY, cameraZ));
        }
        
    }

    private void MoveDown()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(cameraX, cameraY - cameraMovementSpeed, cameraZ));
        }
        
    }

    private void MoveUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(cameraX, cameraY + cameraMovementSpeed, cameraZ));
        }
        
    }
}
