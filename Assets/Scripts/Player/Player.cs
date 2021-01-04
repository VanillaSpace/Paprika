using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

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


    private Vector2 Direction;



    // Start is called before the first frame update
    void Start()
    {
        health.Initialize(maxHealth, maxHealth);
        stamina.Initialize(maxStamina, maxStamina);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

    }

    private void GetInput()
    {
        Direction = Vector2.zero;

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
            health.myCurrentValue += 10; if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["UP"]))
            {
                Direction += Vector2.up;
            }
            stamina.myCurrentValue += 10;
        }

        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["UP"]))
        {
            Direction += Vector2.up;
        }
        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["LEFT"]))
        {
            Direction += Vector2.left;
        }
        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["DOWN"]))
        {
            Direction += Vector2.down;
        }
        if (Input.GetKeyDown(KeybindManager.MyInstance.Keybinds["RIGHT"]))
        {
            Direction += Vector2.right;
        }

        //bags - close and open all bags

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryScript.MyInstance.OpenClose();
        }

        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
    }

}
