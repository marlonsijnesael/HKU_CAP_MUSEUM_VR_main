using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

    public GameObject candle;
    public GameObject candleFlame;
    public GameObject hand;
    public bool hasCandle;  
    Collider candleCollider;
    Collider tempCollider;
    AudioSource audioData;
    ControllerBehaviour controllerBehaviour;

    [FMODUnity.EventRef]
    public string f_event;
    //[FMODUnity.EventRef]
    //public string[] _events;

    [SerializeField]
    public FMOD.Studio.EventInstance f_Instance;

    // Use this for initialization
    void Start () {

        candleCollider = candle.GetComponentInChildren<Collider>();
        hasCandle = false;
        //audioData = GetComponent<AudioSource>();
        controllerBehaviour = hand.GetComponent<ControllerBehaviour>();
        

        //audioData.Play(0);

    }

    private void Awake()
    {
        f_Instance = FMODUnity.RuntimeManager.CreateInstance(f_event);

    }

    // Update is called once per frame
    void Update () {


		
	}

    bool IsInRange(float xAngle)
    {
        if((xAngle >= 355 && xAngle <= 360) || (xAngle >= 0 && xAngle <= 5))
        {
            print(xAngle);
            return true;
        }
        else
        {

            return false;
        }

    }

    IEnumerator ChangeParentSlow()
    {

        //if ((tempCollider == candleCollider) && !hasCandle && IsInRange(candle.transform.rotation.eulerAngles.z))
        if ((tempCollider == candleCollider) && !hasCandle)
            {

            candle.transform.parent = this.transform;
            candle.layer = 0;
            candleFlame.layer = 0;
            //audioData.Play(0);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(f_Instance, GetComponent<Transform>(), GetComponent<Rigidbody>());
            f_Instance.start();

            controllerBehaviour.OpenHand();
            yield return new WaitForSeconds(1.0f);
            hasCandle = true;
            print("Isin1");
            
         

        }


    }

    private void OnTriggerEnter(Collider other)
    {

        print("collides");
        tempCollider = other;
        StartCoroutine("ChangeParentSlow");
        //if (other == candleCollider && !hasCandle)
        //{
        //    candle.transform.parent = this.transform;
        //    hasCandle = true;

        //}


    }

    //public GameObject raySpawnPoint;  //add empty gameObject to right hand, place it a bit in front of the actual hand so it won't hit the actual hand

    //public bool holdingItem = false; //check if player is able to pick up item


    //public GameObject itemSlot; //empty gameobject, you parent the item to this object, you have to allign the orientation of this object with the handmodel
    //public GameObject item; //the actual item

    //private void CastRayFromHand()
    //{
    //    RaycastHit hit;
    //    if (!Physics.Raycast(raySpawnPoint.transform.position, raySpawnPoint.transform.forward, out hit))
    //        return;

    //    if (hit.transform.tag == "Candle" && !holdingItem)
    //    {
    //        //pass in the hit object to the grabItem function
    //        GrabItem(hit);
    //    }
    //}

    //public void GrabItem(RaycastHit hit)
    //{
    //    holdingItem = true;
    //    item = hit.transform.gameObject;
    //    item.transform.SetParent(itemSlot.transform);

    //    item.transform.rotation = itemSlot.transform.rotation;
    //    item.transform.position = itemSlot.transform.position;
    //    //Destroy(item.GetComponent<Rigidbody>());
    //}
}
