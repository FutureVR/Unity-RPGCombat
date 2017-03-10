using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Damager : MonoBehaviour {

    public Person user;
    public GameObject animation;

    public float damage;
    public float accuracy;
    public float radius;
    public float lifetime;

    public enum tar { self, allies, enemies }
    public bool[] targetTypes = new bool[3];
    public bool AOE;
    public bool oneShot;

    //Variables only for projectile case
    public bool isProjectile;

    public Sprite arrow;
    public Vector3 direction;
    public float speed;
    public float distance;


    //These functions are overriden in children
    public virtual void Start()
    {
        if (!isProjectile)
        {
            attack();
        }
        else
        {
            BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sprite = arrow;

            //Normalize direction vector
            Vector3.Normalize(direction);
        }
    }

    public virtual void Update()
    {
        if (isProjectile) gameObject.transform.position += direction * speed;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0 && !oneShot) die();
    }

    void OnCollisionEnter2D()
    {
        if (isProjectile)
        {
            attack();
            die();
        }
    }

    //Function that will be called either in Start or Update
    //Finds all targets and then calls applyDamage function on each of them
    public void attack()
    {
        //Debug.Log("Attacking");
        List<Person> people = findTargets();
        
        foreach(Person p in people)
        {
            applyDamage(p);
        }

        if (oneShot) die();
    }

    //Given the Damager's range, returns all targets in a certain radius
    //who also have the correct tag
    public List<Person> findTargets()
    {
        List<Person> targets = new List<Person>();

        Collider2D[] arrayTargets = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);

        //Add Collider2D from array to list if it has the correct tag
        foreach (Collider2D at in arrayTargets)
        {
            //TODO: MODIFY THIS HERE TO DIFFERENTIATE BETWEEN ALLIES AND ENEMIES AND SELF
            if (at.GetComponent<Person>() == true)
            {
                Person personToAdd = at.gameObject.GetComponent<Person>();

                if (personToAdd == user && targetTypes[(int)tar.self]) targets.Add(personToAdd);
                else if (personToAdd != user && personToAdd.alliance == user.alliance && targetTypes[(int)tar.allies]) targets.Add(personToAdd);
                else if (personToAdd.alliance != user.alliance && targetTypes[(int)tar.enemies]) targets.Add(personToAdd);
            }
        }

        return targets;
    }


    //Use the stats to determine whether the target is hit
    //If it determine how much damage is applied, and how
    //much is absorbed
    //This will call takeDamage function on target only 
    //if the attack misses; takeDamge in Person is very simple subtraction
    public void applyDamage(Person target)
    {
        //Change this to follow RPG rules
        //Use damage and accuracy
        target.takeDamage(2f);

        if (accuracy + Random.Range(0, 100) > target.totalStats[(int)Person.Stats.deflection])
        {
            //The player hits the enemy
            float appliedDamage = damage - target.totalStats[(int)Person.Stats.damageReduction];
            target.takeDamage(appliedDamage);
        }
    }

    public void die()
    {
        GameObject newAnim = GameObject.Instantiate(animation);
        newAnim.transform.position = gameObject.transform.position;
        //Debug.Log("HERE");
        Destroy(gameObject);
    }
}
