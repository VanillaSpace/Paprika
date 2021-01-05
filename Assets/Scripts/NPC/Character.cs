using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{

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

    public Animator animator;

    public Transform MyTarget { get; set; }

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Speed { get => speed; set => speed = value; }
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }
    public bool IsDead { get => isDead; set => isDead = value; }

    void Start()
    {
        health.Initialize(initHealth, initHealth);
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
                     
        if (health.myCurrentValue <= 0)
        {
            isDead = true;
            animator.SetTrigger("Dead");
        }
    }

    public void OnHealthChaneged(float myCurrentValue)
    {

    }
}

