using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    //how quickly the camera moves towards the target
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate = 30 
    // LateUpdate = comes in last
    void LateUpdate()
    {
        //Using lateupdate to avoid jerky

        //TODO: Move camera towards the character and follows it

        if(transform.position != target.position)
        {
            
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //clamp - so it has a min and max position
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        }

    }
}
