using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IInteractable
{

    [SerializeField]
    private SpriteRenderer slimeFacing;

    [SerializeField]
    private CanvasGroup healthGroup;

    [SerializeField]
    private Animator enemyAnim;
       
    private IState currentState;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    public Vector3 MyStartPosition { get; set; }

    [SerializeField]
    private float initAggroRange;
    public float MyAggroRange { get; set; }

    private bool LeftLooking;

    [SerializeField]
    private LootTable lootTable;

    public bool  InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.position) < MyAggroRange;
        }
    }

    public Animator EnemyAnim { get => enemyAnim; set => enemyAnim = value; }
    public SpriteRenderer SlimeFacing { get => slimeFacing; set => slimeFacing = value; }
    public bool enemyLeftLooking { get => LeftLooking; set => LeftLooking = value; }

    protected void Awake()
    {
        enemyLeftLooking = slimeFacing.flipX;
        MyStartPosition = transform.position;
        MyAggroRange = initAggroRange;
        MyAttackRange = 2.0f;
        ChangeState(new IdleState());
    }

    public void Update()
    {
        if (!IsAttacking)
        {
            MyAttackTime += Time.deltaTime;
        }

        currentState.Update();
    }

    public Transform Select()
    {
        healthGroup.alpha = 1;

        return hitBox;
    }

    public  void Deselect()
    {
        healthGroup.alpha = 0;
         
    }



   
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }


    //play hurt animation in Character
    public IEnumerator isHurt()
    {
        
        Debug.Log("Slime is hurt!");

        EnemyAnim.SetBool("isHurt", true);

        yield return new WaitForSeconds(0.2f);

        EnemyAnim.SetBool("isHurt", false);
    }

    public override void TakeDamage(float damage, Transform source)
    {
        if (!(currentState is EvadeState))
        {
            SetTarget(source);
            StartCoroutine(isHurt());
            base.TakeDamage(damage, source);
        }

    }

    public void SetTarget(Transform target)
    {
        if (MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = initAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        MyTarget = null;
        this.MyAggroRange = initAggroRange;
        this.health.myCurrentValue = this.health.MyMaxValue;
        OnHealthChaneged(health.myCurrentValue);
        
    }

    public  void Interact()
    {
        if (IsDead)
        {
            List<Drop> drops = new List<Drop>();

            foreach (IInteractable interactable in Player.MyInstance.MyInteractables)
            {
                if (interactable is Enemy && !(interactable as Enemy).IsDead == false)
                {
                    drops.AddRange((interactable as Enemy).lootTable.GetLoot());
                }
            }

            LootWindow.MyInstance.CreatePages(drops);

        }
        
    }

    public  void StopInteract()
    {
        LootWindow.MyInstance.Close();
    }

}
