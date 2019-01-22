using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumManager : MonoBehaviour {

    GameObject resultingPainting = null;

    private void Awake()
    {
        resultingPainting = GameObject.Find("Layer 1");
        if (resultingPainting != null)
        {
            resultingPainting.transform.position = new Vector3(1.574f, 1.312f, 1.752f);
            resultingPainting.transform.eulerAngles = new Vector3(-270.485f, -167.587f, -52.258f);
            resultingPainting.transform.localScale = new Vector3(0.200846f, 0.2008461f, 0.1390231f);

        }
        
    }

    // Use this for initialization
    void Start () {
		
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
