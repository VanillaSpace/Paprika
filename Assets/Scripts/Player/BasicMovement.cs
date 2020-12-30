using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public bool isBusy = false;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    public Animator animator;
    public float playerSpeed = 3.5f;


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
            if (isBusy)
            {
                Debug.Log("Busy!");
            }
            else
            {
                Debug.Log("Chopping!");
                StartCoroutine(Chop());
            }

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

    public IEnumerator Chop()
    {
        isBusy = true;
        animator.SetBool("isChopping", true);
        yield return new WaitForSeconds(0.55f);
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


}
