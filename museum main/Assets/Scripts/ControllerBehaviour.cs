using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject candle;
    public GameObject candleFlame;
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
        //print(transform.rotation.eulerAngles.z);
	}

    bool IsInRange(float Angle)
    {
        if ((Angle >= 240) && (Angle <= 290))
        {
            //print(Angle);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool HandInPosition()
    {
        return true;
    }

    IEnumerator ChangeParentsSlow()
    {
        //if ((tempCollider == candleCollider) && IsInRange(transform.rotation.eulerAngles.z))
        //{
        //    target = candle.transform.parent.gameObject;           
        //    targetBehaviour = target.GetComponent<TargetBehaviour>();
        //    targetCollider = target.GetComponent<BoxCollider>();

        //    if (targetBehaviour.hasCandle)
        //    {
        //        GrabHand();
        //        candle.transform.parent = this.transform.parent;
        //        candle.layer = LayerMask.NameToLayer("Controllers");
        //        candleFlame.layer = LayerMask.NameToLayer("Controllers");
        //        yield return new WaitForSeconds(1.0f);
        //        targetBehaviour.hasCandle = false;
        //    }
        //}
        if ((tempCollider == candleCollider))
        {
            target = candle.transform.parent.gameObject;
            targetBehaviour = target.GetComponent<TargetBehaviour>();
            targetCollider = target.GetComponent<BoxCollider>();

            if (targetBehaviour.hasCandle)
            {
                GrabHand();
                candle.transform.parent = this.transform.parent;
                candle.transform.localEulerAngles = new Vector3(-2.966f, -57.243f, -4.463f);
                candle.transform.localPosition = new Vector3(-0.0336f, -0.3578f, 0.0189f);
                candle.layer = LayerMask.NameToLayer("Controllers");
                candleFlame.layer = LayerMask.NameToLayer("Controllers");
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
        //print("Isin");
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
