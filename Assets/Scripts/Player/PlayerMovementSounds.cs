using UnityEngine;

public class PlayerMovementSounds : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Concrete"))
        {
            soundManager.walkSound = soundManager.walkOnConcreteSound;

            Debug.Log("sound");
        }
    }
}
