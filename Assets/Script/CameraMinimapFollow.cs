using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMinimapFollow : MonoBehaviour {

    public Transform Target;
	
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        transform.rotation = Quaternion.Euler(90f, Target.eulerAngles.y, 0f);
    }
}
