using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }

    }

    public IMoveable MyMoveable { get; set; }

    private Image icon;

    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
       // icon = GetComponent<Image>();
    }
    void Start()
    {
        icon = GetComponent<Image>();
    }


    void Update()
    {
        icon.transform.position = Input.mousePosition+offset;

        DeleteItem();
    }

    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white;
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;

        MyMoveable = null;

        icon.color = new Color(0, 0, 0, 0);

        return tmp;
    }

    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
    }

    private void DeleteItem()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            if (MyMoveable is Item && InventoryScript.MyInstance.FromSlot != null)
            {
                (MyMoveable as Item).MySlot.Clear();
            }

            Drop();

            InventoryScript.MyInstance.FromSlot = null;
        }
    }
}
