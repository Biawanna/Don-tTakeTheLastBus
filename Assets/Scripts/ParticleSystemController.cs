using UnityEngine;
using System.Collections;

public class ParticleSystemController : MonoBehaviour
{
    public new ParticleSystem particleSystem;
    public float restartTime = 5f;

    private void Start()
    {
        StartCoroutine(RestartParticleSystem());
    }

    private IEnumerator RestartParticleSystem()
    {
        while (true)
        {
            yield return new WaitForSeconds(restartTime);
            particleSystem.Pause();
            yield return new WaitForSeconds(restartTime);
            particleSystem.Play();
        }
    }
}
