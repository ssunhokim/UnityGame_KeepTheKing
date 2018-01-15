using UnityEngine;
using System.Collections;
using System;

public class State_Die : FSM_State<Agent>
{
    static readonly State_Die instance = new State_Die();
    public static State_Die Instance
    {
        get
        {
            return instance;
        }
    }

    float endCount = 2.0f;
    float currentTime = 0.0f;

    static State_Die() { }
    private State_Die() { }

    public override void EnterState(Agent _Monster)
    {
        _Monster.animator.SetBool("IsDie", true);
        _Monster.IsDead = true;
    }
    public override void UpdateState(Agent _Monster)
    {
        currentTime += Time.deltaTime;
        if(_Monster.isActiveAndEnabled && currentTime >= endCount)
        {
            _Monster.gameObject.SetActive(false);
            currentTime = 0.0f;
        }
    }

    public override void ExitState(Agent _Monster)
    {
        
    }
}
