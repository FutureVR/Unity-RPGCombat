using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Weapon/Spell")]
public class Spell : Weapon {

    public override void use()
    {
        //Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Vector3 spawn = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 personPos = myPerson.gameObject.transform.position;
        Vector3 personForward = myPerson.currentDirection;
        personForward = new Vector3(personForward.x, personForward.y, 0);
        personForward = Vector3.Normalize(personForward);

        Vector3 spawn = personPos + personForward * range;
        spawnDamager(spawn);
    }
}
