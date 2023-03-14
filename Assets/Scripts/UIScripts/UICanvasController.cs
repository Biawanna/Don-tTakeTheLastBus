using System.Collections;
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

    [Header("Menu References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject continueButton;

    private GameManager gameManager;
    private MeshRenderer meshRenderer;
    public UnityEvent OnGameover;
    public UnityEvent OnDead;
    public UnityEvent OnStartScene;
    private void Awake()
    {
        Instance = this;
        gameManager = GameManager.instance;
        meshRenderer = fadeScreen.GetComponent<MeshRenderer>();
    }

    public TextMeshProUGUI ToolTipText
    { get { return dialogueText; } }


    private void Start()
    {
        OnStartScene?.Invoke();

        objectiveText.text = objectiveString;

        StartCoroutine(ObjectiveTextRoutine(objectiveTextDestroyTime));
    }

    public void ChangeText(string textString)
    {
        objectiveText.SetText(textString);
    }

    public void ToolTips(string textString)
    {
        dialogueText.SetText(textString);
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        gameManager.PauseGame();
    }
    public void Restart()
    {
        gameManager.ResumeGame();
        gameManager.ResetGame();
        gameOver.SetActive(false);

        meshRenderer.enabled = true;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneAsyncRoutine(currentScene));
    }

    public void MainMenu()
    {
        gameManager.ResumeGame();
        meshRenderer.enabled = true;
        StartCoroutine(GoToSceneAsyncRoutine(0));
    }

    public void Play()
    {
        meshRenderer.enabled = true;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(GoToSceneAsyncRoutine(nextScene));
    }

    public void Pause()
    {
        gameManager.PauseGame();
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        gameManager.ResumeGame();
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Dead()
    {
        ToolTips("You died!");
        continueButton.SetActive(true);
        Player.Instance.AutoHandPlayer.maxMoveSpeed = 0f;

        OnDead?.Invoke();
    }

    public void Continue()
    {
        gameManager.ResumeGame();
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void ChangeSkyBox(Material material)
    {
        RenderSettings.skybox = material;
    }
    public IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        operation.allowSceneActivation = true;
    }

    private IEnumerator ObjectiveTextRoutine(float destroyTime)
    {
        objectiveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(destroyTime);
        objectiveText.gameObject.SetActive(false);

        yield break;
    }
}
