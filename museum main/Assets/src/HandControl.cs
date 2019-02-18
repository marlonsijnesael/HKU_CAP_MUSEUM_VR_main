using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour {

    Animator anim;
    bool isClosed;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        isClosed = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isClosed)
            {
                anim.Play("OpenAnimationR");
                isClosed = false;


            }
            else
            {
                anim.Play("GrabAnimationR");
                isClosed = true;
            }

            

        }
		
	}

    public void OpenHand()
    {
        if (isClosed)
        {
            anim.Play("OpenAnimationR");
            isClosed = false;


        }

    }

    public void GrabHand()
    {
        if (!isClosed)
        {
            anim.Play("GrabAnimationR");
            isClosed = false;


        }


    }
}
