using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private float soundRate;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioSource screamAudioSource;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip[] screamSounds;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] public AudioClip walkSound;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip jumpOnConcreteSound;
    [SerializeField] public AudioClip jumpOnGrassSound;
    [SerializeField] public AudioClip jumpOnWaterSound;
    [SerializeField] public AudioClip walkOnConcreteSound;
    [SerializeField] public AudioClip walkOnWaterSound;
    [SerializeField] public AudioClip walkOnGrassSound;

    [Header("Button Settings")]
    [SerializeField] private float minButtonPitch;
    [SerializeField] private float maxButtonPitch;

    [Header("Scream Settings")]
    [SerializeField] private float minScreamPitch;
    [SerializeField] private float maxScraemPitch;

    //private Coroutine soundRoutine;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Plays an audio clip
    /// </summary>
    public void PlaySound(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Plays an audio clip with a random pitch between two ranges
    /// </summary>
    private void RandomPitch(AudioSource audioSource, float minPitch, float maxPitch)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
    }

    /// <summary>
    /// Plays the button audio clip with a random pitch between two ranges
    /// </summary>
    public void PlayButtonSound(AudioSource audioSource)
    {
        RandomPitch(audioSource, minButtonPitch, maxButtonPitch);
        PlaySound(audioSource, buttonSound);
    }

    public void PlayRandomScreamSound()
    {
        AudioClip audioClip = screamSounds[Random.Range(0, screamSounds.Length)];
        RandomPitch(screamAudioSource, minScreamPitch, maxScraemPitch);
        PlaySound(screamAudioSource, audioClip);
    }
}
