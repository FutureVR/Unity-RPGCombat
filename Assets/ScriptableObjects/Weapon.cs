using UnityEngine;
using System.Collections;

public class Weapon : ScriptableObject {

    public string weaponName;
    public GameObject damagerAnimation;

    [HideInInspector] public Person myPerson;
    //public GameObject myDamager;

    public enum attackStats { damage, accuracy, radius, lifetime }

    //The stats inherent to a weapon
    public float[] weaponStats = new float[4];

    //The amount by which each person stat is scaled when
    //Added to the stats of the Damager
    public float[] personStatMult = new float[3];

    public bool[] targetTypes = new bool[3];
    public bool AOE;

    public float staminaCost;
    public float manaCost;

    //Range is inherent to weapon, and not an aspect of the damager
    public float range;


    public virtual void initialize(Person p)
    {
        myPerson = p;
    }


    public virtual Damager spawnDamager(Vector3 spawnPoint)
    {
        //GameObject newObject = GameObject.Instantiate(myDamager, spawnPoint, Quaternion.identity) as GameObject;
        GameObject newObject = new GameObject();
        newObject.transform.position = spawnPoint;

        Damager newDamager = newObject.AddComponent<Damager>();
        newObject.AddComponent<DestroyIfOffScreen>();

        newDamager.animation = damagerAnimation;
        newDamager.user = myPerson;
        newDamager.targetTypes = targetTypes;
        newDamager.damage = weaponStats[(int)attackStats.damage] + myPerson.totalStats[(int)Person.Stats.might] * personStatMult[(int)attackStats.damage];
        //newDamager.accuracy = weaponStats[(int)attackStats.accuracy] + myPerson.totalStats[(int)Person.Stats.accuracy] * personStatMult[(int)attackStats.accuracy];
        newDamager.radius = weaponStats[(int)attackStats.radius] + myPerson.totalStats[(int)Person.Stats.intelligence] * personStatMult[(int)attackStats.radius];
        newDamager.AOE = AOE;
        newDamager.lifetime = weaponStats[(int)attackStats.lifetime];
        if (weaponStats[(int)attackStats.lifetime] == 0) newDamager.oneShot = true;
        else newDamager.oneShot = false;
        newDamager.isProjectile = false;
        
        return newDamager;
    }


    public virtual void use() { }

}
