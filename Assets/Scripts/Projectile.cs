using UnityEngine;
using System.Collections;

public class Projectile : Damager {

    public override void Start()
    {
        //Use the arrow sprite to create a collider and SpriteRenderer
        //Component to this script, so that it is visible
        //And can detect collision with an enemy
        BoxCollider2D bc = gameObject.AddComponent<BoxCollider2D>();
        SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = arrow;

        //Normalize direction vector
        Vector3.Normalize(direction);
    }

    public override void Update()
    {
        gameObject.transform.position += direction * speed;
    }

    void OnCollisionEnter2D()
    {
        attack();
    }

}
