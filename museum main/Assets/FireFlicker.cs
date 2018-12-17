using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour
{

    public Renderer rend;
    public Light lt;
    public float maxIntensity;
    public float maxRange;
    public int brightnessMultiplier;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
       
        lt.intensity =  maxIntensity + Mathf.Sin(((Time.time / 1.5f) * maxIntensity));
        lt.range = maxRange + Mathf.Sin(((Time.time / 1.5f) * maxRange)) / 5;
     
       
    }
}