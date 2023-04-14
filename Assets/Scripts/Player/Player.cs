using Autohand;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private LineRenderer rightGrabPointer;
    [SerializeField] private LineRenderer leftGrabPointer;

    private AutoHandPlayer autoHandPlayer;
    public AutoHandPlayer AutoHandPlayer { get { return autoHandPlayer; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

        leftGrabPointer.enabled = false;
        rightGrabPointer.enabled = false;
    }
}
