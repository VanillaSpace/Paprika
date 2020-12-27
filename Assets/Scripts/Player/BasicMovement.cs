using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
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
    }


   
}
