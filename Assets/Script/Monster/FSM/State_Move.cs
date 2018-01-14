using UnityEngine;
using System.Collections;
using System;

public class State_Move : FSM_State<Agent>
{
    private bool initNavigate = true;
    private bool IsWayPoint = true;     // 웨이포인트를 따라갈건인지 확인

    static readonly State_Move instance = new State_Move();
    public static State_Move Instance
    {
        get
        {
            return instance;
        }
    }

    private float resetTime = 3.0f;
    private float currentTime;

    static State_Move() { }
    private State_Move()
    {
        
    }

    public override void EnterState(Agent _Monster)
    {
        currentTime = resetTime;
        _Monster.animator.SetBool("IsMove", true);
    }
    public override void UpdateState(Agent _Monster)
    {
        // 죽음 유무를 확인
        if (_Monster.CurrentHP <= 0)
            _Monster.ChangeState(State_Die.Instance);

        // 타겟 구하기
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            // 공격 범위내 있는지 확인
            if ((obj.transform.position - _Monster.transform.position).magnitude <= _Monster.SensorRange)
            {
                RaycastHit hit;
                Vector3 dir = obj.transform.position - _Monster.transform.position;
                dir.Normalize();

                if (Physics.Raycast(_Monster.transform.position, dir * 8.0f, out hit, 8))
                {
                    _Monster.target = obj;      // 타겟 설정

                    if ((obj.transform.position - _Monster.transform.position).magnitude <= _Monster.AttackRange)
                    {
                        _Monster.ChangeState(State_Attack.Instance);
                        Debug.Log("플레이어 공격 범위 내 있음");
                        return;
                    }
                    else
                    {
                        _Monster.setMove = 1.0f;
                        Debug.Log("플레이어 센서 범위 내 있음");
                    }
                }
            }
            else
            {
                _Monster.setMove = 0.0f;
            }

            _Monster.animator.SetFloat("SetRun", _Monster.setMove);
        }

        if(_Monster.setMove > 0.0f)
        {
            Vector3 dir = _Monster.target.transform.position - _Monster.transform.position;
            dir.Normalize();

            _Monster.transform.forward = dir;
            _Monster.transform.position = _Monster.transform.position + 
                (dir * (_Monster.runSpeed) * _Monster.setMove) * 
                Time.smoothDeltaTime;
        }
        else
        {
            _Monster.PathMoveMent();
        }
    }
    public override void ExitState(Agent _Monster)
    {
        Debug.Log("Exit State Move");
        _Monster.animator.SetBool("IsMove", false);

        _Monster.setMove = 0.0f;
    }

    void SetRandDir(Agent monster)
    {
        currentTime += Time.smoothDeltaTime;
        if(currentTime >= resetTime)
        {
            monster.transform.forward = Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360f), Vector3.up) * Vector3.forward;
            resetTime = UnityEngine.Random.Range(1, 4);
            currentTime = 0.0f;
        }
    }
}

