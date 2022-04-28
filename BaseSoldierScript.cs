using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSoldierScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ArmyManager currentArmy;
    public string race;
    public string faction;
    public float weaponSkill;
    public float vehicleSkill;
    public float health;

    void Start()
    {
        this.health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeaponSkillSet(float weaponSkill)
    {
        this.weaponSkill =+ weaponSkill;
    }

    public void VehicleSkillSet(float vehicleSkill)
    {
        this.vehicleSkill =+ vehicleSkill;
    }
}
