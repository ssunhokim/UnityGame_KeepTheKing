using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionMonsterTimer : MonoBehaviour
{
    public Text TextTimer;
    public float[] TimerData;

    private const string timerText = "Next Round\n";
    private const string RoundSize = "Round";
    private const string MonsterSize = "MonsterCount";
    private float currentCoolTime;
    private int currentRound; 

    void Start()
    {
        currentCoolTime = TimerData[0];
        currentRound = MonsterManager.MonsterRoud;
    }

    void Update()
    {
        int curTime;

        currentCoolTime -= Time.deltaTime;
        curTime = (int)currentCoolTime;

        if (curTime < 0)
        {
            if (currentRound == MonsterManager.MonsterRoud)
                MonsterManager.MonsterRoud++;

            TextTimer.text = RoundSize + MonsterManager.MonsterRoud.ToString() + "\n" + MonsterSize + " : " + MonsterManager.MonsterRoud.ToString();
        }
        else
        { 
            TextTimer.text = timerText + (curTime / 60).ToString() + " : " + (curTime % 60).ToString();
        }
    }
}
