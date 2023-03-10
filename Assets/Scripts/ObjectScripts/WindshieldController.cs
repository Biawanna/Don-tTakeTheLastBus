using UnityEngine;
public class WindshieldController : MonoBehaviour
{
    public WindshieldWiper leftWiper;
    public WindshieldWiper rightWiper;

    [SerializeField] private float leftWiperDirection;
    [SerializeField] private float rightWiperDirection;

    void Start()
    {
        // Set the direction of the windshield wipers
        leftWiper.SetDirection(leftWiperDirection);
        rightWiper.SetDirection(rightWiperDirection);
    }
}
