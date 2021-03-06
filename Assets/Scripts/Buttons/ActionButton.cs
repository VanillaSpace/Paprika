using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
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
    public IUseable MyUseable { get; set; }

    [SerializeField]
    private Text stackSize;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;

    public Button MyButton { get; private set; }

    [SerializeField]
    private Image icon;

    public Image MyIcon { get => icon; set => icon = value; }

    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public Text MyStackText 
    { 
        get { return stackSize; }
    }

    public Stack<IUseable> Useables
    {
        get
        {
            return useables;
        }

        set
        {
            if (value.Count > 0 )
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }
            
            useables = value;
        }
    }

    tools_Interactable tools_i;

    public Tools_Scriptable GetTool
    {
        get
        {
            return tools_Interactable.MyInstance.tools;
            //return (Tools_Scriptable)InventoryScript.MyInstance.MyItems[tools_Interactable.MyInstance.inv_item_list];
        }
    }

    private void Awake()
    {
        tools_i = GetComponent<tools_Interactable>();
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);

    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetTool);
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            else if (Useables != null && Useables.Count > 0)
            {
                Useables.Peek().Use();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
 
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUseable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUseable);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IDescribable tmp = null;

        if (MyUseable != null && MyUseable is IDescribable)
        {

            tmp = (IDescribable)MyUseable;
            //UIManager.MyInstance.ShowToolTip(transform.position);

        }
        else if (Useables.Count > 0)
        {
           
            //UIManager.MyInstance.ShowToolTip(transform.position);

        }
        if (tmp != null)
        {
            UIManager.MyInstance.ShowToolTip(new Vector2(1, 0), transform.position, tmp);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }

    public void SetUseable(IUseable useable)
    {
        if (useable is Item)
        {
            Useables = InventoryScript.MyInstance.GetUseables(useable);

            count = Useables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            Useables.Clear();
            this.MyUseable = useable;
        }

        count = Useables.Count;
        UpdateVisual();
   
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if (count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if (item is IUseable && Useables.Count > 0)
        {
            if (Useables.Peek().GetType() == item.GetType())
            {
                Useables = InventoryScript.MyInstance.GetUseables(item as IUseable);

                count = Useables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

}
