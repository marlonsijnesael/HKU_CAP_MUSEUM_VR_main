using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {
    private int languageIndex = 1;
    private List<string> eng_Text = new List<string>(new string[] { "Play", "Settings", "Sound" });
    private List<string> nl_Text = new List<string>(new string[] { "Start", "Instellingen", "Geluid" });
    private List<LoadText> gameText = new List<LoadText>();

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        languageIndex = PlayerPrefs.GetInt("LanguageIndex");
	}

    public void SetLanguageIndex(int index)
    {
        languageIndex = index;
        if (languageIndex < 0) languageIndex = 0;
        UpdateText();
        PlayerPrefs.SetInt("LanguageIndex", languageIndex);
        PlayerPrefs.Save();
    }

    private void UpdateText()
    {
        for(int i =0; i<gameText.Count;i++)
        {
            gameText[i].UpdateTextLoaded();
        }
    }

    public void AddGameText(LoadText _text)
    {
        gameText.Add(_text);
    }

    public string GetText(int id)
    {
        switch(languageIndex)
        {
            case 0: return eng_Text[id];break;
            case 1: return nl_Text[id]; break;
            default: return eng_Text[id]; break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
