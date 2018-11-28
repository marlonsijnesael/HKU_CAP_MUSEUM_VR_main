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
		if(Input.GetKeyUp(KeyCode.Space))
        {
           // SetSound(-15);
        }
	}

    public void SetSound(float soundLevel)
    {
        Debug.Log("why this no work");
        masterMixer.SetFloat("musicVol", soundLevel);
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
