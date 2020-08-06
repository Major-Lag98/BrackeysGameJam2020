using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class LevelTransitionEffect : MonoBehaviour
{

    public Material Material;
    public Image TransitionImage;
    public LevelManager LevelManager;

    private RenderTexture _savedTexture;
    private bool _saveNextFrame;
    private Camera _camera;
    private Material transitionImageMaterial;

    private float counter = 0;
    private bool transitionining = false;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        transitionImageMaterial = TransitionImage.material;

        LevelManager.PreSceneSwitched += () =>
        {
            CaptureFrame();
            transitionining = true;
        };

        //RenderPipelineManager.endFrameRendering += (ctx, cameras) => 
        //{ 
        //    print("test"); 
        //};

    }

    private void CaptureFrame()
    {
        var width = Screen.width;
        var height = Screen.height;

        var rt = RenderTexture.GetTemporary(width, height); // Get graphics widht/height here.
        Texture2D actual = null;
        try
        {
            _camera.targetTexture = rt; // Set the target to rt
            _camera.Render(); // Render the camera
            _camera.targetTexture = null; // Clear the target

            actual = new Texture2D(width, height, TextureFormat.RGBA32, 1, true); 
            Graphics.CopyTexture(rt, actual); //Copy it to our actual

            TransitionImage.enabled = true; // Set our transition to true
            TransitionImage.material.mainTexture = actual; // Set the main texture to our actual
        }
        finally
        {
            RenderTexture.ReleaseTemporary(rt); // Release the temp
        }
        _saveNextFrame = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (transitionining)
        {
            counter += Time.deltaTime;

            transitionImageMaterial.SetFloat("Time", counter);

            if (counter >= 0.4)
            {
                counter = 0;
                transitionining = false;
                TransitionImage.enabled = false;
            }
        }
    }
}
