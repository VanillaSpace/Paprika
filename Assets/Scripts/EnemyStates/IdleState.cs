using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class IdleState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
       
        this.parent.Reset();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        Debug.Log("Idle");

        //change into follow state if player is inside the circle
        if (parent.MyTarget != null)
        {
            parent.ChangeState(new FollowState());
        }
    }
}
