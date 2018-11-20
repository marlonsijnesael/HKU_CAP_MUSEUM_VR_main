using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionGetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        Color emission = mat.GetColor("_EmissionColor");
        
    }
}
