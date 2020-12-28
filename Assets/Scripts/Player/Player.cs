using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

       
    }
}
