using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour {

    public GameObject optionUI;
    private bool IsShowing;
    private GameObject optionObj;
    
	// Use this for initialization
	void Start () {
        IsShowing = false;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsShowing)
            {
                Destroy(optionObj);
                Cursor.visible = false;
            }
            else
            {
                optionObj = Instantiate(optionUI, transform);
                optionObj.transform.Translate(new Vector3(5000, 500, 0));
                Cursor.visible = true;
            }

            IsShowing = !IsShowing;
        }
	}

    public void OnClick()
    {
        
    }
}
