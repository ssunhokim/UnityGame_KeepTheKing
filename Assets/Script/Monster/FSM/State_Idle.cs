using UnityEngine;
using UnityEditor;

public class State_Idle : FSM_State<Agent>
{
    static readonly State_Idle instance = new State_Idle();
    public static State_Idle Instance
    {
        get
        {
            return instance;
        }
    }

    static State_Idle() { }
    private State_Idle() { }

    public override void EnterState(Agent _Monster)
    {
        Debug.Log("Enter Idle Enter");
        _Monster.target = null;
    }
    public override void UpdateState(Agent _Monster)
    {
        if(_Monster.idleTime <= _Monster.currentIdleTime)
        {
            _Monster.NavigateTo(new Vector3(12.4f, 0, -193.6f));
            _Monster.currentIdleTime = 0.0f;

            _Monster.ChangeState(State_Move.Instance);
            return;
        }
        else
        {
            _Monster.currentIdleTime += Time.deltaTime;
        }
    }

    public override void ExitState(Agent _Monster)
    {
        Debug.Log("Monster Idle Exit");
        _Monster.ResetCurrentPath();
        _Monster.currentIdleTime = 0.0f;
    }
}