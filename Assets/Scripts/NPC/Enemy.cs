using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
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

    public override Transform Select()
    {
        healthGroup.alpha = 1;

        return base.Select();
    }

    public override void Deselect()
    {
        healthGroup.alpha = 0;

        base.Deselect();
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

    public override void Interact()
    {
        if (IsDead)
        {
            Debug.Log("Looting Enemy");
            lootTable.ShowLoot();
        }
        
    }

    public override void StopInteract()
    {
        LootWindow.MyInstance.Close();
    }

}
