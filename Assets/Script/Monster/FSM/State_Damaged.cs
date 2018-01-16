using UnityEngine;
using System.Collections;

public class State_Damaged : FSM_State<Agent>
{

    static readonly State_Damaged instance = new State_Damaged();
    public static State_Damaged Instance
    {
        get
        {
            return instance;
        }
    }

    private float resetTime = 3.0f;
    private float currentTime;

    static State_Damaged() { }

    private State_Damaged()
    {

    }

    public override void EnterState(Agent _Monster)
    {
        _Monster.animator.SetBool("IsDamaged", true);
        _Monster.IsDead = true;
    }
    public override void UpdateState(Agent _Monster)
    {
        if(_Monster.CurrentHP <= 0)
        {
            _Monster.ChangeState(State_Die.Instance);
            return;
        }

        if(_Monster.idleTime <= _Monster.currentIdleTime)
        {
            _Monster.ChangeState(State_Idle.Instance);
            _Monster.currentIdleTime = 0.0f;
            return;
        }
        else
        {
            _Monster.currentIdleTime += Time.deltaTime;
        }
    }

    public override void ExitState(Agent _Monster)
    {
        _Monster.animator.SetBool("IsDamaged", false);
    }
}
