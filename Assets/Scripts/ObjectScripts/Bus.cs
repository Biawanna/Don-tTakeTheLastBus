using UnityEngine;

public class Bus : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float busSpeed = 10.0f;

    void Start()
    {
        player.transform.parent = transform;
    }

    void FixedUpdate()
    {
        // Move the bus forward by the busSpeed amount
        transform.Translate(Vector3.forward * Time.deltaTime * busSpeed);
    }

}


