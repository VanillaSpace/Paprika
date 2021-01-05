using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public bool isBusy = false;

    public Transform MyTarget { get; set; }

    public static BasicMovement instance;

    public static BasicMovement MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BasicMovement>();
            }
            return instance;
        }

    }

    public int ExitIndex { get => exitIndex; set => exitIndex = value; }

    public Animator animator;

    [SerializeField]
    private Blocks[] blocks;


    [SerializeField]
    private Transform[] exitPoint;

    private int exitIndex = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        actionInputs();

    }

    public IEnumerator Attack()
    {
        isBusy = true;
        animator.SetBool("ATK", true);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("ATK", false);
        isBusy = false;

    }

    public IEnumerator Roll(string projectileName)
    {
        Projectile newProjectile = projectileBook.MyInstance.castProjectile(projectileName);

        isBusy = true;
        animator.SetBool("isRoll", true);

        yield return new WaitForSeconds(newProjectile.MyCastTime);

        if (MyTarget != null && InLineOfSight())
        {
            ProjectileScript s = Instantiate(newProjectile.MyProjectilePrefab, exitPoint[ExitIndex].position, Quaternion.identity).GetComponent<ProjectileScript>();
            s.Initialize(MyTarget, newProjectile.MyDamage, transform);
        }
       
        animator.SetBool("isRoll", false);
        
        isBusy = false;

    }

    public IEnumerator Water()
    {
        isBusy = true;
        animator.SetBool("isWatering", true);
        yield return new WaitForSeconds(0.55f);
        animator.SetBool("isWatering", false);
        isBusy = false;

    }

    public void CastProjectile(string projectileName)
    {

        Block();

        if (isBusy)
        {
            Debug.Log("Busy!");
        }
        else
        {

            if (MyTarget != null && (Enemy.MyInstance.IsDead is false) && InLineOfSight())
            {
                Debug.Log("Throwing dart!");
                StartCoroutine(Roll(projectileName));
                
            }
            else 
            {
                if(Enemy.MyInstance.IsDead is true)
                {
                    Debug.Log("Enemy is dead");
                    
                    
                }
                else if (MyTarget is null)
                {
                    Debug.Log("No target selected");
                }
                else if (!InLineOfSight())
                {
                    Debug.Log("Cannot See");
                }
            }

        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        
        }
        return false;
    }

    public void Block()
    {
        foreach(Blocks b in blocks)
        {
            b.Deactivate();
        }

        blocks[ExitIndex].Activate();
    }

    public void actionInputs()
    {
        //Sword Attack
       //if (Input.GetButtonDown("Fire1"))
        //{
        //    if (isBusy)
        //    {
        //        Debug.Log("Busy!");
        //    }
        //    else
        //    {
        //        Debug.Log("Attacking!");
        //        StartCoroutine(Attack());
        //    }

        //}

        if (Input.GetKeyDown(KeyCode.C))
        {

            //Block();

            //if (isBusy)
            //{
            //    Debug.Log("Busy!");
            //}
            //else
            //{

            //    if (MyTarget != null && InLineOfSight())
            //    {
            //        Debug.Log("Chopping!");
            //        StartCoroutine(Chop());
            //    }
            //    else
            //    {
            //        Debug.Log("Cannot See or no Target!");
            //    }

            //}

        }
    }
}
