using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject candle;
    public GameObject target;
    CapsuleCollider candleCollider;
    TargetBehaviour targetBehaviour;
    BoxCollider targetCollider;
    Collider tempCollider;

	// Use this for initialization
	void Start () {

        candleCollider = candle.GetComponentInChildren<CapsuleCollider>();
        targetBehaviour = target.GetComponent <TargetBehaviour>();
        targetCollider = target.GetComponent<BoxCollider>();
        
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    IEnumerator ChangeParentsSlow()
    {

        if (tempCollider == candleCollider && targetBehaviour.hasCandle)
        {

            candle.transform.parent = this.transform.parent;
            yield return new WaitForSeconds(1.0f);
            targetBehaviour.hasCandle = false;

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
}
