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
    [SerializeField] private TextMeshProUGUI toolTipText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI coconutsText;
    [SerializeField] private TextMeshProUGUI planksText;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject continueButton;

    [SerializeField] private float toolTipTime;

    [SerializeField] public GameObject buildRaft;
    [SerializeField] FadeScreen fadeScreen;
    private MeshRenderer meshRenderer;

    public UnityEvent OnGameover;
    public UnityEvent OnDead;
    public UnityEvent OnStartScene;
    private void Awake()
    {
        Instance = this;
        meshRenderer = fadeScreen.GetComponent<MeshRenderer>();
    }

    public TextMeshProUGUI ToolTipText
    { get { return toolTipText; } }

    private void Start()
    {
        OnStartScene?.Invoke();
        if (livesText != null)
        {
            LivesUpdate();
        }
    }
    public void ChangeText(string textString)
    {
        objectiveText.SetText(textString);
    }

    public void ToolTips(string textString)
    {
        toolTipText.SetText(textString);
    }

    public void LivesUpdate()
    {
        livesText.SetText("Lives: " + GameManager.instance.playerLives.ToString());  
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        Player.Instance.AutoHandPlayer.maxMoveSpeed = 0f;

        OnGameover?.Invoke();

    }
    public void Restart()
    {
        Time.timeScale = 1f;
        gameOver.SetActive(false);

        meshRenderer.enabled = true;
        GameManager.instance.playerLives = 3;
        GameManager.instance.playerSpawnPoint = GameManager.instance.playerStartPoint;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(GoToSceneAsyncRoutine(currentScene));
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
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
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
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
;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    public void PlayerInstantDeath()
    {
        if (GameManager.instance.playerLives - 1 > 0)
        {
            GameManager.instance.playerLives -= 1;
            Dead();
        }
        else
        {
            GameOver();
        }
    }

    public void SetPlayerTransform()
    {
        GameManager.instance.autoHandPlayer.transform.position = GameManager.instance.playerSpawnPoint.position;
        GameManager.instance.autoHandPlayer.transform.rotation = GameManager.instance.playerSpawnPoint.rotation;
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
}
