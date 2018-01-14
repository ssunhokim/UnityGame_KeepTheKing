using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CreateWalf : MonoBehaviour
{
    public GameObject[] WalfList;
    public GameObject Walf;

    private int CurrentRound;
    private List<GameObject> StoreWalfs;
    private bool InitWalfs = true;

    MonsterManager monsterManager;

    void Start()
    {
        StoreWalfs = new List<GameObject>();
        CurrentRound = MonsterManager.MonsterRoud;

        monsterManager = GetComponent<MonsterManager>();
    }

    public void CreateWalfs()
    {
        for (int i = 0; i < WalfList.Length; i++)
        {
            if (WalfList[i].tag == "MonsterPortal" + MonsterManager.MonsterRoud.ToString())
            {
                if(InitWalfs)
                {
                    GameObject obj = Instantiate(Walf);
                    obj.transform.position = WalfList[i].transform.position;
                    StoreWalfs.Add(obj);
                }
                else
                {
                    StoreWalfs[i].SetActive(true);                       
                }
            }
        }

        InitWalfs = false;
    }

    public void DeleteWalfs()
    {
        for(int i=0;i< StoreWalfs.Count;i++)
        {
            StoreWalfs[i].SetActive(false);

        }
    }
}