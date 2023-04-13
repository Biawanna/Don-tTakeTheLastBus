using UnityEngine;

public class PlayerMovementSounds : MonoBehaviour
{
    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = SoundManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
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
