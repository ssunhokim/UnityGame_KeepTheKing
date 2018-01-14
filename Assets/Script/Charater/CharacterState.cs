using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * static public -> 유니티에 나오질 않는다.
 */

public class CharacterState : MonoBehaviour {

    public Slider myHpBar;
    public Slider myMpBar;
    public Slider myStaminerBar;
    public Text myHpBarText;
    public Text myMpBarText;
    public Text myStaminerBarText;
    public Text receiveDamageTxt;

    public float MaxMP = 100;       // 최대 HP값
    public float MaxHP = 100;
    public float MaxStaminer = 100;
    public float RunSpend = 0.1f;       // 달릴 때 소모되는 스태미너 값
    public float ResetStaminer = 0.3f;  // 스태미너 리셋되는 값
    public float ResetStaminerTime = 0.3f;  // 리셋될때의 시간
    public float AttackSpend = 0.0f;        // 공격시 사용되는 스태미너 값
    public int Damage;      // 케릭터 기본공격 대미지(최소 공격 대미지)
    public int RandomDamage;    // 데미지 강도
    static public bool IsDead;
    static public int ReceiveDamage;        // 받은 데미지

    private float currentHP = 100;
    private float currentMP = 100;
    private float currentStaminer = 100;
    private float currentTime = 0;

    private Animator anim;
    private CharacterBehaviour character;
    bool IsTimePlus = true;

    static public bool IsStaminerZero = false;

    // Use this for initialization
    void Start () {
        currentHP = MaxHP;
        currentMP = MaxMP;
        currentStaminer = MaxStaminer;
        anim = GetComponent<Animator>();
        character = GetComponent<CharacterBehaviour>();
        Agent.SetTakeDamaged(Damage, RandomDamage);
    }
	
	// Update is called once per frame
	void Update () {
        IsTimePlus = false;

        myHpBar.value = currentHP / MaxHP;
        myMpBar.value = currentMP / MaxMP;
        myStaminerBar.value = currentStaminer / MaxStaminer;

        if (currentHP < 0)
            currentHP = 0;
        if (currentMP < 0)
            currentMP = 0;
        if (currentStaminer < 0)
            currentStaminer = 0;

        SetStaminerSize();
        CheckStaminer();
        CheckTime();
        SetText();
    }

    // 텍스트를 처리해주는 메서드
    void SetText()
    {
        int curHp = (int)currentHP;
        int curMp = (int)currentMP;
        int curStaminer = (int)currentStaminer;

        myHpBarText.text = curHp.ToString() + "/" + MaxHP.ToString();
        myMpBarText.text = curMp.ToString() + "/" + MaxMP.ToString();
        myStaminerBarText.text = curStaminer.ToString() + "/" + MaxMP.ToString();
    }

    // 스태미너를 조절해준다.
    void SetStaminerSize()
    {
        if (anim.GetBool("IsRun"))
            currentStaminer -= RunSpend;
        else if(anim.GetBool("IsAttack"))
        {
            
        }
        else if (!anim.GetBool("IsRun") && !anim.GetBool("IsAttack"))
        {
            if (currentTime > ResetStaminerTime)
            {
                currentStaminer += ResetStaminer;

                if (currentStaminer > MaxStaminer)
                    currentStaminer = MaxStaminer;
            }

            IsTimePlus = true;
        }
    }

    // 스태미너를 체크해준다
    void CheckStaminer()
    {
        if(currentStaminer<=0)
        {
            currentStaminer = 0;
            if(anim.GetBool("IsRun"))
            {
                anim.SetBool("IsRun", false);
            }
            else if(anim.GetBool("IsAttack"))
            {
                anim.SetBool("IsAttack", false);
            }

            IsStaminerZero = true;
        }
        else
        {
            IsStaminerZero = false;
        }
    }

    void CheckTime()
    {
        if (IsTimePlus)
            currentTime += Time.deltaTime;
        else
            currentTime = 0.0f;
    }

    void AttackInit()
    {

    }
}
