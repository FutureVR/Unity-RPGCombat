using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class WeaponPanel : MonoBehaviour
{
    public PanelController panelController;

    int currentWeapon = 0;

    public Weapon[] selectedWeapons = new Weapon[2];
    public WeaponButton[] buttons;

    public Weapon[] possibleWeapons;

    public Button finishedButton;
    public Text currentPlayer;

    void OnEnable()
    {
        currentPlayer.text = "Player: " + (panelController.playersCreated + 1).ToString();
    }

    void Start()
    {
        buttons = GetComponentsInChildren<WeaponButton>();
        assignWeaponToButtons();
        finishedButton.onClick.AddListener(sendWeaponsArrayUp);
    }

    void assignWeaponToButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && possibleWeapons[i] != null)
            {
                buttons[i].weaponName.text = possibleWeapons[i].weaponName;
                buttons[i].myWeapon = possibleWeapons[i];
            }
        }
    }

    public void addWeapon(Weapon w)
    {
        if (currentWeapon < selectedWeapons.Length)
        {
            selectedWeapons[currentWeapon] = w;
            currentWeapon++;
        }
    }

    public void sendWeaponsArrayUp()
    {
        panelController.Weapons = selectedWeapons;
    }
}
