using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public float menuTimeVisable;
    private IEnumerator coroutine;
	public GameObject mainMenu;
	public GameObject settingMenu;

	public AudioMixer masterMixer;
    // Use this for initialization
    void Start () {
        //coroutine = WaitAndPrint(menuTimeVisable);
        //StartCoroutine(coroutine);
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
		Application.LoadLevel(1);
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

    private IEnumerator WaitAndPrint(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Done");
            //startMenuCanvas.SetActive(false);
            StopCoroutine(coroutine);
        }
    }
}
