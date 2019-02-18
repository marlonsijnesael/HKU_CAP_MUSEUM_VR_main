using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour {

    public Material mat;
    public Light lt;
	
	void Update () {
        float _intesity = mat.GetFloat("_fire_brightness");
        float _sine = mat.GetFloat("_sineMul");
        float _speed = mat.GetFloat("_speed");
        lt.intensity = _intesity + Mathf.Sin(_sine * Time.time);
        mat.SetFloat("_emissionPower", _intesity + Mathf.Sin(_sine * (Time.time * _speed)));
    }
}
