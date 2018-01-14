using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    [SerializeField]
    public float distance = 10.0f;
    [SerializeField]
    public Transform target;
    [SerializeField]
    public float height = 5.0f;
    [SerializeField]
    public float rotationDamping;
    [SerializeField]
    public float heightDamping;

	void LateUpdate ()
    {
        if (!target)
            return;

        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;
        
        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;
        
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping);
        
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.Rotate(Input.GetAxis("Mouse Y") * 10, 0, 0);
        transform.LookAt(target);
    }
}
