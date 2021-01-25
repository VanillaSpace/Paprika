using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{

    public static Character instance;

    public static Character MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Character>();
            }
            return instance;
        }

    }

    [SerializeField]
    private float speed;

    private Vector2 direction;

    protected Animator myAnimator;

    private bool isAttacking = false;

    private bool isDead = false;

    public bool MyProperty { get; set; }

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected CharacterStats health;

    [SerializeField]
    private float initHealth;
    public float EnemyHealth
    {
        get { return initHealth; }
        set { initHealth = value; }
    }
    public Animator animator;

    [SerializeField]
    private string type;

    public Transform MyTarget { get; set; }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public string MyType { get => type; set => type = value; }

    void Start()
    {
        health.Initialize(EnemyHealth, EnemyHealth);
    }

    private void FixedUpdate()
    {
        Move();
        OnHealthChaneged(health.myCurrentValue);
    }

    public void Move()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
    }

    public virtual void TakeDamage(float damage, Transform source)
    {
        health.myCurrentValue -= damage;
        CombatTextManager.MyInstance.CreateText(transform.position, damage.ToString(), SCTTYPE.DAMAGE);

        if (health.myCurrentValue <= 0)
        {
            GameManager.MyInstance.OnKillConfirmed(this);
            animator.SetTrigger("Dead");
            isDead = true;
        }
    }

    public void OnHealthChaneged(float myCurrentValue)
    {

    }


}

