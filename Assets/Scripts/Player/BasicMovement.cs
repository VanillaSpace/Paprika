using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public bool isBusy = false;

    public static BasicMovement instance;

    public Transform MyTarget { get; set; }

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

    public Animator animator;
    public float playerSpeed = 3.5f;

    [SerializeField]
    private GameObject[] ProjectilePrefab;

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
  
        //Player Controller (up, down, left and right)
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Vector 3 (x, y, z)
        Vector3 playerMovement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        transform.position = transform.position + playerMovement * Time.deltaTime * playerSpeed;
         
        if (Player.MyInstance.MyHealth.myCurrentValue == 0)
        {
            isBusy = true;
            animator.SetBool("Dead", true);
        }

        if (moveHorizontal > 0)
        {
            exitIndex = 0;
        }
        if (moveHorizontal < 0)
        {
            exitIndex = 1;
        }
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
    public IEnumerator Attack()
    {
        isBusy = true;
        animator.SetBool("ATK", true);
        yield return new WaitForSeconds(0.6f);
        animator.SetBool("ATK", false);
        isBusy = false;

    }

    public IEnumerator Chop(int projectileIndex)
    {
        isBusy = true;
        animator.SetBool("isChopping", true);
  
        yield return new WaitForSeconds(0.55f);

       Projectile s = Instantiate(ProjectilePrefab[projectileIndex], exitPoint[exitIndex].position, Quaternion.identity).GetComponent<Projectile>();

        s.MyTarget = MyTarget;
        
        animator.SetBool("isChopping", false);
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

    public void CastProjectile(int projectileIndex)
    {

        Block();

        if (isBusy)
        {
            Debug.Log("Busy!");
        }
        else
        {

            if (MyTarget != null && InLineOfSight())
            {
                Debug.Log("Chopping!");
                StartCoroutine(Chop(projectileIndex));
            }
            else
            {
                Debug.Log("Cannot See or no Target!");
            }

        }
    }

    private bool InLineOfSight()
    {
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position),256);

        if(hit.collider == null)
        {
            return true;
        }

        //Debug.DrawRay(transform.position, targetDirection, Color.red);
        return false;

    }

    public void Block()
    {
        foreach(Blocks b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}
