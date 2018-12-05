using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject candle;
    CapsuleCollider candleCollider;

    GameObject target;
    TargetBehaviour targetBehaviour;
    BoxCollider targetCollider;

    Collider tempCollider;

	// Use this for initialization
	void Start () {

        candleCollider = candle.GetComponentInChildren<CapsuleCollider>();

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
}
