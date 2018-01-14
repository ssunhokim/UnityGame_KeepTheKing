using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadarObject
{
    public Image icon { get; set; }
    public GameObject owner { get; set; }
}

public class RadarMiniMap : MonoBehaviour {

    public Transform playerPos;
    public float mapScale = 2.0f;

    public static List<RadarObject> radObjects = new List<RadarObject>();

    public GameObject map;
    private bool IsTotalMap = false;

    public static void RegisterRadarObject(GameObject o, Image i)
    {
        Image image = Instantiate(i);
        radObjects.Add(new RadarObject() { owner = o, icon = image });
    }

    public static void RemoveRadarObject(GameObject o)
    {
        List<RadarObject> newList = new List<RadarObject>();
        for(int i=0;i<radObjects.Count;i++)
        {
            if (radObjects[i].owner == o)
            {
                Destroy(radObjects[i].icon);
                continue;
            }
            else
                newList.Add(radObjects[i]);
        }

        radObjects.RemoveRange(0, radObjects.Count);
        radObjects.AddRange(newList);
    }

    void DrawRaddarDots()
    {
        foreach(RadarObject ro in radObjects)
        {
            Vector3 raddarPos = (ro.owner.transform.position - playerPos.position);
            float distToObject = Vector3.Distance(playerPos.position, ro.owner.transform.position) * mapScale;
            float deltay = Mathf.Atan2(raddarPos.x, raddarPos.z) * Mathf.Rad2Deg - 270 - playerPos.eulerAngles.y;

            raddarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
            raddarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);

            ro.icon.transform.SetParent(this.transform);
            ro.icon.transform.position = new Vector3(raddarPos.x, raddarPos.z, 0) + this.transform.position;
        }
    }
	
	void Update () {
        DrawRaddarDots();

        // 탭키 눌렀을 때 전체적인 맵을 그리기
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            IsTotalMap = !IsTotalMap;

            if(IsTotalMap)
            {

            }
            else
            {

            }
        }
	}

    // 전체적인 맵 그리기
    void TotalMapDraw()
    {

    }
}
