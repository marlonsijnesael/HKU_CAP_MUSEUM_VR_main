using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CapturePainting : MonoBehaviour {

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
  

    // Use this for initialization
    void Start () {


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
    void Update () {

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




        if (Input.GetKeyDown(KeyCode.Space))
        {
            //screenshotCount++;
            TakeScreenShot();
            ReadPixelsOut("SS_" + screenshotCount + ".png");
            //FreezeCam();
            
            //StartFreezing();

        }


    }

    private void ReadPixelsOut(string filename)
    {
        if (resultantImage != null)
        {
            resultantImage.GetPixels();
            RenderTexture.active = currentRT;

            byte[] bytes = resultantImage.EncodeToPNG();

            //Sprite s = Sprite.Create(resultantImage, new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), new Vector2(0.0f, 0.0f));

            Texture2D tex = new Texture2D(cam.targetTexture.width, cam.targetTexture.height, TextureFormat.RGB24, false);
            tex.LoadImage(bytes);

            screenLayer2.GetComponent<Renderer>().material.mainTexture = tex;


            // save on disk
            var path = screenshotsDirectory + "/" + filename;
            File.WriteAllBytes(path, bytes);

            Destroy(resultantImage);
        }
    }

    IEnumerator FadePaintingIn()
    {

        if(screenshotCount == 1)
        {
            Color tcolor1;
            Color tcolor2;

            for (float f = 1.0f; f >= 0; f -= 0.005f)
            {
                //tcolor1 = layer1.GetComponent<Renderer>().material.color;
                //tcolor2 = layer2.GetComponent<Renderer>().material.color;
                //tcolor1.a = f;
                //tcolor2.a = 1 - f;
                ////imageLayer1.color = tcolor1;
                //layer2.GetComponent<Renderer>().material.color = tcolor2;

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

        }


        yield return new WaitForSeconds(0.005f);
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
