using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponButton : MonoBehaviour
{
    public WeaponPanel weaponPanel;

    public Button button;
    public Text weaponName;
    public Weapon myWeapon;
    public bool pressed = false;
	
    void Start()
    {
        button.onClick.AddListener(addMyWeapon);
        weaponName = GetComponentInChildren<Text>();
    }

    void addMyWeapon()
    {
        weaponPanel.addWeapon(myWeapon);
        pressed = true;
    }
}
