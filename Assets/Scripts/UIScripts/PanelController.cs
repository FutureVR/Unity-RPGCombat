using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PanelController : MonoBehaviour
{
    public int numOfPlayers = 2;    
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> characterPrefabs = new List<GameObject>();
    public List<Transform> spawnLocations = new List<Transform>();

    public int playersCreated;

    public GameObject[] panels;
    public int currentPanel = -1;

    public float[] statScalars = new float[9];
    public float[] statMin = new float[9];

    //When the panel sets this array, switch to next array
    public float[] stats = new float[9];
    public float[] Stats
    {
        set
        {
            setNextPanelActive();
            stats = value;
        }
    }

    //When the panel sets this array, switch to next array
    public Weapon[] weapons = new Weapon[2];
    public Weapon[] Weapons
    {
        set
        {
            setNextPanelActive();
            weapons = value;
        }
    }


    void Start()
    {
        setNextPanelActive();
    }

    //Sets the next panel active
    void setNextPanelActive()
    {
        currentPanel++;

        if (currentPanel < panels.Length  &&  playersCreated != numOfPlayers)
        {
            foreach (GameObject go in panels)
            {
                go.SetActive(false);
            }

            panels[currentPanel].SetActive(true);
        }
        else
        {
            playersCreated++;

            if (playersCreated <= numOfPlayers)
            {
                createOneCharacter();
                currentPanel = -1;
                setNextPanelActive();
            }
            else
            {
                //All the characters have been created
                spawnAllCharacters();
                Application.LoadLevel("Main");
            }
        }
    }

    void createOneCharacter()
    {
        GameObject newPerson = GameObject.Instantiate(characterPrefabs[playersCreated-1]);
        Person personComponent = newPerson.GetComponent<Person>();

        newPerson.transform.position = spawnLocations[playersCreated - 1].position;
        personComponent.alliance = playersCreated - 1;
        personComponent.baseStats = stats;
        personComponent.weapons = weapons.ToList();
        personComponent.statMin = statMin;
        personComponent.statScalars = statScalars;
        newPerson.SetActive(false);

        characters.Add(newPerson);
    }

    void spawnAllCharacters()
    {
        //Debug.Log("Creating All Characters");
        foreach (GameObject go in characters)
        {
           go.SetActive(true);
        }
    }
}
