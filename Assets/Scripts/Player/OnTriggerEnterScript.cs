using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterScript : MonoBehaviour
{
    public UnityEvent OnTriggerEnterEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }
}
