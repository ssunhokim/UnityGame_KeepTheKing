using UnityEngine;
using System.Collections;

public class OptionExpandMiniMap : MonoBehaviour
{
    public GameObject ExpandMinimap;        // 미니맵 UI 프리팹 저장
    public GameObject CharacterObject;      // 케릭터 오브젝트
    public GameObject CharacterMinimap;   // 케릭터 위치
    public GameObject MonsterMiniMap;

    private bool IsShowMinimap;
    private GameObject ExpandObject;
    private GameObject CharacterMinimapObject;

    void Start()
    {
        IsShowMinimap = false;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(IsShowMinimap)
            {
                Destroy(ExpandObject);
                Destroy(CharacterMinimapObject);
                Cursor.visible = false;
            }
            else
            {
                ExpandObject = Instantiate(ExpandMinimap, transform);
                ExpandObject.transform.Translate(new Vector3(-700, -300, 0));
                CharacterMinimapObject = Instantiate(CharacterMinimap, transform);
                Cursor.visible = true;
            }

            IsShowMinimap = !IsShowMinimap;
        }

        if(IsShowMinimap)
        {
            CharacterMinimapObject.transform.position = CharacterObject.transform.position + Vector3.up * 30.0f;
            CharacterMinimapObject.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.0f, 1.0f, 255);
        }
    }
}
