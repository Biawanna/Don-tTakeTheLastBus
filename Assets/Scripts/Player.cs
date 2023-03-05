using Autohand;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private AutoHandPlayer autoHandPlayer;
    [SerializeField] public float playerHealth = 1;

    private SoundManager soundManager;

    public AutoHandPlayer AutoHandPlayer
    {
        get { return autoHandPlayer; }
    }

    private void Awake()
    {
        Instance = this;
        soundManager = SoundManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water"))
        {
            soundManager.walkSound = soundManager.walkOnWaterSound;
            soundManager.jumpSound = soundManager.jumpOnWaterSound;
        }
        if (other.CompareTag("Grass"))
        {
            soundManager.walkSound = soundManager.walkOnGrassSound;
            soundManager.jumpSound = soundManager.jumpOnGrassSound;
        }
        if (other.CompareTag("Concrete"))
        {
            soundManager.walkSound = soundManager.walkOnConcreteSound;
            soundManager.jumpSound = soundManager.jumpOnConcreteSound;
        }
    }
}
