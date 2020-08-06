using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public AnimationCurve SlowingCurve;
    public float TotalShakeTime = 0.5f;
    public float ShakeIntensity = 5f;
    public float NoiseScale = 5f;

    private CinemachineVirtualCamera CMcam;
    private CinemachineCameraOffset CMcamOffset;
    private bool _shaking = false;
    private float _counter = 0;

    public static ScreenShakeController Instance;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
            Instance = this;

        CMcam = GetComponent<CinemachineVirtualCamera>();
        CMcamOffset = GetComponent<CinemachineCameraOffset>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (_shaking)
        {
            _counter += Time.deltaTime;
            ScreenShake(_counter, TotalShakeTime);
            print("Shaking");
            if (_counter >= TotalShakeTime)
            {
                _shaking = false;
            }
        }
    }

    private void ScreenShake(float counter, float shakeTime)
    {
        var scaleX = counter * NoiseScale;
        var scaleY = (shakeTime-counter)*NoiseScale;

        var noiseX = Mathf.PerlinNoise(scaleX, scaleY);
        var noiseY = Mathf.PerlinNoise(scaleY, scaleX);

        noiseX -= 0.5f; // Normalize it to -0.5 to 0.5
        noiseY -= 0.5f; // Normalize it to -0.5 to 0.5

        noiseX *= (ShakeIntensity*SlowingCurve.Evaluate(counter/shakeTime)); //Up its intensity
        noiseY *= (ShakeIntensity * SlowingCurve.Evaluate(counter / shakeTime)); //Up its intensity

        CMcamOffset.m_Offset = new Vector3(noiseX, noiseY, 1);
    }

    public void Shake()
    {
        _shaking = true;
    }
}
