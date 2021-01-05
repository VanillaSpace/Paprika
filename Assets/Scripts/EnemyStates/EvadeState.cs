using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeState : IState
{
    private Enemy parent;


    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        parent.Direction = Vector2.zero;
        parent.Reset();
    }
        
    void IState.Update()
    {
        parent.Direction = (parent.MyStartPosition - parent.transform.position).normalized;
        
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.MyStartPosition, parent.Speed * Time.deltaTime);

        float distance = Vector2.Distance(parent.MyStartPosition, parent.transform.position);

        //if(parent.enemyLeftLooking)
        //{
        //    parent.SlimeFacing.flipX = false;
        //}
        //else
        //{
        //    parent.SlimeFacing.flipX = true;
        //}

        parent.SlimeFacing.flipX = (parent.enemyLeftLooking) ? false : true;

        if (distance <= 0)
        {
            //if (parent.enemyLeftLooking)
            //{
            //    parent.SlimeFacing.flipX = true;
            //}
            //else
            //{
            //    parent.SlimeFacing.flipX = false;
            //}

            parent.SlimeFacing.flipX = (parent.enemyLeftLooking) ? true : false;

            parent.ChangeState(new IdleState());
        }
    }
}
