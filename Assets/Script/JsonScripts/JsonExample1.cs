using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class PlayerInfo
{
    public int ID;
    public string Name;
    public double Gold;
    public PlayerInfo(int id, string name, double gold)
    {
        ID = id;
        Name = name;
        Gold = gold;
    }
}

public class JsonExample1 : MonoBehaviour {

    public List<PlayerInfo> PlayerInfoList = new List<PlayerInfo>();

	void Start () {
        //SavePlayerInfo();
        LoadPlayerInfo();
	}
	
	void Update () {
		
	}

    public void LoadPlayerInfo()
    {
        Debug.Log("LoadPlayerInfo()");

        string jsonString;

        if(File.Exists(Application.dataPath + "/Resource/JsonTest/PlayerInfoData.json"))
        {
            jsonString = File.ReadAllText(Application.dataPath + "/Resource/JsonTest/PlayerInfoData.json");

            Debug.Log(jsonString);
            JsonData playerData = JsonMapper.ToObject(jsonString);

            for(int i=0;i<playerData.Count;i++)
            {
                Debug.Log(playerData[i]["ID"].ToString());
                Debug.Log(playerData[i]["Name"].ToString());
                Debug.Log(playerData[i]["Gold"].ToString());
            }
        }
    }

    public void SavePlayerInfo()
    {
        Debug.Log("SavePlayerInfo()");

        // 테스트용 데이터
        PlayerInfoList.Add(new PlayerInfo(1, "김선호1", 9994));
        PlayerInfoList.Add(new PlayerInfo(2, "김선호2", 2354));
        PlayerInfoList.Add(new PlayerInfo(3, "김선호3", 2345));
        PlayerInfoList.Add(new PlayerInfo(4, "김선호4", 3422));
        PlayerInfoList.Add(new PlayerInfo(5, "김선호5", 12552));
        PlayerInfoList.Add(new PlayerInfo(6, "김선호6", 11241));

        JsonData infoJson = JsonMapper.ToJson(PlayerInfoList);

        // 현재 어플리케이션 절대 경로
        Debug.Log(Application.dataPath);

        File.WriteAllText(Application.dataPath + "/Resource/JsonTest/PlayerInfoData.json",infoJson.ToString());
    }
}
