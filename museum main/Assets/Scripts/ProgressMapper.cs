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
            Color targetColor;

            targetColor = targetObject.GetComponent<Renderer>().material.color;
            targetColor.a = 1.0f;
            targetObject.GetComponent<Renderer>().material.color = targetColor;


        }
    }

    public void GoToMuseum()
    {
        //fadeout required
        //SceneManager.LoadScene(museumScene.buildIndex);
        SceneManager.LoadScene(2);
    }
}
