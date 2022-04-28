using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainScript : MonoBehaviour
{
    private float health;
    //TODO Create Materials
    //private GameObject Material;
    private string dropObject;
    private int dropAmount;

    private int currentHealth;
    private Sprite[] sprite;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    public void setMountain(string name, float health, string dropObject, float minDropAmount, float maxDropAmount, int tileNumber)
    {
        this.name = name;
        this.health = health;
        this.dropObject = dropObject;
        this.dropAmount = (int)Mathf.Round(UnityEngine.Random.Range(minDropAmount, maxDropAmount));
        sprite = Resources.LoadAll<Sprite>("Sprites/" + name);
        GetComponent<SpriteRenderer>().sprite = sprite[tileNumber];
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        enabled = true;
    }
}
