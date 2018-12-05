using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

    public GameObject candle;
    public bool hasCandle;  
    Collider candleCollider;
    Collider tempCollider;

	// Use this for initialization
	void Start () {

        candleCollider = candle.GetComponentInChildren<Collider>();
        hasCandle = false;
		
	}
	
	// Update is called once per frame
	void Update () {


		
	}

    bool IsInRange(float xAngle)
    {
        if((xAngle >= 355 && xAngle <= 360) || (xAngle >= 0 && xAngle <= 5))
        {

            return true;
        }
        else
        {

            return false;
        }

    }

    IEnumerator ChangeParentsSlow()
    {

        if ((tempCollider == candleCollider) && !hasCandle && IsInRange(candle.transform.rotation.eulerAngles.x))
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
