using Autohand;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private LineRenderer rightGrabPointer;
    [SerializeField] private LineRenderer leftGrabPointer;

    private AutoHandPlayer autoHandPlayer;
    private UICanvasController canvasController;
    private GameManager gameManager;
    public AutoHandPlayer AutoHandPlayer { get { return autoHandPlayer; } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canvasController = UICanvasController.Instance;
        gameManager = GameManager.instance;

        leftGrabPointer.enabled = false;
        rightGrabPointer.enabled = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !gameManager.GameOver)
        {
            if (Input.GetButtonDown("XRI_Right_PrimaryButton") || Input.GetButtonDown("XRI_Left_PrimaryButton"))
            {
                canvasController.Pause();
            }
        }
    }
}
