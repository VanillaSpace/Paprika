using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootButtons : MonoBehaviour
{
    [SerializeField]
    private Image icon;

    [SerializeField]
    private Text title;

    public Image MyIcon { get => icon; }
    public Text MyTitle { get => title;}
}
