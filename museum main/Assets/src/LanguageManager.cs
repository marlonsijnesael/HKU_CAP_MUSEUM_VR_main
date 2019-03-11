using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour {
    private int languageIndex = 0; //default language
    private List<string> eng_Text = new List<string>(new string[] { "Play", "Settings", "Sound" });//all english text
    private List<string> nl_Text = new List<string>(new string[] { "Start", "Instellingen", "Geluid" });//all dutch text
    private List<LoadText> gameText = new List<LoadText>();//all LoadText references that gets filled during startup of scene

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        if(PlayerPrefs.HasKey("LanguageIndex"))
        {
            languageIndex = PlayerPrefs.GetInt("LanguageIndex");
        }
        else
        {
            PlayerPrefs.SetInt("LanguageIndex", languageIndex);
            PlayerPrefs.Save();
        }
	}

    //saves language index in playerprefs
    public void SetLanguageIndex(int index)
    {
        languageIndex = index;
        if (languageIndex < 0) languageIndex = 0;
        UpdateText();
        PlayerPrefs.SetInt("LanguageIndex", languageIndex);
        PlayerPrefs.Save();
    }

    //for all LoaText ellements in list get the text updated
    private void UpdateText()
    {
        for(int i =0; i<gameText.Count;i++)
        {
            gameText[i].UpdateTextLoaded();
        }
    }

    //saves LoadText reference to list at startup of scene
    public void AddGameText(LoadText _text)
    {
        gameText.Add(_text);
    }

    //retuns the right word/sentence based on id and curent languageindex
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
