using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon/Sword")]
public class Sword : Weapon {

    public override void use()
    {
        Vector3 personPos = myPerson.gameObject.transform.position;
        //Vector3 personForward = myPerson.gameObject.transform.forward;
        Vector3 personForward = myPerson.currentDirection;
        Vector3 damagerOffset = Vector3.Normalize(personForward) * range;

        Vector3 spawnPoint = personPos + damagerOffset;
        spawnDamager(spawnPoint);
    }
}
