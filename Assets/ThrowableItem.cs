using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowableItem : MonoBehaviour
{
    public static ThrowableItem instance;

    [SerializeField] public GameObject hitParticle;
    [SerializeField] public Transform particleSpawnPoint;
    [SerializeField] public AudioClip throwableSound;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        GameObject sound = GameObject.FindGameObjectWithTag("soundfx");
        audioSource = sound.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag("target") || collision.transform.CompareTag("plane"))
        {
            audioSource.PlayOneShot(throwableSound);
            Instantiate(hitParticle, particleSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}

