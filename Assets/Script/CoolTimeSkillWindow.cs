using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 쿨타임 이미지를 표시할 클래스
public class CoolTimeSkillWindow : MonoBehaviour
{
    public Image imgCoolTime;   // 쿨타임 필터링 이미지
    public Text coolTimeCounter;    // 남은 쿨타임을 표시

    public float coolTime;      // 현재 쿨타임
    
    private float currentCoolTime; // 남은 쿨타임을 추적할 변수
    private bool canUseSkill = true;    // 초기는 스킬을 사용할 수 있으므로

    void Start()
    {
        imgCoolTime.fillAmount = 0.0f;
    }

    void Update()
    {
        // 스킬을 사용할 수 있는 상태라면
        if(canUseSkill)
        {
            coolTimeCounter.text = "";
        }

        string WindowStr = "SkillWindow";

        if (coolTimeCounter.tag == WindowStr + SkillImpact.SkillNumber.ToString())
        {
            if(canUseSkill)
            {
                UseSkill();
            }
        }

    }

    // 스킬 사용 메서드
    public void UseSkill()
    {
        if(canUseSkill)
        {
            imgCoolTime.fillAmount = 1.0f;
            StartCoroutine("Cooltime");

            currentCoolTime = coolTime;
            coolTimeCounter.text = "" + currentCoolTime;

            StartCoroutine("CoolTimeCounter");

            canUseSkill = false;
        }
    }

    IEnumerator Cooltime()
    {
        while(imgCoolTime.fillAmount > 0)
        {
            imgCoolTime.fillAmount -= 1 * Time.deltaTime / coolTime;
            yield return null;
        }

        canUseSkill = true;     // 스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로
        yield break;
    }

    IEnumerator CoolTimeCounter()
    {
        while(currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounter.text = "" + currentCoolTime;
        }

        yield break;
    }
}
