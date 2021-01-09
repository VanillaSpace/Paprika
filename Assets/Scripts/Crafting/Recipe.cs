using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour, ICastable
{
    [SerializeField]
    private CraftingMaterial[] materials;

    [SerializeField]
    private Item output;

    [SerializeField]
    private int outputCount;

    [SerializeField]
    private string description;

    [SerializeField]
    private Image hightlight;

    public Item Output { get => output; }
    public int OutputCount { get => outputCount; set => outputCount = value; }
    public string MyDescription { get => description; }
    public CraftingMaterial[] Materials { get => materials; }

    [SerializeField]
    private float craftTime;

    [SerializeField]
    private Color barColor;


    public string MyTitle
    {
        get
        {
            return output.MyTitle;
        }
    }

    public Sprite MyIcon
    {
        get
        {
            return output.MyIcon;
        }
    }

    public float MyCastTime
    {
        get
        {
            return craftTime;
        }
    }

    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }

    void Start()
    {
        GetComponent<Text>().text = output.MyTitle;
    }

    public void Select()
    {
        Color c = hightlight.color;
        c.a = 0.3f;
        hightlight.color = c;
    }

    public void DeSelect()
    {

        Color c = hightlight.color;
        c.a = 0f;
        hightlight.color = c;
    }
}
