using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        RayCast();
	}

    private void RayCast()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "PaintingButton")
            {
                GameObject.Find("Manager").GetComponent<MenuManager>().ShowSettings(true, false);
               
            }

            else if (hit.transform.tag == "SettingsButton")
            {
                GameObject.Find("Manager").GetComponent<MenuManager>().ShowSettings(false, true);
            }

            else if (hit.transform.tag == "StartButton")
            {
                GameObject.Find("Manager").GetComponent<MenuManager>().StartGame();
            }
        }

    }
}
