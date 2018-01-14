using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterManager : MonoBehaviour
{
    class Monster
    {
        public GameObject MonsterObj;      // 몬스터의 오브젝트 저장
        public bool IsDead;     // 몬스터가 죽은 상태인지
    };

    public GameObject SkeletonWarrior;
    public List<GameObject> MonsterList;
    public List<GameObject> DestinationList;
    public float MonsterTimer = 30; // 몬스터 나오는 시간
    public float WalfTimer = 2;     // 워프 시간 초기화

    private CreateWalf walf;
    private float CurrentMonsterTimer;
    private float CurrentWalfTimer;
    private bool IsWalfDestroy = false;
    private int MonsterIndex = 0;
    private int mCount = 0;

    static public int MonsterRoud = 0;

    void Start()
    {
        walf = GetComponent<CreateWalf>();
        MonsterList = new List<GameObject>();

        CurrentMonsterTimer = MonsterTimer;
        CurrentWalfTimer = WalfTimer;
    }

    void Update()
    {
        if (MonsterRoud != 0 && CurrentMonsterTimer >= MonsterTimer)
        {
            CreateMonster();
            walf.CreateWalfs();
            IsWalfDestroy = true;
            CurrentMonsterTimer = 0.0f;
            CurrentWalfTimer = 0.0f;
        }
        else
        {
            CurrentMonsterTimer += Time.deltaTime;
            CurrentWalfTimer += Time.deltaTime;
        }

        if(WalfTimer < CurrentWalfTimer)
        {
            if (IsWalfDestroy)
            {
                walf.DeleteWalfs();
            }

            IsWalfDestroy = false;
        }
    }

    void CreateMonster()
    {
        for (int i = 0; i < walf.WalfList.Length; i++)
        {
            if (walf.WalfList[i].tag == ("MonsterPortal" + MonsterRoud.ToString()))
            {
                GameObject obj = Instantiate(SkeletonWarrior);
                obj.transform.position = walf.WalfList[i].transform.position;
                MonsterList.Add(obj);       // 생성자 호출을 한다.

                if (walf.WalfList[i].tag == "MonsterPortal1")
                    MonsterList[MonsterIndex].transform.Rotate(0, -90.0f, 0);

            }
            else
            {
                i--;
            }

            MonsterIndex++;
        }

        mCount++;
    }
}
