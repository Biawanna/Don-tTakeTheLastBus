using Autohand;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public AutoHandPlayer autoHandPlayer;

    [SerializeField] public float playerHealth = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        autoHandPlayer = GetComponentInChildren<AutoHandPlayer>();

        transform.position = GameManager.instance.playerSpawnPoint.position;
        transform.rotation = GameManager.instance.playerSpawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            SoundManager.Instance.walkSound = SoundManager.Instance.walkOnWaterSound;
            SoundManager.Instance.jumpSound = SoundManager.Instance.jumpOnWaterSound;
        }
        if (other.CompareTag("Grass"))
        {
            SoundManager.Instance.walkSound = SoundManager.Instance.walkOnGrassSound;
            SoundManager.Instance.jumpSound = SoundManager.Instance.jumpOnGrassSound;
        }
        if (other.CompareTag("Concrete"))
        {
            SoundManager.Instance.walkSound = SoundManager.Instance.walkOnConcreteSound;
            SoundManager.Instance.jumpSound = SoundManager.Instance.jumpOnConcreteSound;
        }
    }
}
