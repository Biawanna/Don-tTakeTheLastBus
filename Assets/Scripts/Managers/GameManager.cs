using StarterAssets;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [SerializeField] public int playerLives = 3;
    [SerializeField] public GameObject autoHandPlayer;

    [Header("CheckPoints")]
    [SerializeField] public Transform playerSpawnPoint;
    [SerializeField] public Transform playerStartPoint;
    [SerializeField] public Transform checkPointOne;
    [SerializeField] public Transform checkPointTwo;


    public bool pCBuild;
    public int coconuts;
    public int planks;
     
    private void Awake()
    {
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

    private void Update()
    {
        if (autoHandPlayer == null)
        {
            autoHandPlayer = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Start()
    {
        autoHandPlayer = GameObject.FindGameObjectWithTag("Player");
        //UICanvasController.Instance.SetPlayerTransform();
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
                UICanvasController.Instance.Dead();
                coconuts = 0;
                planks = 0;
            }
            else
            {
                UICanvasController.Instance.GameOver();
                coconuts = 0;
                planks = 0;
            }
        }
    }
}
