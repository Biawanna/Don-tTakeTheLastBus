using UnityEngine;
using Autohand;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This script is responsible for gameplay methods and keeps a reference to the player.
    /// </summary>
    public static GameManager instance;

    [Header("Player Speed Settings")]
    [SerializeField] private float playerStop = 0;

    [Header("Pointer References")]
    [SerializeField] private GameObject grabPointerL;
    [SerializeField] private GameObject grabPointerR;
    [SerializeField] private GameObject teleportPointer;

    [Header("Script References")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private UICanvasController uICanvasController;
    [SerializeField] private PlayerInventory inventory;

    private bool gameOver = false;
    private float playerMaxSpeed;
    private GameObject autoHandPlayer;
    private AutoHandPlayer autoHandScript;
    private PlayerInventory playerInventory;
   
    public GameObject Player
    {
        get { return autoHandPlayer; }
        set { autoHandPlayer = value; }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        autoHandPlayer = GameObject.FindGameObjectWithTag("Player");

        autoHandScript = autoHandPlayer.GetComponentInChildren<AutoHandPlayer>();

        playerMaxSpeed = autoHandScript.maxMoveSpeed;
    }

    /// <summary>
    /// Stops player movement. Disables player pointers.
    /// </summary>
    public void PauseGame()
    {
        autoHandScript.maxMoveSpeed = playerStop;

        ToolBox.EnableGameObject(teleportPointer, false);
        ToolBox.EnableGameObject(grabPointerL, false);
        ToolBox.EnableGameObject(grabPointerR, false);
    }

    /// <summary>
    /// Resumes player movement. Enables player pointers.
    /// </summary>
    public void ResumeGame()
    {
        autoHandScript.maxMoveSpeed = playerMaxSpeed;

        ToolBox.EnableGameObject(teleportPointer, true);
        ToolBox.EnableGameObject(grabPointerL, true);
        ToolBox.EnableGameObject(grabPointerR, true);
    }

    /// <summary>
    /// Resets the players inventory.
    /// </summary>
    public void ResetGame()
    {
        inventory.ResetInventory();
    }
}
