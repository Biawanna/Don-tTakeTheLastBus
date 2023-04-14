using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UICanvasController : MonoBehaviour
{
    public static UICanvasController Instance;
   
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Objective Text Settings")]
    [SerializeField] private string objectiveString;
    [SerializeField] private float objectiveTextDestroyTime;

    [Header("Fade References")]
    [SerializeField] FadeScreen fadeScreen;
    [SerializeField] FadeCanvas jumpScare;

    [Header("Menu References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject continueButton;

    private GameManager gameManager;
    private SoundManager soundManager;
    private MeshRenderer meshRenderer;
    public UnityEvent OnGameover;
    public UnityEvent OnDead;
    public UnityEvent OnStartScene;
    private void Awake()
    {
        Instance = this;
        gameManager = GameManager.instance;
        soundManager = SoundManager.Instance;
        meshRenderer = fadeScreen.GetComponent<MeshRenderer>();
    }

    public TextMeshProUGUI ToolTipText
    { get { return dialogueText; } }


    private void Start()
    {
        OnStartScene?.Invoke();

        // Disables objective text.
        StartCoroutine(ToolBox.TextRoutine(objectiveTextDestroyTime, objectiveText));

        jumpScare.FadeOut(0);

        objectiveText.text = objectiveString;
    }

    /// <summary>
    /// Set objective text.
    /// </summary>
    public void ChangeText(string textString)
    {
        objectiveText.SetText(textString);
    }

    /// <summary>
    /// Set tool tip text.
    /// </summary>
    public void ToolTips(string textString)
    {
        dialogueText.SetText(textString);
    }

    /// <summary>
    /// Triggers game over elements.
    /// </summary>
    public void GameOver()
    {
        soundManager.PlayRandomScreamSound();
        gameOver.SetActive(true);
        gameManager.PauseGame();
        jumpScare.StartFadeInFadeOutRoutine();
        soundManager.PlayRandomScreamSound();

        gameManager.GameOver = true;
    }

    /// <summary>
    /// Triggers restart button methods.
    /// </summary>
    public void Restart()
    {
        gameManager.ResumeGame();
        gameManager.ResetGame();
        gameOver.SetActive(false);

        meshRenderer.enabled = true;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ToolBox.GoToSceneAsyncRoutine(currentScene, fadeScreen));

        gameManager.GameOver = false;
    }

    /// <summary>
    /// Triggers Main Menu button methods.
    /// </summary>
    public void MainMenu()
    {
        gameManager.ResumeGame();
        meshRenderer.enabled = true;
        StartCoroutine(ToolBox.GoToSceneAsyncRoutine(0, fadeScreen));

        gameManager.GameOver = false;
    }

    /// <summary>
    /// Triggers play button elements.
    /// </summary>
    public void Play()
    {
        meshRenderer.enabled = true;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(ToolBox.GoToSceneAsyncRoutine(nextScene, fadeScreen));
    }

    /// <summary>
    /// Triggers pause button elements.
    /// </summary>
    public void Pause()
    {
        gameManager.PauseGame();
        pauseMenu.SetActive(true);
    }

    /// <summary>
    /// Triggers resume button elements.
    /// </summary>
    public void Resume()
    {
        gameManager.ResumeGame();
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    /// <summary>
    /// Application quit.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Triggers player dead elements.
    /// </summary>
    public void Dead()
    {
        ToolTips("You died!");
        continueButton.SetActive(true);
        Player.Instance.AutoHandPlayer.maxMoveSpeed = 0f;

        OnDead?.Invoke();
    }

    /// <summary>
    /// Triggers continue button elements.
    /// </summary>
    public void Continue()
    {
        gameManager.ResumeGame();
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
