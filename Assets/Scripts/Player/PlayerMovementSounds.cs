using UnityEngine;
using Autohand;

public class PlayerMovementSounds : MonoBehaviour
{
    private SoundManager soundManager;
    private AutoHandPlayer autohandPlayer;

    private void Start()
    {
        soundManager = SoundManager.Instance;
        autohandPlayer = GetComponent<AutoHandPlayer>();
    }

    private void Update()
    {
        if (autohandPlayer.isMoving)
        {
            soundManager.PlayWalkSound();
        }
        else
        {
            soundManager.StopWalkSound();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Concrete"))
        {
            soundManager.walkSound = soundManager.walkOnConcreteSound;
        }
    }
}
