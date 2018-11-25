using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour {
	public Slider volumeSlider;
	// Use this for initialization
	void Start () {
		if(Input.GetKeyUp(KeyCode.Space)) {
			Debug.Log("");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSliderChanged(float value)
	{
		//Debug.Log(value);
		volumeSlider.value = Mathf.Round(value * 10) / 10;
	}
}
