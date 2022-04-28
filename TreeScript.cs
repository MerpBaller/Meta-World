using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public string treeName;
    public float woodDropAmount;
    public GameObject clock;

    private Timer timer;
    private float treeStageDivider;
    private int currentGrowthDate;
    private float currentGrowthHour;
    private float hoursTillGrown;
    private int currentStage;
    private int stageHolder;

    // Start is called before the first frame update

    // Update is called once per frame
    void OnEnable()
    {
        InvokeRepeating("increaseCurrentHour", 0f, 60f);
    }

    public void SetTree(string treeName, float daysTillFullGrown, float woodDropAmount, int stages)
    {
        this.treeName = treeName;
        this.treeStageDivider = daysTillFullGrown / stages;
        this.currentGrowthDate = (int)Mathf.Round(UnityEngine.Random.Range(treeStageDivider, daysTillFullGrown));
        this.currentGrowthHour = currentGrowthDate * 20;
        this.woodDropAmount = woodDropAmount;
        this.currentStage = (int)Mathf.Round(currentGrowthDate / treeStageDivider);
        //this.currentStage = UnityEngine.Random.Range(1, stages+1);
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + treeName + " Stage " + currentStage.ToString());
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        timer = clock.GetComponent<Timer>();
        
        enabled = true;
    }

    public void increaseCurrentHour()
    {
        currentGrowthHour++;
        if(currentGrowthHour % 20 == 0)
        {
            currentGrowthDate++;
            stageHolder = (int)Mathf.Round(currentGrowthDate / treeStageDivider);
            if(stageHolder != currentStage)
            {
                currentStage++;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/" + treeName + " Stage " + currentStage.ToString());
                Debug.Log("I Grew UP!");
            }
        }
        
    }
    
}
