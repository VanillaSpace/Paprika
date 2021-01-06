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

    private Vector2 Direction;

    public Animator playerAnim;

    public float speed = 3.5f;

    private bool isBusy = false;
    public CharacterStats MyHealth { get => health; set => health = value; }
    public CharacterStats MyStamina { get => stamina; set => stamina = value; }
    public Vector2 MyDirection { get => Direction; set => Direction = value; }
    public bool IsBusy { get => isBusy; set => isBusy = value; }

    [SerializeField]
    private CharacterStats health;

    [SerializeField]
    private CharacterStats stamina;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float maxStamina;

    public SpriteRenderer paprikaFace;




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

        PlayerAxis();

        Move();

        StopProjectiles();
    }

    public void Move()
    {
        transform.Translate(MyDirection * speed * Time.deltaTime);
    }

    private void GetInput()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        MyDirection = Vector2.zero;

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
            {
                MyDirection += Vector2.up;
            }
            stamina.myCurrentValue += 10;
        }

        //player movement 

            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
            {
                paprikaFace.flipX = true;
                MyDirection += Vector2.left;
                
            }
            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
            {
                paprikaFace.flipX = false;
                 MyDirection += Vector2.right;
            }

            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
            {
                MyDirection += Vector2.up;
            }
            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
            {
                MyDirection += Vector2.down;
            }


        //bags - close and open all bags

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryScript.MyInstance.OpenClose();
        }

        //Action Button
        foreach (string action in KeybindManager.MyInstance.ActionBinds.Keys)
        {
            if (Input.GetKeyDown(KeybindManager.MyInstance.ActionBinds[action]))
            {
                UIManager.MyInstance.ClickActionButton(action);
            }
        }
    }

    public void Death()
    {
        if (MyHealth.myCurrentValue == 0)
        {
            IsBusy = true;
            playerAnim.SetBool("Dead", true);
        }
    }

    public void PlayerAxis()
    {

            playerAnim.SetFloat("Vertical", Input.GetAxis("Vertical"));
            playerAnim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        
           
    }

    public void StopProjectiles()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //if the player moves, the casting for the projectile stops
        if (moveHorizontal > 0)
        {
            playerAnim.SetBool("isChopping", false);
            projectileBook.MyInstance.stopCasting();
            BasicMovement.MyInstance.ExitIndex = 0;
            
        }
        if (moveHorizontal < 0)
        {
            playerAnim.SetBool("isChopping", false);
            projectileBook.MyInstance.stopCasting();
            BasicMovement.MyInstance.ExitIndex = 1;
        }
        if (moveVertical < 0 && moveVertical > 0)
        {
            playerAnim.SetBool("isChopping", false);
            projectileBook.MyInstance.stopCasting();
        }

    }

}
