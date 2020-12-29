using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    private IUseable useable;

    public static ActionButton instance;

    public static ActionButton MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ActionButton>();
            }
            return instance;
        }

    }


    public Button MyButton { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        //MyButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (useable != null)
        {
            useable.Use();
        }

        BasicMovement.instance.Chop();
    }

    public void chopping()
    {
        Debug.Log("chopping called!");
        BasicMovement.MyInstance.StartCoroutine("Chop");
    }

    public void attack()
    {
        Debug.Log("attack called!");
        BasicMovement.MyInstance.StartCoroutine("Attack");
    }

    public void water()
    {
        Debug.Log("watering");
        BasicMovement.MyInstance.StartCoroutine("Water");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}
