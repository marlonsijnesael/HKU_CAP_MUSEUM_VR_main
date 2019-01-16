using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {


    //1 Play
    //2 Settings
    //3 Sound
    private int languageIndex = 1;
    private List<string> eng_Text = new List<string>(new string[] { "Play", "Settings", "Sound" });
    private List<string> nl_Text = new List<string>(new string[] { "Start", "Instellingen", "Geluid" });
    private List<LoadText> gameText = new List<LoadText>();

    private void Awake()
    {
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

    public void UpdateText()
    {
        for(int i =0; i <gameText.Count;i++)
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
        switch (languageIndex)
        {
            case 0: return eng_Text[id]; 
            case 1: return nl_Text[id];
            default: return eng_Text[id];
        }
   
    }
}
