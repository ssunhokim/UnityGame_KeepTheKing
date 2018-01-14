using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MakeRaddarObject : MonoBehaviour
{
    public Image image;

    void Start()
    {
        RadarMiniMap.RegisterRadarObject(this.gameObject, image);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        RadarMiniMap.RemoveRadarObject(this.gameObject);
    }
}
