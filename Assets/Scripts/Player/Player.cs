using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  

    public static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }

    }

    public CharacterStats MyHealth { get => health; set => health = value; }
    public CharacterStats MyStamina { get => stamina; set => stamina = value; }

    [SerializeField]
    private CharacterStats health;

    [SerializeField]
    private CharacterStats stamina;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float maxStamina;

  

    // Start is called before the first frame update
    void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        stamina.Initialize(maxStamina, maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
       
  
        //health

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Should be losing HP");
            health.myCurrentValue -= 10;
            stamina.myCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Should be gaining HP");
            health.myCurrentValue += 10;
            stamina.myCurrentValue += 10;
        }

        //bags - close and open all bags

        if  (Input.GetKeyDown(KeyCode.B))
        {
            inventoryScript.MyInstance.OpenClose();
        }
       
    }

    //Items - becomes invisible once used 
    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount == 0)
        {
            //make icon invisible color (0,0,0,0)
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
        }
    }
}
