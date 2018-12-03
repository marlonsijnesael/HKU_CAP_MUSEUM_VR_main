using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

    public GameObject candle;
    public bool hasCandle;  
    CapsuleCollider candleCollider;
    Collider tempCollider;

	// Use this for initialization
	void Start () {

        candleCollider = candle.GetComponentInChildren<CapsuleCollider>();
        hasCandle = false;
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    IEnumerator ChangeParentsSlow()
    {

        if (tempCollider == candleCollider && !hasCandle)
        {

            candle.transform.parent = this.transform;
            yield return new WaitForSeconds(1.0f);
            hasCandle = true;

        }


    }

    private void OnTriggerEnter(Collider other)
    {

        print("collides");
        tempCollider = other;
        StartCoroutine("ChangeParentsSlow");
        //if (other == candleCollider && !hasCandle)
        //{
        //    candle.transform.parent = this.transform;
        //    hasCandle = true;

        //}


    }
}
