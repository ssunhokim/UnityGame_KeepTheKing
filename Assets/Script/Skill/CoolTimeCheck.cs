using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CoolTimeCheck : MonoBehaviour {

    public Image[] CoolTimeImage;

    static public bool[] ListUse = new bool[4] { true, true, true, true };  // 초기상태 false
    static private int Count = 0;

	// Use this for initialization
	void Start () {
    }
	
	void Update () {

        for(int i=0;i<CoolTimeImage.Length;i++)
        {
            if (CoolTimeImage[i].fillAmount == 0.0f)
                ListUse[i] = true;           
            else
                ListUse[i] = false;
        }
	}
}
