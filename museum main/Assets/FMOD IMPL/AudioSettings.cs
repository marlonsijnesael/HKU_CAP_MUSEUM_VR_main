using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// singleton class which serves the main audio mixer
/// </summary>
public class AudioSettings : MonoBehaviour {

    FMOD.Studio.Bus masterBus;
    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus oneShotBus;

    public string pathToMasterBus;
    public string pathToMusicBus;
    public string PathToOneShotBus;

    [Header("Sound Settings")]
    [HideInInspector]
    float masterVolume = 1.0f;
    [HideInInspector]
    public float musicVolume = 0.5f;
    [HideInInspector]
    public float oneShotsVolume = 0.5f;

    public static AudioSettings _instance;

    public GameObject UI;
    public KeyCode key;
    private bool activeUI = false;

    private void Awake()
    {
        //singleton reference//
        #region singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion
        //-------------------//
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/" + pathToMasterBus);
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/" + pathToMusicBus);
        oneShotBus = FMODUnity.RuntimeManager.GetBus("bus:/" + PathToOneShotBus);
        UpdateSettings();
        UI.SetActive(false);
    }

    private void Update()
    {
        UpdateSettings();
        if (Input.GetKeyDown(key))
        {
            activeUI = !activeUI;
            UI.SetActive(activeUI);
        }
    }

    public void SetMasterVolume(float _newVolume)
    {
        masterVolume = _newVolume;
    }
    public void SetMusicVolume(float _newVolume)
    {
        musicVolume = _newVolume;
    }
    public void SetOneShotVolume(float _newVolume)
    {
        oneShotsVolume = _newVolume;
    }

    public void UpdateSettings()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        oneShotBus.setVolume(oneShotsVolume);
    }
}
