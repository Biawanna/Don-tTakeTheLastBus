using UnityEngine;

public class WindshieldWiper : MonoBehaviour
{
    public float speed = 5f; // The speed of the windshield wipers
    public float angle = 45f; // The maximum angle of rotation of the windshield wipers
    public float distance = 2f; // The distance the windshield wipers move

    private float startPosX; // The starting position of the windshield wipers
    private float direction = 1f; // The direction of the windshield wipers

    void Start()
    {
        startPosX = transform.position.x; // Set the starting position of the windshield wipers
    }

    void Update()
    {
        float newPositionX = startPosX + Mathf.PingPong(Time.time * speed, distance * 2) - distance; // Calculate the new position of the windshield wipers
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z); // Move the windshield wipers to the new position

        float angleSin = Mathf.Sin(Time.time * speed) * angle * direction; // Calculate the angle of rotation based on the current time, speed, and direction
        transform.localRotation = Quaternion.Euler(0f, 0f, angleSin); // Rotate the windshield wipers based on the calculated angle
    }

    public void SetDirection(float newDirection)
    {
        direction = newDirection; // Set the direction of the windshield wipers to a new direction value
    }
}
