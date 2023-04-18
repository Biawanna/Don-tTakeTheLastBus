using UnityEngine;
using System.Collections;
public class BusLights : MonoBehaviour
{
    [SerializeField] private Light[] lights;

    [Header("Light Settings")]
    [SerializeField] private float flickerSpeed = 5f;
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1f;

    private float turnOffThreshold;
    private float lightOffIntensity = 0f;

    private void Start()
    {
        StartCoroutine(Flicker());

        turnOffThreshold = minIntensity + 1;
    }
    IEnumerator Flicker()
    {
        while (true)
        {
            // Set the intensity of each light to a random value between min and max
            foreach (Light light in lights)
            {
                float intensity = Random.Range(minIntensity, maxIntensity);
                if (intensity < turnOffThreshold) // Change this threshold to adjust the horizontalFrequency of flickering
                {
                    light.intensity = lightOffIntensity; // Turn off the light
                }
                else
                {
                    light.intensity = intensity;
                }
            }

            // Wait for a random amount of time between 0 and flickerSpeed
            yield return new WaitForSeconds(Random.Range(0, flickerSpeed));
        }
    }
}
