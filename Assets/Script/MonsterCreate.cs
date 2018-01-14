using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreate : MonoBehaviour {

    public int[] MonsterCount;              // 라운드 별 몬스터 갯수
    public GameObject SkeletonWarrior;

    private List<GameObject> MonsterList;

    private int currentRound;
    private int PortalCreateFlag;

	void Start () {
        currentRound = MonsterManager.MonsterRoud;
        PortalCreateFlag = 0;
    }

	void Update () {

        GetComponent<MonsterCreate>();

        if(currentRound != MonsterManager.MonsterRoud)
        {
            switch (MonsterManager.MonsterRoud)
            {
                case 1:
                    Round1Create();
                    break;
                case 2:
                    Round2Create();
                    break;
                case 3:
                    Round3Create();
                    break;
                case 4:
                    Round4Create();
                    break;
                default:
                    break;
            }
        }
    }

    void Round1Create()
    {

    }

    void Round2Create()
    {

    }

    void Round3Create()
    {

    }

    void Round4Create()
    {

    }
}
