using UnityEngine;
using System.Collections;
public class BusLights : MonoBehaviour
{
    public Light[] lights;
    public float flickerSpeed = 5f;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1f;

    private void Start()
    {
        StartCoroutine(Flicker());
    }
    IEnumerator Flicker()
    {
        while (true)
        {
            // Set the intensity of each light to a random value between min and max
            foreach (Light light in lights)
            {
                light.intensity = Random.Range(minIntensity, maxIntensity);
            }
            // Wait for a random amount of time between 0 and flickerSpeed
            yield return new WaitForSeconds(Random.Range(0, flickerSpeed));
        }
    }
}
