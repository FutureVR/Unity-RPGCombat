using UnityEngine;
using System.Collections;

public class DestroyIfOffScreen : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        float tileWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float tileHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        float dist = (transform.position.y - Camera.main.transform.position.y);
        float leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + tileWidth;
        float rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - tileWidth;

        float upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + tileHeight;
        float downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - tileHeight;

        if (transform.position.x > rightLimitation || transform.position.x < leftLimitation) die();
        if (transform.position.y > downLimitation || transform.position.y < upLimitation) die();
    }

    void die()
    {
        GetComponent<Damager>().die();
    }
}
