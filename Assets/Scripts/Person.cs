using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : MonoBehaviour {

    //Determines which side this person is on
    //All people with same number are on the same side
    public int alliance;

    public enum Stats { health, stamina, mana, damageReduction, deflection, speed, might, accuracy, intelligence}
    public float[] baseStats = new float[9];
    public float[] totalStats = new float[9];

    [HideInInspector] public float[] statScalars = new float[9];
    [HideInInspector] public float[] statMin = new float[9];

    public float health;
    public float stamina;
    public float mana;

    public List<Weapon> weapons;
    public List<Weapon> abilities;
    public List<Gear> gear;

    string leftWeaponButton = "Fire1";
    string rightWeaponButton = "Fire2";

    public enum Directions { up, right, down, left }
    public Vector2 currentDirection;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        calculateTotalStatsFromGear();
        initializeStats();
        initializeWeaponsAndAbilities();
    }

    void initializeStats()
    {
        for (int i = 0; i < 9; i++)
        {
            totalStats[i] = totalStats[i] * statScalars[i] + statMin[i]; 
        }

        health = totalStats[(int)Stats.health];
        stamina = totalStats[(int)Stats.stamina];
        mana = totalStats[(int)Stats.mana];
    }

    void initializeWeaponsAndAbilities()
    {
        if (weapons[0] != null || weapons[1] != null) foreach(Weapon w in weapons) w.initialize(this);
        if (abilities.Count > 0) foreach(Weapon a in abilities) a.initialize(this);
    }

    void calculateTotalStatsFromGear()
    {
        totalStats = baseStats;
    }

    void Update()
    {
        CheckMovementInput();
        CheckAttackInput();
        RegenerateResources();
    }

    public float healthRegenRate = .1f;
    public float staminaRegenRate = .1f;
    public float manaRegenRate = .1f;

    public float shotDelay;
    private float timeAtLastShot;

    void RegenerateResources()
    {
        if (health < totalStats[(int)Stats.health])   health += healthRegenRate * Time.deltaTime;
        if (stamina < totalStats[(int)Stats.stamina])   stamina += staminaRegenRate * Time.deltaTime;
        if (mana < totalStats[(int)Stats.mana])   mana += manaRegenRate * Time.deltaTime;
    }

    void CheckMovementInput()
    {
        float x_movement = Input.GetAxisRaw(alliance + "_Horizontal");
        float y_movement = Input.GetAxisRaw(alliance + "_Vertical");

        Vector3 movement = new Vector3(x_movement, y_movement, 0);
        movement = Vector3.Normalize(movement) * totalStats[(int)Stats.speed];

        if (movement.magnitude != 0) currentDirection = movement;

        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        float camSize = mainCam.orthographicSize;

        Vector3 curPos = gameObject.transform.position;
        float ratio = Screen.width / (float)Screen.height;
        Debug.Log(ratio);

        gameObject.transform.Translate(movement);

        float tileWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        float tileHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;

        float dist = (transform.position.y - Camera.main.transform.position.y);
        float leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x + tileWidth;
        float rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x - tileWidth;

        float upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y + tileHeight;
        float downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y - tileHeight;

        float tempx = Mathf.Clamp(transform.position.x, leftLimitation, rightLimitation);
        float tempy = Mathf.Clamp(transform.position.y, upLimitation, downLimitation);

        transform.position = new Vector3(tempx, tempy, transform.position.z);
    }

    void CheckAttackInput()
    {
        if (Input.GetButtonDown(alliance + "_Fire1")) useWeapon(0);
        if (Input.GetButtonDown(alliance + "_Fire2")) useWeapon(1);
    }

    public void useWeapon(int index)
    {
        if (Time.time - timeAtLastShot > shotDelay)
        {
            if (weapons.Count >= index)
            {
                if (weapons[index] != null && stamina > weapons[index].staminaCost && mana > weapons[index].manaCost)
                {
                    timeAtLastShot = Time.time;

                    weapons[index].use();
                    stamina -= weapons[index].staminaCost;
                    mana -= weapons[index].manaCost;
                }
            }
        }
    }


    public void takeDamage(float damage)
    {
        Debug.Log("Taking Damage");
        health -= damage;
        if (health <= 0) die();
    }

    public void die()
    {
        Destroy(gameObject);
        Debug.Log("I'm Dying");
    }
}
