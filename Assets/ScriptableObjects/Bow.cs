using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon/Bow")]
public class Bow : Weapon {

    public Sprite arrow;
    public float distance;
    public float speed;


    public override void use()
    {
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 personPos = myPerson.gameObject.transform.position;
        //Vector3 personForward = mainCam.ScreenToWorldPoint(Input.mousePosition) - personPos;
        Vector3 personForward = myPerson.currentDirection;
        personForward = new Vector3(personForward.x, personForward.y, 0);
        personForward = Vector3.Normalize(personForward);
        Vector3 damagerOffset = Vector3.Normalize(personForward) * range;

        Vector3 spawnPoint = personPos + damagerOffset;
        spawnDamager(spawnPoint, personForward);
    }

    public void spawnDamager(Vector3 spawnPoint, Vector3 direction)
    {
        Damager newDamager = spawnDamager(spawnPoint);
        newDamager.arrow = arrow;
        newDamager.direction = direction;
        newDamager.speed = speed;
        newDamager.distance = distance;
        newDamager.isProjectile = true;
    }

}
