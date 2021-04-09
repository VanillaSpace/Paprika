using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicMovement : MonoBehaviour
{

    public bool isBusy = false;

    public Transform MyTarget { get; set; }

    public static BasicMovement instance;

    public static BasicMovement MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BasicMovement>();
            }
            return instance;
        }

    }

    public int ExitIndex { get => exitIndex; set => exitIndex = value; }
    
    public Animator animator;

    [SerializeField]
    private Blocks[] blocks;

    [SerializeField]
    private Transform[] exitPoint;

    private int exitIndex = 0;

    private Coroutine initRoutine;

    private string animNames;

    public string MyAnimNames { get => animNames; set => animNames = value; }
    public Coroutine MyInitRoutine { get => initRoutine; set => initRoutine = value; }

    [SerializeField]
    private Profession profession;


    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator AttackRoutine(ICastable castable)
    {
        Transform currentTarget = MyTarget;

        yield return MyInitRoutine = StartCoroutine(ActionRoutine(castable));

        if (MyTarget != null && InLineOfSight())
        {
            Projectile newProjectile = projectileBook.MyInstance.GetProjectile(castable.MyTitle);

            ProjectileScript s = Instantiate(newProjectile.MyProjectilePrefab, exitPoint[ExitIndex].position, Quaternion.identity).GetComponent<ProjectileScript>();
            s.Initialize(MyTarget, newProjectile.MyDamage, transform);
        }
               
    }
    private IEnumerator GatherRoutine(ICastable castable, List<Drop> items)
    {

        yield return MyInitRoutine = StartCoroutine(ActionRoutine(castable));

        LootWindow.MyInstance.CreatePages(items);

        Player.MyInstance.MyStamina.myCurrentValue -= 20f;

     }

    public IEnumerator CraftRoutine(ICastable castable)
    {
        yield return MyInitRoutine = StartCoroutine(ActionRoutine(castable));

        profession.AddItemToInventory();
    }

    private IEnumerator ChopRoutine(ICastable castable)
    {

        yield return MyInitRoutine = StartCoroutine(ActionRoutine(castable));

        Player.MyInstance.MyStamina.myCurrentValue -= 30f;

    }

    private IEnumerator ActionRoutine(ICastable castable)
    {

        projectileBook.MyInstance.Cast(castable);

        animCast(castable);

        isBusy = true;
        animator.SetBool(MyAnimNames, true);

        yield return new WaitForSeconds(castable.MyCastTime);

        animator.SetBool(MyAnimNames, false);
        isBusy = false;
        
    }

    public void CastProjectile(ICastable castable)
    {

        Block();

            if (!isBusy && MyTarget != null && (Enemy.MyInstance.IsDead == false) && InLineOfSight())
            {
                
                MyInitRoutine = StartCoroutine(AttackRoutine(castable));

            }

             else 
            {
                if(Enemy.MyInstance.IsDead is true)
                {
                    Debug.Log("Enemy is dead");
                    
                    
                }
                else if (MyTarget is null)
                {
                    Debug.Log("No target selected");
                }
                else if (!InLineOfSight())
                {
                    Debug.Log("Cannot See");
                }
            }
  
    }

    public void Gather(ICastable castable,  List<Drop> items)
    {
        if (!isBusy)
        {
            MyInitRoutine = StartCoroutine(GatherRoutine(castable, items));
        }
    }

    public void Chop(ICastable castable)
    {
        if (!isBusy)
        {
            MyInitRoutine = StartCoroutine(ChopRoutine(castable));
        }
    }

    private bool InLineOfSight()
    {
        if (MyTarget != null)
        {
            Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

            if (hit.collider == null)
            {
                return true;
            }
        
        }
        return false;
    }

    public void Block()
    {
        foreach(Blocks b in blocks)
        {
            b.Deactivate();
        }

        blocks[ExitIndex].Activate();
    }

    private void StopInit()
    {
        if (MyInitRoutine != null)
        {
            StopCoroutine(MyInitRoutine);
        }
    }

    public void StopAction()
    {
        projectileBook.MyInstance.stopCasting();

        isBusy = false;

        if (MyInitRoutine !=null)
        {
            StopCoroutine(MyInitRoutine);
        }
    }

    //Calls the different animations
    public void animCast(ICastable castable)
    {
        if (castable.MyTitle == "FIRE DART" || castable.MyTitle == "FROST DART" || castable.MyTitle == "THUNDER DART")
        {
            MyAnimNames = "isRoll";
        }
        else if (castable.MyTitle == "Gather")
        {
            MyAnimNames = "isGathering";
        }
        else if (castable.MyTitle == "Chop")
        {
            MyAnimNames = "isChopping";
        }

    }

}
