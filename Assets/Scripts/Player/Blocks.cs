using System;
using UnityEngine;

[Serializable]
public class Blocks
{
    [SerializeField]
    private GameObject top, bottom;

    public void Deactivate()
    {
        top.SetActive(false);
        bottom.SetActive(false);
    }

    public void Activate()
    {
        top.SetActive(true);
        bottom.SetActive(true);
    }

}
