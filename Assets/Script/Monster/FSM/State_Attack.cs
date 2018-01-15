using UnityEngine;
using System.Collections;
using System;

public class State_Attack : FSM_State<Agent>
{
    static readonly State_Attack instance = new State_Attack();
    public static State_Attack Instance
    {
        get
        {
            return instance;
        }
    }

    private float AttackTimer = 0.0f;
    static State_Attack() { }
    private State_Attack() { }

    public override void EnterState(Agent _Monster)
    {
        if (_Monster.target == null)
            return;

        Debug.Log("Enter State Attack");

        _Monster.animator.SetBool("IsAttack", true);

        AttackTimer = _Monster.AttackSpeed;
    }
    public override void UpdateState(Agent _Monster)
    {
        if (_Monster.HP <= 0)
            _Monster.ChangeState(State_Die.Instance);

        //if(!CharacterState.IsDead && _Monster.CheckRange() && _Monster.CheckAngle())
        //{
        //    if(AttackTimer >= _Monster.AttackSpeed)
        //    {
        //        CharacterState.ReceiveDamage = _Monster.Damage;
        //        AttackTimer = 0.0f;
        //        _Monster.ChaseTime = 0.0f;
        //        Debug.Log("Attack Start");
        //    }
        //}
        
        if(_Monster.AttackCurrentTimer >= _Monster.AttackEndTimer)
        {
            _Monster.AttackCurrentTimer = 0.0f;
            _Monster.ChangeState(State_Idle.Instance);
            return;
        }
        else
        {
            _Monster.AttackCurrentTimer += Time.deltaTime;
        }
    }
    public override void ExitState(Agent _Monster)
    {
        Debug.Log("Exit State_Attack");
        _Monster.animator.SetBool("IsAttack", false);
    }
}
