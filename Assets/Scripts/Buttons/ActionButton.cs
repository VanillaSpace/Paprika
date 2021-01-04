using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerClickHandler
{
    public IUseable MyUseable { get; set; }

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

    private void Awake()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
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
        if (MyUseable != null)
        {
            MyUseable.Use();
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

    public void SetUseable(IUseable useable)
    {
        this.MyUseable = useable;

        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;
    }
}
