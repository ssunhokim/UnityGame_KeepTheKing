using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*
* Translate
* 상대좌표를 기준으로 움직임
* 매 프레임마다 값 만큼 움직임
* 현재 위치를 기준으로 x,y,z 좌표가 값에 따라 이동한다.
*/

/* Time.deltaTime
 * 전 프레임에서 현제 프레임 간의 사이 시간
 */

public class Agent : MonoBehaviour
{
    public int HP;      // HP설정
    public int CurrentHP;   // 현재 HP
    public int Damage;  // 데미지 설정
    public float AttackSpeed;
    public float AttackRange;   // 공격범위내 있는지 확인
    public float AttackEndTimer;
    public float AttackCurrentTimer;
    public float SensorRange;   // 센서 범위내 있는 지 확인
    public float ChaseTime = 0.0f;
    public float ChaseCancelTime = 8.0f;
    public float walkSpeed;     // 걷기 속도
    public float runSpeed;      // 달리기 속도
    public float setMove = 0.0f;
    public float idleTime = 3.0f;   // 유휴시간 정해주기
    public float currentIdleTime;   // 현재 유휴시간
    public bool IsDead = false;     // 죽었는지 확인
    public bool IsRun = false;

    // 웨이포인트 적용시키는 변수들
    private Stack<Vector3> currentPath;
    private Vector3 currentWaypointPosition;
    private float moveTimeTotal;
    private float moveTimeCurrent;
    private Rigidbody rigidBody;
    public Animator animator;   // 몬스터 애니메이터
    public GameObject target;   // 타겟 설정
    public Image HPBarImg;

    protected MonsterStateMachine<Agent> monsterState;      //몬스터 상태전이(FSM)
    public List<GameObject> WalfList;       // 워프들 위치를 받게 한다.

    void Awake () {
        animator = gameObject.GetComponent<Animator>();
        monsterState = new MonsterStateMachine<Agent>();
        transform.Rotate(0, Mathf.PI / 2, 0);       // 몬스터 돌게 하기

        ResetState();
        CurrentHP = HP;

        foreach (var list in GameObject.FindGameObjectsWithTag("WayPoint"))
        {
            WalfList.Add(list);
        }

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Spawn"))
        {
            monsterState.Update();
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Spawn"))
        {
            Debug.Log("Path찾는 중");
            NavigateTo(new Vector3(12.4f, 0, -193.6f));
        }
    }

    public void ChangeState(FSM_State<Agent> state)
    {
        monsterState.ChangeState(state);
    }

    // 트리거 사용할 때 리지드 바디랑 캡슐 콜리더를 같이 사용해야 한다.

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "KingSword")
        {
            Debug.Log("몬스터 맞음");
            var character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterBehaviour>();
            if (character.IsBaseAttack)
            {
                var state = character.GetComponent<CharacterState>();

                int damaged = UnityEngine.Random.Range(state.Damage, state.Damage + state.RandomDamage);

                CurrentHP -= damaged;
                HPBarImg.fillAmount = (float)CurrentHP / HP;
                character.IsBaseAttack = false;

                MonsterFloatText.ShowMessage(damaged.ToString(), transform.position);

                monsterState.ChangeState(State_Damaged.Instance);
            }
        }
    }

    public bool CheckRange()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= AttackRange)
            return true;

        return false;
    }

    public bool CheckAngle()
    {
        if (Vector3.Dot(target.transform.position, transform.position) >= 0.5f)
            return true;

        return false;
    }

    // 정해진 길 따라서 움직임
    public void PathMoveMent()
    {
        if (currentPath != null && currentPath.Count > 0)
        {
            if (moveTimeCurrent < moveTimeTotal)
            {
                moveTimeCurrent += Time.deltaTime;
                if (moveTimeCurrent > moveTimeTotal)
                    moveTimeCurrent = moveTimeTotal;
                transform.position = Vector3.Lerp(currentWaypointPosition, currentPath.Peek(), moveTimeCurrent / moveTimeTotal);
                transform.LookAt(currentPath.Peek());
            }
            else
            {
                currentWaypointPosition = currentPath.Pop();
                if (currentPath.Count == 0)
                    Stop();
                else
                {
                    moveTimeCurrent = 0.0f;
                    moveTimeTotal = (currentWaypointPosition - currentPath.Peek()).magnitude / walkSpeed;
                }
            }
        }
    }

    public void ResetState()
    {
        monsterState.Initial_Setting(this, State_Move.Instance);
        target = null;
    }

    public void NavigateTo(Vector3 destination)
    {
        currentPath = new Stack<Vector3>();
        var currentNode = FindClosestWaypoint(this.transform.position);
        var endNode = FindClosestWaypoint(destination);

        if (currentNode == null || endNode == null || currentNode == endNode)
            return;

        var openList = new SortedList<float, MonsterWayPoint>();
        var closedList = new List<MonsterWayPoint>();

        openList.Add(0, currentNode);
        currentNode.previous = null;
        currentNode.distance = 0.0f;

        while (openList.Count > 0)
        {
            currentNode = openList.Values[0];
            openList.RemoveAt(0);

            var dist = currentNode.distance;
            closedList.Add(currentNode);

            if (currentNode == endNode)
                break;

            foreach (var neighbor in currentNode.neighbors)
            {
                if (closedList.Contains(neighbor) || openList.ContainsValue(neighbor))
                    continue;

                neighbor.previous = currentNode;
                neighbor.distance = dist + (neighbor.transform.position - currentNode.transform.position).magnitude;
                var distanceToTarget = (neighbor.transform.position - endNode.transform.position).magnitude;
                openList.Add(neighbor.distance + distanceToTarget, neighbor);
            }
        }

        if (currentNode == endNode)
        {
            while (currentNode.previous != null)
            {
                currentPath.Push(currentNode.transform.position);
                currentNode = currentNode.previous;
            }

            currentPath.Push(transform.position);
        }
    }

    public void Stop()
    {
        currentPath = null;
        moveTimeCurrent = 0;
        moveTimeTotal = 0;
    }

    private MonsterWayPoint FindClosestWaypoint(Vector3 target)
    {
        GameObject closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var waypoint in GameObject.FindGameObjectsWithTag("WayPoint"))
        {
            var dist = (waypoint.transform.position - target).magnitude;

            if (dist < closestDist)
            {
                closest = waypoint;
                closestDist = dist;
            }
        }

        if (closest != null)
        {
            return closest.GetComponent<MonsterWayPoint>();
        }

        return null;
    }

    public void ResetCurrentPath()
    {
        moveTimeCurrent = 0.0f;
        moveTimeTotal = 0.0f;
    }
}
