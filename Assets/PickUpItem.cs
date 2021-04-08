using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float TimeToLeave = 10f;

    private void Awake()
    {
        player = GameManager.MyInstance.Paprika.transform;
    }
    private void Update()
    {
        DestroyObjTimer();

        float distance = Vector3.Distance(transform.position, player.position);
        if(distance  > pickUpDistance)
        {
            return;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

    }

    private void DestroyObjTimer()
    {
        TimeToLeave -= Time.deltaTime;
        
        if(TimeToLeave < 0)
        {
            Destroy(gameObject);
        }
    }
}
