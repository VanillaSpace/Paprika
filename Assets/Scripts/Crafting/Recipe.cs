using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
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
    public string Description { get => description; }
    public CraftingMaterial[] Materials { get => materials; }

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
