using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumManager : MonoBehaviour {

    GameObject resultingPainting = null;
    public GameObject sphereCover;

    private void Awake()
    {
        resultingPainting = GameObject.Find("Layer 1");
        if (resultingPainting != null)
        {
            resultingPainting.transform.position = new Vector3(1.574f, 1.312f, 1.752f);
            resultingPainting.transform.eulerAngles = new Vector3(-270.485f, -167.587f, -52.258f);
            resultingPainting.transform.localScale = new Vector3(0.200486f, 0.2004861f, 0.1390231f);

        }
        StartCoroutine(FadeOutBlack());

    }

    // Use this for initialization
    void Start () {
		
        
	}

    IEnumerator FadeOutBlack()
    {

        Color tcolor1;


        tcolor1 = sphereCover.GetComponent<Renderer>().material.color;

        for (float f = 1.0f; f >= 0; f -= 0.0070f)
        {
            tcolor1 = sphereCover.GetComponent<Renderer>().material.color;

            tcolor1.a = f;

            sphereCover.GetComponent<Renderer>().material.color = tcolor1;
            yield return new WaitForSeconds(0.0025f);

        }





    }

    // Update is called once per frame
    void Update () {
		
	}
}
