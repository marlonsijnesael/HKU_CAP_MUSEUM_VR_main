using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {

    private GameObject manager; //manager gameobject that handles general flow of application
    public int textIndex;// index to define wich word/sentence will be received from manager
    Text displayText;//the text element where the sentence will be displayed in
    LanguageManager languageManager;//manager with all language sentences

	//Adds the reference of this text to the languagemanager list
	void Start () {
        displayText = transform.GetChild(0).GetComponent<Text>();
        manager = GameObject.Find("Manager");
        languageManager = manager.GetComponent<LanguageManager>();
        languageManager.AddGameText(this);
        displayText.text = languageManager.GetText(textIndex);
	}
	
	//update the text if language is changed is called from LanguageManager
    internal void UpdateTextLoaded()
    {
        displayText.text = languageManager.GetText(textIndex);
    }
}
