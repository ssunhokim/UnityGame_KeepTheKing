using UnityEngine;
using System.Collections;

public class MonsterFloatText : MonoBehaviour
{
    private Vector3 position;
    private Vector3 screenPointPosition;
    private Camera cameraHold;
    private string text;

    void Start()
    {
        cameraHold = Camera.main;
        screenPointPosition = cameraHold.WorldToScreenPoint(position);
    }

    void Update()
    {
        screenPointPosition.y -= 1;
    }

    public static void ShowMessage(string text, Vector3 pos)
    {
        var newInstance = new GameObject("Damage Popup");
        var damagePopup = newInstance.AddComponent<MonsterFloatText>();
        damagePopup.position = pos;
        damagePopup.text = text;
    }

    private void OnGUI()
    {
        var screenPX = cameraHold.WorldToScreenPoint(position);

        GUIStyle gui = new GUIStyle();
        gui.fontSize = 80;
        gui.normal.textColor = Color.red;

        GUI.Label(new Rect(screenPX.x,
            screenPointPosition.y, 500, 200),text,gui);

        Destroy(gameObject,1);
    }
}
