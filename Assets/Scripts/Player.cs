using Autohand;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private InventoryScriptableObject inventory;

    private AutoHandPlayer autoHandPlayer;
    private SoundManager soundManager;
    private UICanvasController canvasController;

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
    }

    private void Update()
    {
        if (Input.GetButtonDown("XRI_Right_PrimaryButton"))
        {
            canvasController.Pause();
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
}
