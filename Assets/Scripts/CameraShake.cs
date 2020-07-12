using UnityEngine;
using System.Collections;
using Cinemachine;

public class CameraShake : MonoBehaviour
{

	#region Sigleton
    private static CameraShake instance;
    public static CameraShake Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CameraShake>();
            return instance;
        }
    }
    #endregion

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private CinemachineVirtualCamera vCam;
	private CinemachineBasicMultiChannelPerlin vCamNoise;
	private float vCamNoiseAmplitude;
	private float vCamNoiseFrequency;

	// How long the object should shake for.
	public float shakeDuration = 0f;

	// Amplitude of the shake. A larger value shakes the camera harder.

	public float normalAmplitude = 0.04f;
	public float shakingAmplitude = 0.2f;
	public float normalFrequency = 0.005f;
	public float shakingFrequency = 0.25f;
	public float decreaseFactor = 1.0f;

	void Start(){
		vCam = GetComponent<CinemachineVirtualCamera>();
		vCamNoise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		vCamNoiseAmplitude = normalAmplitude;
		vCamNoiseFrequency = normalFrequency;

		vCamNoise.m_AmplitudeGain = vCamNoiseAmplitude;
		vCamNoise.m_FrequencyGain = vCamNoiseFrequency;
	}

	void Update() {
		vCamNoise.m_AmplitudeGain = vCamNoiseAmplitude;
		vCamNoise.m_FrequencyGain = vCamNoiseFrequency;

		if (shakeDuration > 0) {
			vCamNoiseAmplitude = shakingAmplitude;
			vCamNoiseFrequency = shakingFrequency;
			shakeDuration -= Time.deltaTime * decreaseFactor;
		}
		else {
			shakeDuration = 0f;
			vCamNoiseAmplitude = normalAmplitude;
			vCamNoiseFrequency = normalFrequency;
		}
	}
}
