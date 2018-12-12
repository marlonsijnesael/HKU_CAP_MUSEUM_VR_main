using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject candle;
    Collider candleCollider;

    GameObject target;
    TargetBehaviour targetBehaviour;
    BoxCollider targetCollider;
    Animator anim;
    bool isClosed;

    Collider tempCollider;

	// Use this for initialization
	void Start () {

        candleCollider = candle.GetComponentInChildren<Collider>();
        anim = GetComponent<Animator>();
        isClosed = false;
        GrabHand();

    }
	
	// Update is called once per frame
	void Update () {


		
	}

    IEnumerator ChangeParentsSlow()
    {
 

        if (tempCollider == candleCollider)
        {
            target = candle.transform.parent.gameObject;
            targetBehaviour = target.GetComponent<TargetBehaviour>();
            targetCollider = target.GetComponent<BoxCollider>();

            if (targetBehaviour.hasCandle)
            {
                GrabHand();
                candle.transform.parent = this.transform.parent;
                yield return new WaitForSeconds(1.0f);
                targetBehaviour.hasCandle = false;
                
            }

            

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        tempCollider = other;
        StartCoroutine("ChangeParentsSlow");
        //print("collides");
        //if (other == candleCollider && !hasCandle)
        //{
        //    candle.transform.parent = this.transform;
        //    hasCandle = true;

        //}


    }

    public void OpenHand()
    {
        print("Isin");
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
            isClosed = true;


        }


    }
}
