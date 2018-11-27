using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public float menuTimeVisable;
    private IEnumerator startscreentimer;
	public GameObject mainMenu;
    public GameObject startScreen;
	public GameObject settingMenu;

	public AudioMixer masterMixer;

    
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

	public void SetMasterVolume(float mastervolume)
	{
		masterMixer.SetFloat("MaserVolume",mastervolume);
	}

	public void StartApp()
	{
        mainMenu.SetActive(false);
        startScreen.SetActive(true);
        startscreentimer = StartscreenTimer(5);
        StartCoroutine(startscreentimer);
	}

	public void OpenSettings()
	{
		mainMenu.SetActive(false);
		settingMenu.SetActive(true);
	}

	public void CloseSettings()
	{
		mainMenu.SetActive(true);
		settingMenu.SetActive(false);
	}

    private IEnumerator StartscreenTimer(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Application.LoadLevel(1);
        }
    }
}
