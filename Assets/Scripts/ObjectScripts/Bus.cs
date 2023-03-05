using UnityEngine;

public class Bus : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called at a fixed interval (default 50 times per second) for physics calculations
    void FixedUpdate()
    {
        // Move the bus forward by the speed amount
        Vector3 velocity = transform.forward * speed;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
