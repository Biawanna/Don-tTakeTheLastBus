using Autohand;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private InventoryScriptableObject inventory;
    [SerializeField] private LineRenderer rightGrabPointer;
    [SerializeField] private LineRenderer leftGrabPointer;

    private AutoHandPlayer autoHandPlayer;
    private SoundManager soundManager;
    private UICanvasController canvasController;
    private GameManager gameManager;

    public InventoryScriptableObject Inventory { get { return inventory; } }

    public AutoHandPlayer AutoHandPlayer { get { return autoHandPlayer; } }


    private void Awake()
    {
        Instance = this;
        soundManager = SoundManager.Instance;
    }

    private void Start()
    {
        canvasController = UICanvasController.Instance;
        gameManager = GameManager.instance;

        leftGrabPointer.enabled = false;
        rightGrabPointer.enabled = false;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && !gameManager.GameOver)
        {
            if (Input.GetButtonDown("XRI_Right_PrimaryButton") || Input.GetButtonDown("XRI_Left_PrimaryButton"))
            {
                canvasController.Pause();
            }
        }
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

    public void SetDogBoneTrue()
    {
        inventory.dogBone = true;
    }

    public void SetPopCanTrue()
    {
        inventory.popCan = true;
    }
    public void SetNewspaperTrue()
    {
        inventory.newspaper = true;
    }

    public void SetCoffeeTrue()
    {
        inventory.coffee = true;
    }

    public void SetNewspaperGivenTrue()
    {
        inventory.newspaperGiven = true;
    }
    public void SetCoffeeGiveTrue()
    {
        inventory.coffeeGiven = true;
    }
    public void SetWaterGivenTrue()
    {
        inventory.waterGiven = true;
    }
}
