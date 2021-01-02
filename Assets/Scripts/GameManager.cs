using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private BasicMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        ClickTarget();
    }

    private void ClickTarget()
    {
        if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject())
        {
           RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero,Mathf.Infinity,512);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "Enemy")
                {
                    Debug.Log("Targeted Enemy");
                    playerMovement.MyTarget = hit.transform.GetChild(0);
                }

            }
            else
            {
                //Detag target
                playerMovement.MyTarget = null;
            }
        }
        
    }
}
