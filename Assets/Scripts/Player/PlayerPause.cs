using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    private UICanvasController canvasController;
    private GameManager gameManager;

    private void Start()
    {
        canvasController = UICanvasController.Instance;
        gameManager = GameManager.instance;
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
}
