using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Projectile : IUseable, IMoveable, IDescribable
{
    [SerializeField]
    private string name;

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

    public string MyName { get => name; set => name = value; }
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
        return string.Format("{0}\nCast Time: {1} seocnds\n<color=#ffd111>{2}</color>", name, castTime,description);
    }

    public void Use()
    {
        BasicMovement.MyInstance.CastProjectile(MyName);
    }
}
