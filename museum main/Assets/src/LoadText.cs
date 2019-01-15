using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {

    private GameObject manager;
    public int textIndex;
    Text displayText;
    LanguageManager languageManager;

	// Use this for initialization
	void Start () {
        displayText = transform.GetChild(0).GetComponent<Text>();
        manager = GameObject.Find("Manager");
        languageManager = manager.GetComponent<LanguageManager>();
        languageManager.AddGameText(this);
        displayText.text = languageManager.GetText(textIndex);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void UpdateTextLoaded()
    {
        displayText.text = languageManager.GetText(textIndex);
    }
}
