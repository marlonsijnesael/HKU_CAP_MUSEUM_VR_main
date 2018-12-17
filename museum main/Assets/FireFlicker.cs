using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlicker : MonoBehaviour {

    public Renderer rend;
    public Light lt;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        float brightness = Mathf.Abs(Mathf.Sin((Time.deltaTime * 10) * 10));
        lt.intensity *= brightness;
        rend.material.SetFloat("_fire_brightness", brightness);
    }
}
