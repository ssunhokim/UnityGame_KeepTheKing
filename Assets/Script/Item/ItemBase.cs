using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour {

    class ItemClass
    {
        public GameObject itemObj;
        public float startTime;
        public float endTime;

        public ItemClass(GameObject o, float end)
        {
            itemObj = o;
            endTime = end;
        }
    }

    public GameObject[] ItemObjs;
    public GameObject[] Items;
    private List<ItemClass> ItemLists;


    void Start ()
    {
        /*
         foreach (GameObject obj in ItemObjs)
        {
            GameObject go;

            if (obj.tag == "LifePotion")
            {
                go = Instantiate(Items[0]);
                go.transform.position = obj.transform.position + (Vector3.up * 0.5f);
            }
            else if(obj.tag == "ManaPotion")
            {
                go = Instantiate(Items[1]);
                go.transform.position = obj.transform.position + (Vector3.up * 0.5f);
            }
        }
        */
    }
	
	void Update () {
	}

    void CreateItem()       // 아이템 다시 생성
    {

    }
}
