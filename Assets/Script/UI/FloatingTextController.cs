using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextController : MonoBehaviour {

    private static FloatingText popupDamageText;
    private static GameObject canvas;
    private GameObject popupTextPrefab;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");

        if (!popupDamageText)
            popupDamageText = Resources.Load<FloatingText>("Prefabs/Text/MonsterDamage");
        else
            Debug.Log("popupText is Null");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupDamageText);
        instance.transform.SetParent(canvas.transform, false);
        instance.SetText(text);
    }
}
