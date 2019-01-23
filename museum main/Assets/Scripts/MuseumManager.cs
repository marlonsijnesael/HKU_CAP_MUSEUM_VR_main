using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumManager : MonoBehaviour {

    GameObject resultingPainting = null;

    private void Awake()
    {
        resultingPainting = GameObject.Find("Easel");
        if (resultingPainting != null)
        {
            resultingPainting.transform.position = new Vector3(-3.425f, 0.0018f, -1.554f);
            resultingPainting.transform.eulerAngles = new Vector3(0.0f, -238.567f, 0.0f);
            resultingPainting.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
        
    }

    // Use this for initialization
    void Start () {
		
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
