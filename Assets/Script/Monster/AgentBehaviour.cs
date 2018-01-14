using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Awake()
 * 실제 생성자
 * Start() -> Update() 함수 호출 직전에 1회 호출
 */
public class AgentBehaviour : MonoBehaviour {

    protected Agent agent;      // 몬스터의 실질적인 내용
    protected PathManager pathManager;      // 몬스터 경로 설정
    protected MonsterStateMachine<Agent> monsterState;      //몬스터 상태전이
    private Vector3 currentWaypointPosition;
    private float moveTimeTotal;
    private float moveTimeCurrent;

    private Animator animator;
    private float currentTime;
    public virtual void Awake()
    {
        agent = gameObject.GetComponent<Agent>();
        animator = gameObject.GetComponent<Animator>();
        pathManager = gameObject.GetComponent<PathManager>();
    }
	

    public float MapToRange(float rotation)
    {
        rotation %= 360.0f;

        if(Mathf.Abs(rotation) > 180.0f)
        {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else
                rotation -= 360.0f;
        }

        return rotation;
    }

    // 클래스의 방향값을 벡터로 변경
    public Vector3 GetOriAsVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;

        return vector.normalized;
    }

    void MonsterMoveMent()
    {
        if (pathManager.currentPath != null && pathManager.currentPath.Count > 0)
        {
            if (moveTimeCurrent < moveTimeTotal)
            {
                moveTimeCurrent += Time.deltaTime;
                if (moveTimeCurrent > moveTimeTotal)
                    moveTimeCurrent = moveTimeTotal;
                transform.position = Vector3.Lerp(currentWaypointPosition, pathManager.currentPath.Peek(), moveTimeCurrent / moveTimeTotal);
                transform.LookAt(pathManager.currentPath.Peek());
            }
            else
            {
                currentWaypointPosition = pathManager.currentPath.Pop();
                if (pathManager.currentPath.Count == 0)
                    pathManager.Stop();
                else
                {
                    moveTimeCurrent = 0.0f;
                    moveTimeTotal = (currentWaypointPosition - pathManager.currentPath.Peek()).magnitude / agent.walkSpeed;
                }
            }
        }
    }
}
