using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/*
 * 데미지 띄우는 클래스
 */

public class FloatingText : MonoBehaviour {

    public Animator animator;
    private Text dmgText;

	void OnEnable ()
    {
        Debug.Log("Start Floating Text");

        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);

        dmgText = animator.GetComponent<Text>();

    }

	void Update () {
    }

    public void SetText(string txt)
    {
        animator.GetComponent<Text>().text = txt;
    }
}
