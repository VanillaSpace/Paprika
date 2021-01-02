using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Projectile
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
    private Color barColor;

    public string MyName { get => name; set => name = value; }
    public int MyDamage { get => damage; set => damage = value; }
    public Sprite MyIcon { get => icon; set => icon = value; }
    public float MySpeed { get => speed; set => speed = value; }
    public float MyCastTime { get => castTime; set => castTime = value; }
    public GameObject MyProjectilePrefab { get => projectilePrefab; set => projectilePrefab = value; }
    public Color MyBarColor { get => barColor; set => barColor = value; }
}
