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

    //Render texture that will be on the painting layers
    public RenderTexture paintingScreen;
    //Painting layer 1
    public GameObject screenLayer1;
    //Painting layer 2
    public GameObject screenLayer2;
    MeshRenderer bigScreen2MeshRenderer;
    //Ortogonal camera
    Camera cam;
    //Name of the folder that contains the captures
    private string screenshotsDirectory = "UnityHeadlessRenderingScreenshots";
    private int screenshotCount = 0;
    private int frameCount = 0;
    //Texture for the captures
    private Texture2D resultantImage;
    //Render texture for the captures
    public RenderTexture currentRT;
    //AudioSource for the brushing sound
    public AudioSource brushingSource;

    public GameObject itemSlot;
    public GameObject item;
    public bool holdingItem = false;
    //Flag that indicates if the process of fading cam start
    private bool canFade;

    


    // Use this for initialization
    // Creates a directory for the captured images if it doesn't exist. If it exists, deletes it and creates it again.
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
            cam.targetTexture.width = 1024;
            cam.targetTexture.height = 1024;
        }

        canFade = true;
    }


    // This function isn't used and can be eliminated. 
    IEnumerator FreezeCam()
    {
        //yield return null;
        cam.clearFlags = CameraClearFlags.Nothing;
        yield return null;
        cam.cullingMask = 0;
    }

    // This function isn't used and can be eliminated. 
    void StartFreezing()
    {
        StartCoroutine("FreezeCam");

    }

    private void OnPostRender()
    {

    }

    // Update is called once per frame
    // Used to detect the trigger from the controller
    void LateUpdate()
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

        if(Input.GetKeyUp(KeyCode.Space))
        {
            TakeScreenShot();
            ReadPixelsOut("SS_" + screenshotCount + ".png");
        }
        
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

    // Used to define if the trigger was pulled or not. Used in the LateUpdate function
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

    // Turns image from camera to png
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

    // Does the fading. Uses two layers to make the fading effect. Since its a procedure uses a flag called canFade that is True 
    // if a new fading process can start and False when it cannot. It also triggers the sound for the brushing in the canvas.
    IEnumerator FadePaintingIn()
    {

        //if(screenshotCount == 1)
        //{
        Color tcolor1;
        Color tcolor2;

        canFade = false;

        tcolor1 = screenLayer1.GetComponent<Renderer>().material.color;
        tcolor2 = screenLayer2.GetComponent<Renderer>().material.color;

        brushingSource.Play();

        //Goes from 1 to 0 changing the alpha color of the layers of the painting
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


            //this timer makes the effect of fading. It waits time for the alpha color to change from 0 to 1
            yield return new WaitForSeconds(0.0025f);


        }

        screenLayer2.GetComponent<Renderer>().material.color = tcolor1;
        screenLayer2.GetComponent<Renderer>().material.mainTexture = screenLayer1.GetComponent<Renderer>().material.mainTexture;

        //}


        yield return new WaitForSeconds(0.005f);

        canFade = true;
    }

    // Captures the current view of the camera
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
