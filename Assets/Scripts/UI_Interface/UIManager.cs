using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }

    }


    [SerializeField]
    private ActionButton[] actionButtons;

    [SerializeField]
    private CanvasGroup keybindMenu;

    [SerializeField]
    private CanvasGroup projBook;

    private GameObject[] keybindButtons;

    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private Text DecriptionText;

    private Text tooltipText;

    [SerializeField]
    private RectTransform tooltipRect;

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
        tooltipText = tooltip.GetComponentInChildren<Text>();
        keybindMenu.blocksRaycasts = false;
        projBook.blocksRaycasts = false;
 

    }
    void Start()
    {
        //OpenClose(projBook);
    }

    // Update is called once per frame
    void Update()
    {
  
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenClose(keybindMenu);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenClose(projBook);
        }
    }


    public void UpdateKeyText(string key, KeyCode code)
    {
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }

    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke();
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == false ? true : true;
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if (clickable.MyCount > 1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
        if (clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }

    //show tool tip for items
    public void ShowToolTip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        tooltipRect.pivot = pivot;
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }

    public void HideToolTip()
    {
        tooltip.SetActive(false);
    }
}
