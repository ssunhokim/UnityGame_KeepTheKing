using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonsterHpBar : MonoBehaviour
{
    public GameObject MonsterBar;
    private List<GameObject> ListObject;

    float ImageYPos;

    void Start()
    {
        ImageYPos = transform.position.y;
    }

    public void SetMonsterHpBar(float SetHpBar)
    {
        GameObject obj = MonsterBar;

        Image img = obj.GetComponentInChildren<Image>();
        img.fillAmount = SetHpBar;

        Transform ts = transform;
        var instance = Instantiate(obj, new Vector3(transform.position.x,ImageYPos,0),new Quaternion());

        ImageYPos += 20.0f;

        if (ImageYPos > transform.position.y + 120.0f)
            ImageYPos = transform.position.y;

        Debug.Log("Monster HP Bar" + SetHpBar.ToString());

        //ListObject.Add(instance);
    }
}
