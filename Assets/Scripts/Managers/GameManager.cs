using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [SerializeField] private GameObject autoHandPlayer;
    private UICanvasController uICanvasController;

    public GameObject Player
    {
        get { return autoHandPlayer; }
        set { autoHandPlayer = value; }
    }

    private void Awake()
    {
        uICanvasController = UICanvasController.Instance;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
