using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCamMove : MonoBehaviour
{
    public Vector2 cameraChange;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text Location_TXT;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Camera switches to the new room!");

        if (other.CompareTag("Player"))
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(locationNameCo());
            }
        }
    }

    //using corroutine because we only want it a certain amount of time 

    private IEnumerator locationNameCo()
    {
        text.SetActive(true);
        Location_TXT.text = placeName;
        yield return new WaitForSeconds(2.5f);
        text.SetActive(false);
    }
}
