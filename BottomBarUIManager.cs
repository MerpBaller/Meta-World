using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomBarUIManager : MonoBehaviour
{
    public GameObject canvasObject;
    public RectTransform rectTransform;
    public GameObject button;
    private List<BottomBarUI> bottomBarUIList = new List<BottomBarUI>();
    private float xCanvasSize;
    private float buttonXSize;
    private float buttonYSize;
    private float buttonXPosition = 0;
    private float buttonYPosition = 0;
    // Start is called before the first frame update
    void Start()
    {
        LoaderForAll loaderForAll = new LoaderForAll();
        bottomBarUIList = loaderForAll.LoadBottomBarUI();
        rectTransform = canvasObject.GetComponent<RectTransform>();
        xCanvasSize = rectTransform.rect.width;
        buttonYSize = rectTransform.rect.height / 20;
        createButtons();
    }

    private void createButtons()
    {
        int listSize;
        listSize = bottomBarUIList.Count;
        this.buttonXSize = xCanvasSize / listSize;

        foreach (BottomBarUI bottomBarUI in bottomBarUIList)
        {

            button = Instantiate(button);
            button.transform.SetParent(canvasObject.transform);
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonXSize, buttonYSize);
            button.GetComponent<RectTransform>().transform.position = new Vector3(buttonXPosition, buttonYPosition, 0);
            button.GetComponentInChildren<Text>().text = bottomBarUI.getName();
            button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            buttonXPosition = buttonXPosition + buttonXSize;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
