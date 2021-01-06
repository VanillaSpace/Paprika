using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    public IUseable MyUseable { get; set; }

    [SerializeField]
    private Text stackSize;

    private Stack<IUseable> useables = new Stack<IUseable>();

    private int count;

    public Button MyButton { get; private set; }

    [SerializeField]
    private Image icon;

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

    private void Awake()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        inventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {
                MyUseable.Use();
            }
            if (useables != null && useables.Count > 0)
            {
                useables.Peek().Use();
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
        else if (useables.Count > 0)
        {
           
            //UIManager.MyInstance.ShowToolTip(transform.position);

        }
        if (tmp != null)
        {
            UIManager.MyInstance.ShowToolTip(transform.position, tmp);
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
            useables = inventoryScript.MyInstance.GetUseables(useable);
            count = useables.Count;
            inventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            inventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            this.MyUseable = useable;
        }
     

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
        if (item is IUseable && useables.Count > 0)
        {
            if (useables.Peek().GetType() == item.GetType())
            {
                useables = inventoryScript.MyInstance.GetUseables(item as IUseable);

                count = useables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }
}
