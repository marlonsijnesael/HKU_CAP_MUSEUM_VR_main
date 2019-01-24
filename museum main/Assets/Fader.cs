using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{


    public GameObject imgObject;
    private Image img;
    bool active;
    public static Fader _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        img = imgObject.GetComponent<Image>();
        DoStuff(true, false);
    }


    public void DoStuff(bool _fadeAway ,bool _nextScene)
    {
        StartCoroutine(FadeImage(_fadeAway, _nextScene));
    }
    //public void OnButtonClick()
    //{
    //    active = imgObject.activeInHierarchy;
    //    imgObject.SetActive(!active);
    //    // fades the image out when you click
    //    StartCoroutine(FadeImage(!active, false));

    //}

    public IEnumerator FadeImage(bool fadeAway, bool nextScene)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime / 5)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
            imgObject.SetActive(false);
            active = false;
        }
        // fade from transparent to opaque
        else if (!fadeAway)
        {
            imgObject.SetActive(true);
            active = true;
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime / 5)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, 1);
                yield return null;
            }
            
            if (nextScene)
            {
                SceneManager.LoadScene(1);
            }
        }


    }
}
