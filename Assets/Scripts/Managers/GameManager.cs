using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [SerializeField] public int playerLives = 3;
    [SerializeField] public GameObject autoHandPlayer;

    [Header("CheckPoints")]
    [SerializeField] public Transform playerSpawnPoint;
    [SerializeField] public Transform playerStartPoint;

    private UICanvasController uICanvasController;

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

    private void Start()
    {
        autoHandPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    public void DisableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    public void AttackPlayer(float minAttackDamage, float maxAttackDamage)
    {
        var randomNumber = Random.Range(minAttackDamage, maxAttackDamage);

        if (Player.Instance.playerHealth - randomNumber > 0)
        {
            Player.Instance.playerHealth -= randomNumber;
        }
        else
        {
            if (playerLives - 1 > 0)
            {
                playerLives -= 1;
                uICanvasController.Dead();
            }
            else
            {
                uICanvasController.GameOver();
            }
        }
    }
}
