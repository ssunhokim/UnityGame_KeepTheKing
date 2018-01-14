using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 내 주변에 있는 임팩트
public class SkillImpact : MonoBehaviour
{
    class SkillObject
    {
        public GameObject skillObj;     // 스킬 오브젝트
        public float endTime;       // 스킬 끝나는 시점
        public float startTime; // 시작 시점

        public SkillObject(GameObject obj, float time)
        {
            skillObj = obj;
            endTime = time;
            startTime = 0.0f;
        }
    }

    static public int SkillNumber;      // 스킬 넘버
    public GameObject[] gameSkill;      // 내 주변에 있는 임팩트 스킬
    public GameObject summonSkill;      // 만약 소환 스킬이라면!!
    public GameObject groundSkill;      // 만약 그라운드 스킬이라면
    private Animator anim;

    List<SkillObject> ListSkill = new List<SkillObject>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        foreach(SkillObject sk in ListSkill)
        {
            if(sk.startTime > sk.endTime)
            {
                ListSkill.Remove(sk);
                Destroy(sk.skillObj);
            }
            else
            {
                sk.startTime += Time.deltaTime;
            }
        }

        CreateSkillObject();
    }

    void CreateSkillObject()
    {
        if(SkillNumber > 0)
        {
            float delayTime = 0.0f;

            GameObject gameObj = Instantiate(gameSkill[SkillNumber - 1]);
            gameObj.transform.position = transform.position;

            if (SkillNumber == 1)       // 회오리 스킬
                delayTime = 10.0f;
            else if (SkillNumber == 2)  // 소환 스킬
            {
                delayTime = 15.0f;
            }
            else if (SkillNumber == 3)  // 소환 스킬
            {
                delayTime = 4.5f;
                GameObject Obj = Instantiate(groundSkill);
                Obj.transform.position = transform.position;
                ListSkill.Add(new SkillObject(Obj, delayTime));
            }


            ListSkill.Add(new SkillObject(gameObj, delayTime));

            SkillNumber = 0;
        }
    }
}
