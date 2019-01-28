using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public GameObject MainMenuPanel;
    public GameObject SettingMenuPanel;
    public GameObject VolumeSlider;
    private float masterVolume;
    //0 Play
    //1 Settings
    //2 Sound

    private int languageIndex = 1;
    private List<string> eng_Text = new List<string>(new string[] {"Play","Settings","Sound" });
    private List<string> nl_Text = new List<string>(new string[] { "Start", "Instellingen", "Geluid" });

    private List<LoadText> gameText = new List<LoadText>();
    // Use this for initialization
    void Start () {
        if(PlayerPrefs.GetFloat("MasterVolume")==null)
        {
            masterVolume = 1;
        }
        else
        {
           masterVolume = VolumeSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MasterVolume");
        }
        SetMasteVolume(masterVolume);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackMainMenu()
    {
        MainMenuPanel.SetActive(true);
        SettingMenuPanel.SetActive(false);
    }

    public void SetMasteVolume(float f)
    {
        masterVolume = f;
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.Save();
    }



    public void ShowSettings(bool mainMenu, bool settingsMenu)
    {
        MainMenuPanel.SetActive(mainMenu);
        SettingMenuPanel.SetActive(settingsMenu);
    }
}
