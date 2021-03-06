using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Projectile : IUseable, IMoveable, IDescribable, ICastable
{
    [SerializeField]
    private string title;

    [SerializeField]
    private int damage;

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private string description;

    [SerializeField]
    private Color barColor;

    public string MyTitle { get => title; set => title = value; }
    public int MyDamage { get => damage; set => damage = value; }
    public Sprite MyIcon 
    { 
        get
        {
            return icon;
        }
    }
    public float MySpeed { get => speed; set => speed = value; }
    public float MyCastTime { get => castTime; set => castTime = value; }
    public GameObject MyProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public Color MyBarColor { get => barColor; set => barColor = value; }



    public string GetDescription()
    {

        return string.Format("<color=#{0}>{1}</color>\nCast Time: {2} seconds\n<color=#{0}>{3}</color>", ColorUtility.ToHtmlStringRGB(MyBarColor), title, castTime, description);
    }

    public void Use()
    {
        BasicMovement.MyInstance.CastProjectile(this);
    }

   
}
