using UnityEngine;
using Autohand;
using System.Collections;

public class GameManager : MonoBehaviour
{
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

    private float playerMaxSpeed;
    private GameObject autoHandPlayer;
    private AutoHandPlayer autoHandScript;
   

    public GameObject Player
    {
        get { return autoHandPlayer; }
        set { autoHandPlayer = value; }
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

        StartCoroutine(CheckIfGameComplete());
    }
   
    public void PauseGame()
    {
        autoHandScript.maxMoveSpeed = playerStop;

        EnableGameObject(teleportPointer, false);
        EnableGameObject(grabPointerL, false);
        EnableGameObject(grabPointerR, false);
    }

    public void ResumeGame()
    {
        autoHandScript.maxMoveSpeed = playerMaxSpeed;

        EnableGameObject(teleportPointer, true);
        EnableGameObject(grabPointerL, true);
        EnableGameObject(grabPointerR, true);
    }

    public void EnableGameObject(GameObject gameObject, bool enable)
    {
        gameObject.SetActive(enable);
    }
    public void SetBool(bool boolToSet, bool set)
    {
        boolToSet = set;
    }

    public void ResetGame()
    {
        dialogueManager.ResetInventory();
    }

    private IEnumerator CheckIfGameComplete()
    {
        while (true)
        {
            if (dialogueManager.CheckIfPlayerWins())
            {
                uICanvasController.GameOver();
                PauseGame();

                yield break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
}
