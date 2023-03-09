using UnityEngine;

public class DestroyableItem : MonoBehaviour
{
    [SerializeField] private float lifeSpan;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if( timer >= lifeSpan )
        {
            Destroy(gameObject);
        }
    }
}


