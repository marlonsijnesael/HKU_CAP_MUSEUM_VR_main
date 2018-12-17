using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour {

    public Renderer rend;
    public Light lt;
    public int maxIntensity;
    public int brightnessMultiplier;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float brightness = brightnessMultiplier * Mathf.Abs(Mathf.Sin((Time.deltaTime *  maxIntensity) ));
        lt.intensity = brightness + 4;
        rend.material.SetFloat("_fire_brightness", brightness );
    }
}
