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
        float brightness = Mathf.Abs(Mathf.Sin((Time.deltaTime * maxIntensity) *brightnessMultiplier));
        lt.intensity = maxIntensity * brightness;
        rend.material.SetFloat("_fire_brightness", brightness);
    }
}
