using UnityEngine;
using System.Collections;

public class MonsterStateMachine<T>
{
    private T Owner;
    private FSM_State<T> CurrentState;
    private FSM_State<T> PreviousState;

    public void Awake()
    {
        CurrentState = null;
        PreviousState = null;
    }

    public void ChangeState(FSM_State<T> newState)
    {
        if (newState == CurrentState)
            return;

        PreviousState = CurrentState;

        if (CurrentState != null)
            CurrentState.ExitState(Owner);

        CurrentState = newState;

        if (CurrentState != null)
            CurrentState.EnterState(Owner);
    }
    
    // 초기상태설정
    public void Initial_Setting(T Owner, FSM_State<T> InitialState)
    {
        this.Owner = Owner;
        ChangeState(InitialState);
    }

    // 상태 업데이트
    public void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateState(Owner);
    }

    // 이전 상태 회귀
    public void StateRevert()
    {
        if (PreviousState != null)
            ChangeState(PreviousState);
    }
}
