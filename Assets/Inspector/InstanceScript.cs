using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceScript : MonoBehaviour {

    public GameObject bulletObj = null;
    public float power = 500.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            // 새롭게 생성하는 인스턴시에이트
           // Instantiate(bulletObj, transform.position, transform.rotation); -> 배열 형태가 아닌 변수만

            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            Vector3 direction = new Vector3(0, 0.3f, 0.5f);

            bullet.GetComponent<Rigidbody>().AddForce(power * direction);

            Destroy(bullet, 1.0f);  // 1초 뒤에 뷸렛을 삭제한다.

            // this.gameObject.SendMessage("ASF"); -> 메서드 이름을 호출해준다.
            // SendMessage("TTTT", bullet);
        }
	}

    public void ASF()
    {
        Debug.Log("SendMessage");
    }
}
