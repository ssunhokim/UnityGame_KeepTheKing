using UnityEngine;
using System.Collections;

public class State_Run : FSM_State<Agent>
{
    static readonly State_Run instance = new State_Run();
    public static State_Run Instance
    {
        get
        {
            return instance;
        }
    }

    static State_Run() { }
    private State_Run() { }

    public override void EnterState(Agent _Monster)
    {
        Debug.Log("Enter Sensor Enter");
    }
    public override void UpdateState(Agent _Monster)
    {
        Vector3 dir = _Monster.target.transform.position - _Monster.transform.position;
        dir.Normalize();

        _Monster.transform.forward = dir;
        _Monster.transform.position += _Monster.transform.forward * Time.smoothDeltaTime * _Monster.runSpeed;

        if((_Monster.target.transform.position - _Monster.transform.position).magnitude <= _Monster.AttackRange)
        {
            CharacterState.ReceiveDamage = _Monster.Damage;
            _Monster.ChangeState(State_Attack.Instance);
            Debug.Log("공격 범위 내 있음");
            return;
        }
    }

    public override void ExitState(Agent _Monster)
    {
        Debug.Log("Monster Run Exit");
    }
}
