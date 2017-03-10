using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonArray : MonoBehaviour {

    public PanelController panelController;

    public enum Stats { health, stamina, mana, damageReduction, deflection, speed, might, accuracy, intelligence }
    public string[] statNames = new string[9];
    public float[] stats = new float[numOfButtons];
    public int maxStats = 50;
    public int totalCurrentStats;

    public int TotalCurrentStats
    {
        get
        {
            return totalCurrentStats;
        }
        set
        {
            totalCurrentStats = value;
            setPointsLeft();
        }
    }

    public const int numOfButtons = 9;
    public ButtonSet[] buttonSets = new ButtonSet[numOfButtons];
    public Button finishedButton;

    public Text pointsLeft;
    public Text currentPlayer;

    void OnEnable()
    {
        currentPlayer.text = "Player: " + (panelController.playersCreated + 1).ToString();
    }
    

    void Start ()
    {
        buttonSets = GetComponentsInChildren<ButtonSet>();
        calculateTotalCurrentStats();

        finishedButton.onClick.AddListener(finishStatSelection);

        setStatNames();
        setPointsLeft();
	}

    void setStatNames()
    {
        for (int i = 0; i < buttonSets.Length; i++)
        {
            buttonSets[i].fieldName.text = statNames[i];
        }
    }

    void Update()
    {

    }

    void calculateTotalCurrentStats()
    {
        totalCurrentStats = 0;

        foreach (ButtonSet bs in buttonSets)
        {
            totalCurrentStats += bs.value;
        }
    }

    void setPointsLeft()
    {
        pointsLeft.text = (maxStats - totalCurrentStats).ToString();
    }

    void finishStatSelection()
    {
        //Assign the correct variables into the stats array of PanelController
        for (int i = 0; i < buttonSets.Length; i++)
        {
            stats[i] = buttonSets[i].value;
        }

        //Pass this array to the PanelController
        panelController.Stats = stats;
    }
}
