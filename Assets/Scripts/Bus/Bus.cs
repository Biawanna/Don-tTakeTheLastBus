using UnityEngine;

public class Bus : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for the bus movement.
    /// </summary>
    
    [SerializeField] private GameObject player;

    [Header("Bus Movement Settings")]
    [SerializeField] private float busSpeed = 10.0f;
    [SerializeField] private float horizontalFrequency = 1.0f; 
    [SerializeField] private float horizontalMax;
    [SerializeField] private float horizontalMin;

    private float time = 0f; // Variable to keep track of time

    void Start()
    {
        player.transform.parent = transform;
    }

    void FixedUpdate()
    {
        // Update time
        time += Time.deltaTime;

        // Calculate the new position of the bus based on busSpeed and horizontalFrequency
        Vector3 newPosition = transform.position + new Vector3(Mathf.Sin(time * horizontalFrequency) * Time.deltaTime * busSpeed, 0f, 1f) * Time.deltaTime * busSpeed;

        // Clamp the new position to stay within a certain range (e.g., between -5 and 5 on X-axis)
        newPosition.x = Mathf.Clamp(newPosition.x, horizontalMin, horizontalMax);

        // Move the bus to the new position
        transform.position = newPosition;
    }
}


