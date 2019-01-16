using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvas : MonoBehaviour {

    public Material matEng, matDutch;
    public MeshRenderer mr;
    private void Awake()
    {
         if (PlayerPrefs.GetInt("LanguageIndex") == 0)
        {
            mr.material = matEng;
        }
        else
        {
            mr.material = matDutch;
        }
    }
}
