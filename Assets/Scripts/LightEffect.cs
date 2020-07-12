using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightEffect : MonoBehaviour {

    [SerializeField]List<Light2D> LightGroup1;
    [SerializeField]List<Light2D> LightGroup2;

    [SerializeField]public float lightSwitchingDelay = 1;

    List<Color> EcolorList = new List<Color>() {
        Color.red,
        Color.green,
        Color.yellow,
        Color.cyan,
        Color.blue,
        Color.magenta,
    };

    void Start() {
        StartCoroutine(LightSwitching());
    }

    IEnumerator LightSwitching() {
        while (true) {
            foreach (Light2D light in LightGroup1) {
                light.intensity = Random.Range(1.2f, 1.6f);
                light.color = EcolorList[Random.Range(0, EcolorList.Count)];;
            }
            foreach (Light2D light in LightGroup2) {
                light.intensity = Random.Range(0.6f, 1f);
            }

            yield return new WaitForSeconds(lightSwitchingDelay);

            foreach (Light2D light in LightGroup1) {
                light.intensity = Random.Range(0.6f, 1f);
            }
            foreach (Light2D light in LightGroup2) {
                light.intensity = Random.Range(1.2f, 1.6f);
                light.color = EcolorList[Random.Range(0, EcolorList.Count)];;
            }

            yield return new WaitForSeconds(lightSwitchingDelay);
        }
    }

    public IEnumerator ShutOffLights() {
        foreach (Light2D light in LightGroup1) {
            light.intensity = 0;
        }
        foreach (Light2D light in LightGroup2) {
            light.intensity = 0;
        }
        yield return null;
    }

}
