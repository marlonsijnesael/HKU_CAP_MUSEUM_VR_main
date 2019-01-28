using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressMapper : MonoBehaviour {

    public Scene menuScene, paintingScene, museumScene;
    [HideInInspector]
    public GameObject[] targets = new GameObject[3];
    int targetCount = 0;
    int numberTriggerPresses = 0;
    public int amountOfTargets = 0;
    public static ProgressMapper _instance;
    public GameObject targetObject;
    public GameObject sphereCover;
    private float _fadeDuration = 10f;


    //public GameObject endingCollider;

    //singleton for easy acces
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

    public void Start()
    {
        targets = new GameObject[amountOfTargets];
    }

    public void AddTriggerPress()
    {
        numberTriggerPresses++;

    }



    //checks if target is already crossed off, if not it will be added to the array. If all the targets are completed, the museum scene will load
    public void AddTarget(GameObject _target)
    {
  
        foreach (GameObject _go in targets)
        {
            int i = 0;
            if (_go == _target)
            {
                return;
            }
            if (_go == null && _go != targets[0]) {
                return;
                    }

            if (targetCount < amountOfTargets)
            {
                targets[targetCount] = _target;
                targetCount++;
            }

            if (targetCount >= amountOfTargets)
            {
                GoToMuseum();
            }


            i++;

            //if (_go != null)
            //{
            //    if (targetCount < amountOfTargets)
            //    {
            //        targets[targetCount] = _target;
            //        targetCount++;
            //    }
            //    else
            //    {
            //        GoToMuseum();
            //    }
            //}
       
      
        }
    }

    public void Update()
    {
        if(numberTriggerPresses == 1)
        {
            targetObject.SetActive(true);
            
            //FadeToWhite();
            numberTriggerPresses++;
            //Color targetColor;

            //targetColor = targetObject.GetComponent<Renderer>().material.color;
            //targetColor.a = 1.0f;
            //targetObject.GetComponent<Renderer>().material.color = targetColor;


        }
    }

    IEnumerator FadeToBlack()
    {

        Color tcolor1;
 

        tcolor1 = sphereCover.GetComponent<Renderer>().material.color;

        for (float f = 1.0f; f >= 0; f -= 0.0070f)
        {
            tcolor1 = sphereCover.GetComponent<Renderer>().material.color;

            tcolor1.a = 1 - f;

            sphereCover.GetComponent<Renderer>().material.color = tcolor1;
            yield return new WaitForSeconds(0.0025f);

        }

        SceneManager.LoadScene(2);



    }

    public void GoToMuseum()
    {
        //fadeout required
        //FadeToWhite();
        StartCoroutine(FadeToBlack());
        //SceneManager.LoadScene(1);
        //Fader._instance.DoStuff(false,true);
        //Debug.Log("fader is working");
       
    }

    private void FadeToWhite()
    {
        //set start color
        SteamVR_Fade.Start(Color.clear, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.white, _fadeDuration);
    }
    private void FadeFromWhite()
    {
        //set start color
        SteamVR_Fade.Start(Color.white, 0f);
        //set and start fade to
        SteamVR_Fade.Start(Color.clear, _fadeDuration);
    }
}
