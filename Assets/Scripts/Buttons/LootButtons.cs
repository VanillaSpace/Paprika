using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LootButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    public Image MyIcon { get => icon; }
    public Text MyTitle { get => title;}

    public Item MyLoot { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //loot item
       if (inventoryScript.MyInstance.AddItem(MyLoot))
        {
            gameObject.SetActive(false);
            UIManager.MyInstance.HideToolTip();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.MyInstance.ShowToolTip(transform.position, MyLoot);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideToolTip();
    }
}
