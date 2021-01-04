using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProjectileButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private string projName;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Pressed");
            HandScript.MyInstance.TakeMoveable(projectileBook.MyInstance.GetProjectile(projName));
        }
    }
}
