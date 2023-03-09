using UnityEngine;
using System.Collections;

// Makes objects float up & down while spinning.
public class FloatObject : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    private float amplitude = 0.5f;
    private float frequency = 1f;

    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    [SerializeField] private float minAmplitude;
    [SerializeField] private float maxAmplitude;

    [SerializeField] private float minFrequency;
    [SerializeField] private float maximumFrequency;

    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;

        amplitude = Random.Range(minAmplitude, maxAmplitude);
        frequency = Random.Range(minFrequency, maximumFrequency);
    }

    void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}