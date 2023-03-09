using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private float soundRate;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] public AudioClip walkSound;
    [SerializeField] public AudioClip jumpSound;
    [SerializeField] public AudioClip jumpOnConcreteSound;
    [SerializeField] public AudioClip jumpOnGrassSound;
    [SerializeField] public AudioClip jumpOnWaterSound;
    [SerializeField] public AudioClip walkOnConcreteSound;
    [SerializeField] public AudioClip walkOnWaterSound;
    [SerializeField] public AudioClip walkOnGrassSound;

    private Coroutine soundRoutine;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            soundRoutine = StartCoroutine(RandomSoundCoroutine());
        }
    }

    public IEnumerator RandomSoundCoroutine()
    {
        while (true)
        {
            audioSource.PlayOneShot(RandomSound(audioClips));
            yield return new WaitForSeconds(soundRate);
        }
    }
    public void StopAudio(AudioSource audioSource)
    {
        audioSource.Stop();
    }

    public void StopSoundCoroutine()
    {
        StopCoroutine(soundRoutine);
    }
    public AudioClip RandomSound(AudioClip[] audioClips) => audioClips[Random.Range(0, audioClips.Length)];
}
