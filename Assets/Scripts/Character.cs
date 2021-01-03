using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    protected Vector2 direction;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected CharacterStats health;

    [SerializeField]
    private float initHealth;

    public Animator animator;

    void Start()
    {
        health.Initialize(initHealth, initHealth);

    }

   
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public virtual void TakeDamage(float damage)
    {
        health.myCurrentValue -= damage;

        if (health.myCurrentValue <= 0)
        {
            
            animator.SetTrigger("Dead");
        }
    }
}

