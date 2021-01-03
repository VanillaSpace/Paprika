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

    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");
    }
    void Start()
    {
        SetUseable(actionButtons[0], projectileBook.MyInstance.GetProjectile("FIRE DART"));
        SetUseable(actionButtons[1], projectileBook.MyInstance.GetProjectile("FROST DART"));
        SetUseable(actionButtons[2], projectileBook.MyInstance.GetProjectile("THUNDER DART"));
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

    public void SetUseable(ActionButton btn, IUseable useable)
    {
        btn.MyIcon.sprite = useable.MyIcon;
        btn.MyIcon.color = Color.white;
        btn.MyUseable = useable;
    }

    public void OpenClose(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;
    }
}
