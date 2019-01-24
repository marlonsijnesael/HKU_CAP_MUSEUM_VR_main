using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{


    public GameObject imgObject;
    private Image img;
    bool active;
    private void Start()
    {
        img = imgObject.GetComponent<Image>();
        StartCoroutine(FadeImage(!active));
    }

    public void OnButtonClick()
    {
        active = imgObject.activeInHierarchy;
        imgObject.SetActive(!active);
        // fades the image out when you click
        StartCoroutine(FadeImage(!active));

    }

    IEnumerator FadeImage(bool fadeAway)
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
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime / 5)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
            imgObject.SetActive(true);
        }
    }
}
