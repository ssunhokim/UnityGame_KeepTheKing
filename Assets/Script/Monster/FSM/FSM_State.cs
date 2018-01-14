using UnityEngine;
using System.Collections;

abstract public class FSM_State<T>
{
    abstract public void EnterState(T _Monster);
    abstract public void UpdateState(T _Monster);
    abstract public void ExitState(T _Monster);
}
