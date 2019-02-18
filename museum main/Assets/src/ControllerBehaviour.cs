using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBehaviour : MonoBehaviour {

    public GameObject candle;
    public GameObject candleFlame;
    Collider candleCollider;
    Vector3 candlePosition;
    Vector3 candleRotation;


    GameObject target;
    TargetBehaviour targetBehaviour;
    BoxCollider targetCollider;
    
    Animator anim;
    bool isClosed;
    bool isBeginning;

    Collider tempCollider;

	// Use this for initialization
	void Start () {
        candleCollider = candle.GetComponentInChildren<Collider>();
        anim = GetComponent<Animator>();
        isClosed = false;
        GrabHand();


        candlePosition = new Vector3(-0.05f, -0.18f, -0.01f);
        candleRotation = new Vector3(9.3f, 0f, -20.78f);
        isBeginning = true;
        


    }
	
	// Update is called once per frame
	void Update () {
        //print(transform.rotation.eulerAngles.z);

        if (isBeginning)
        {
            if ((candle.transform.localEulerAngles != candleRotation) || (candle.transform.localPosition != candlePosition))
            {
                candle.transform.localEulerAngles = candleRotation;
                candle.transform.localPosition = candlePosition;

            }
            isBeginning = false;

        }
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
        //        candle.transform.localEulerAngles = candleRotation;
        //        candle.transform.localPosition = candlePosition;
        //        candle.layer = LayerMask.NameToLayer("Controllers");
        //        candleFlame.layer = LayerMask.NameToLayer("Controllers");
        //        yield return new WaitForSeconds(0.5f);
        //        targetBehaviour.hasCandle = false;
        //    }
        //}
        if (!isBeginning)
        {
            if ((tempCollider == candleCollider))
            {
                target = candle.transform.parent.gameObject;
                targetBehaviour = target.GetComponent<TargetBehaviour>();
                targetCollider = target.GetComponent<BoxCollider>();

                if (targetBehaviour.hasCandle)
                {
                    GrabHand();
                    candle.transform.parent = this.transform.parent;
                    candle.transform.localEulerAngles = candleRotation;
                    candle.transform.localPosition = candlePosition;
                    candle.layer = LayerMask.NameToLayer("Controllers");
                    candleFlame.layer = LayerMask.NameToLayer("Controllers");
                    yield return new WaitForSeconds(1.0f);
                    targetBehaviour.hasCandle = false;
                }
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
