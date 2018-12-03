using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using HTC.UnityPlugin.Vive;


public class CapturePainting : MonoBehaviour
{

    public GameObject spawnPoint;
    public HandRole hand = HandRole.RightHand;
    public bool rightTrigger;

    public RenderTexture paintingScreen;
    public GameObject screenLayer1;
    public GameObject screenLayer2;
    MeshRenderer bigScreen2MeshRenderer;
    Camera cam;
    private string screenshotsDirectory = "UnityHeadlessRenderingScreenshots";
    private int screenshotCount = 0;
    private int frameCount = 0;
    private Texture2D resultantImage;
    public RenderTexture currentRT;

    public GameObject itemSlot;
    public GameObject item;
    public bool holdingItem = false;
    private bool canFade;

    


    // Use this for initialization
    void Start()
    {


        bigScreen2MeshRenderer = screenLayer2.GetComponent<MeshRenderer>();
        cam = GetComponent<Camera>();

        cam.forceIntoRenderTexture = true;
        if (Directory.Exists(screenshotsDirectory))
        {
            Directory.Delete(screenshotsDirectory, true);
        }
        //if (!Application.isEditor)
        if (!Directory.Exists(screenshotsDirectory))
        {
            Directory.CreateDirectory(screenshotsDirectory);
            cam.targetTexture = currentRT;
        }

        canFade = true;
    }

    IEnumerator FreezeCam()
    {
        //yield return null;
        cam.clearFlags = CameraClearFlags.Nothing;
        yield return null;
        cam.cullingMask = 0;
    }

    void StartFreezing()
    {
        StartCoroutine("FreezeCam");

    }

    private void OnPostRender()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Taking Screenshots
        //frameCount += 1;
        //if (frameCount == 1)
        //{
        //    TakeScreenShot();
        //}
        //else if (frameCount == 3)
        //{
        //    ReadPixelsOut("SS_" + screenshotCount + ".png");
        //}

        //if (frameCount >= 3)
        //{
        //    frameCount = 0;
        //}

     
        rightTrigger = HandleInput(hand, ControllerButton.Trigger, rightTrigger);

        
        if (rightTrigger && canFade)
        {
            TakeScreenShot();
            ReadPixelsOut("SS_" + screenshotCount + ".png");
            //do stuff
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //screenshotCount++;
        //    TakeScreenShot();
        //    ReadPixelsOut("SS_" + screenshotCount + ".png");
        //    //FreezeCam();

        //    //StartFreezing();

        //}


    }

    public bool HandleInput(HandRole _hand, ControllerButton _controllerButton, bool _inputBool)
    {
        if (ViveInput.GetPress(_hand, _controllerButton))
        {
            return _inputBool = true;
        }

        if (ViveInput.GetPressUp(_hand, _controllerButton))
        {
            return _inputBool = false;
        }

        
       

        return false;

    }

    private void ReadPixelsOut(string filename)
    {
        if (resultantImage != null)
        {
            resultantImage.GetPixels();
            //RenderTexture.active = currentRT;
            RenderTexture.active = cam.targetTexture;

            byte[] bytes = resultantImage.EncodeToPNG();

            //Sprite s = Sprite.Create(resultantImage, new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), new Vector2(0.0f, 0.0f));

            Texture2D tex = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);
            tex.LoadImage(bytes);

            screenLayer1.GetComponent<Renderer>().material.mainTexture = tex;
            //screenLayer2.GetComponent<Renderer>().material.mainTexture = tex;

            StartCoroutine("FadePaintingIn");

            //screenLayer2.GetComponent<Renderer>().material.mainTexture = tex;


            // save on disk
            var path = screenshotsDirectory + "/" + filename;
            File.WriteAllBytes(path, bytes);

            Destroy(resultantImage);
        }
    }

    IEnumerator FadePaintingIn()
    {

        //if(screenshotCount == 1)
        //{
        Color tcolor1;
        Color tcolor2;

        canFade = false;

        tcolor1 = screenLayer1.GetComponent<Renderer>().material.color;
        tcolor2 = screenLayer2.GetComponent<Renderer>().material.color;

        for (float f = 1.0f; f >= 0; f -= 0.005f)
        {
            tcolor1 = screenLayer1.GetComponent<Renderer>().material.color;
            tcolor2 = screenLayer2.GetComponent<Renderer>().material.color;
            tcolor1.a = 1 - f;
            tcolor2.a = f;
            ////imageLayer1.color = tcolor1;
            screenLayer1.GetComponent<Renderer>().material.color = tcolor1;

            //matEnvironment1 = stages[layer].GetComponent<SkinnedMeshRenderer>().materials;
            //for (int i = 0; i < matEnvironment1.Length; i++)
            //{
            //    tColorEnvironment = matEnvironment1[i].color;
            //    tColorEnvironment.a = f;

            //    matEnvironment1[i].color = tColorEnvironment;
            //}

            //matEnvironment2 = stages[layer + 1].GetComponent<SkinnedMeshRenderer>().materials;
            //for (int i = 0; i < matEnvironment2.Length; i++)
            //{
            //    tColorEnvironment = matEnvironment2[i].color;
            //    tColorEnvironment.a = 1.0f - f;

            //    matEnvironment2[i].color = tColorEnvironment;
            //}



            yield return new WaitForSeconds(0.005f);


        }

        screenLayer2.GetComponent<Renderer>().material.color = tcolor1;
        screenLayer2.GetComponent<Renderer>().material.mainTexture = screenLayer1.GetComponent<Renderer>().material.mainTexture;

        //}


        yield return new WaitForSeconds(0.005f);

        canFade = true;
    }


    public void TakeScreenShot()
    {
        screenshotCount += 1;
        RenderTexture.active = cam.targetTexture;
        cam.Render();
        resultantImage = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);

        //Sprite s = Sprite.Create(resultantImage, new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), new Vector2(0.0f, 0.0f));

        //bigScreen2.GetComponent<MeshRenderer>().material.mainTexture = resultantImage;



        resultantImage.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
        resultantImage.Apply();
    }
}
