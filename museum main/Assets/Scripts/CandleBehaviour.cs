using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {


        int i = 0;

        foreach (ContactPoint contact in collision.contacts)
        {
            print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
            // Visualize the contact point
            //Debug.DrawRay(contact.point, contact.normal, Color.white);
        }


    }
}
