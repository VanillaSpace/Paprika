using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
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

    [SerializeField]
    private CharacterStats health;

    [SerializeField]
    private CharacterStats stamina;

    [SerializeField]
    private CharacterStats xpStat;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float maxStamina;

    [SerializeField]
    private float maxXP;

    [SerializeField]
    private float startXP;

    [SerializeField]
    private int level;

    [SerializeField]
    private Text levelTXT;

    public SpriteRenderer paprikaFace;

    private List<IInteractable> interactable = new List<IInteractable>();

    public CharacterStats MyHealth { get => health; set => health = value; }
    public CharacterStats MyStamina { get => stamina; set => stamina = value; }
    public Vector2 MyDirection { get => Direction; set => Direction = value; }
    public bool IsBusy { get => isBusy; set => isBusy = value; }
    public List<IInteractable> MyInteractables { get => interactable; set => interactable = value; }
    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public CharacterStats MyXP { get => xpStat; set => xpStat = value; }

    public int MyLevel { get => level; set { level = value; } }

    public float MymaxXP { get => maxXP; set => maxXP = value; }
    public float MyMaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MyMaxStamina { get => maxStamina; set => maxStamina = value; }

    private bool isMoving;

    public Vector2 lastMotionVector;


    // Start is called before the first frame update
    void Start()
    {
        health.Initialize(MyMaxHealth, MyMaxHealth);
        stamina.Initialize(MyMaxStamina, MyMaxStamina);

        //level system going upwards 
        xpStat.Initialize(Mathf.Floor(100 * MyLevel * Mathf.Pow(MyLevel, 0.2f)), 0);
        levelTXT.text = MyLevel.ToString();
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

        if (moveHorizontal == 0 && moveVertical == 0 && isBusy == false)
        {
            playerAnim.SetBool("isRun", false);
            playerAnim.SetBool("isWalk", false);

            if (stamina.myCurrentValue < 50)
            {
                stamina.myCurrentValue += 0.01f;
            }
            
        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            lastMotionVector = new Vector2(
                moveHorizontal,
                moveVertical
            ).normalized;
        }


        MyDirection = Vector2.zero;

        //health
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("Should be losing HP");
        //    health.myCurrentValue -= 10;
        //    stamina.myCurrentValue -= 10;
        //}
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Should be gaining HP");
            health.myCurrentValue += 10;
            {
                MyDirection += Vector2.up;
            }
            stamina.myCurrentValue += 10;
        }

        //Running
        if ((Input.GetKey(KeyCode.LeftShift)))
        {
            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
            {
                speed = 5f;
                paprikaFace.flipX = true;
                playerAnim.SetBool("isRun", true);
                playerAnim.SetBool("isWalk", false);
                MyDirection += Vector2.left;
                stamina.myCurrentValue -= 0.005f;
            }
            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
            {
                speed = 5f;
                paprikaFace.flipX = false;
                playerAnim.SetBool("isRun", true);
                playerAnim.SetBool("isWalk", false);
                MyDirection += Vector2.right;
                stamina.myCurrentValue -= 0.005f;
            }

            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
            {
                speed = 5f;
                paprikaFace.flipX = false;
                playerAnim.SetBool("isRun", true);
                playerAnim.SetBool("isWalk", false);
                MyDirection += Vector2.up;
                stamina.myCurrentValue -= 0.005f;
            }

            if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
            {
                speed = 5f;
                paprikaFace.flipX = false;
                playerAnim.SetBool("isRun", true);
                playerAnim.SetBool("isWalk", false);
                MyDirection += Vector2.down;
                stamina.myCurrentValue -= 0.005f;
            }

        }


        //Walking
        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["LEFT"]))
            {
                speed = 3.0f;
                playerAnim.SetBool("isRun", false);
                playerAnim.SetBool("isWalk", true);
                paprikaFace.flipX = true;
                MyDirection += Vector2.left;
                
            }
        else if(Input.GetKey(KeybindManager.MyInstance.Keybinds["RIGHT"]))
            {
                speed = 3.0f;
                playerAnim.SetBool("isRun", false);
                playerAnim.SetBool("isWalk", true);
                paprikaFace.flipX = false;
                 MyDirection += Vector2.right;
            }

        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["UP"]))
            {
                speed = 3.0f;
                playerAnim.SetBool("isRun", false);
                playerAnim.SetBool("isWalk", true);
                MyDirection += Vector2.up;
            }
        else if (Input.GetKey(KeybindManager.MyInstance.Keybinds["DOWN"]))
            {
                speed = 3.0f;
                playerAnim.SetBool("isRun", false);
                playerAnim.SetBool("isWalk", true);
                MyDirection += Vector2.down;
            }


        //bags - close and open all bags
        if (Input.GetKeyDown(KeyCode.B))
        {
            InventoryScript.MyInstance.OpenClose();
        }
        //gain XP 
        if (Input.GetKeyDown(KeyCode.X))
        {
            GainXP(100);
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

    public  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();

            if (!MyInteractables.Contains(interactable))
            {
                MyInteractables.Add(interactable);
            }

            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
      if (collision.tag == "Enemy" || collision.tag == "Interactable")
        {
            if (MyInteractables.Count > 0)
            {
                IInteractable interactable = MyInteractables.Find(x => x == collision.GetComponent<IInteractable>());

                if (interactable != null)
                {
                    interactable.StopInteract();
                    UIManager.MyInstance.HideToolTip();
                }

                MyInteractables.Remove(interactable);
            }
        }
      
    }

    public void GetHealth(int recoveredHealth)
    {
        health.myCurrentValue += recoveredHealth;
        CombatTextManager.MyInstance.CreateText(transform.position, recoveredHealth.ToString(), SCTTYPE.HEAL);
    }

    public void GetDamage(int damage)
    {
        health.myCurrentValue += damage;
        CombatTextManager.MyInstance.CreateText(transform.position, damage.ToString(), SCTTYPE.DAMAGE);
    }

    public void GainXP(int xp)
    {
        xpStat.myCurrentValue += xp;
        CombatTextManager.MyInstance.CreateText(transform.position, xp.ToString(), SCTTYPE.XP);
        if (xpStat.myCurrentValue >= xpStat.MyMaxValue)
        {
            StartCoroutine(levelUp());
        }
    }

    public void GetItem(string name)
    {
        CombatTextManager.MyInstance.CreateText(transform.position, name.ToString(), SCTTYPE.ITEM);

        //for debug
        Debug.Log("collected" + name);
    }

    public void UpdateLevel()
    {
        levelTXT.text = MyLevel.ToString();
    }

    private IEnumerator levelUp()
    {
        while(!xpStat.IsFull)
        {
            yield return null;
        }

        //Level up and XP increases
        MyLevel++;
        levelTXT.text = MyLevel.ToString();
        xpStat.MyMaxValue = 100 * MyLevel * Mathf.Pow(MyLevel, 0.5f);
        xpStat.MyMaxValue = Mathf.Floor(xpStat.MyMaxValue);
        xpStat.myCurrentValue = xpStat.MyOverflow;
        xpStat.Reset();
        
    }

  
}
