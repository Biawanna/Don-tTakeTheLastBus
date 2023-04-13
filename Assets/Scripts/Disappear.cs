using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    [SerializeField] private List<GameObject> disappearObjects = new List<GameObject>();

    [Header("Disappear Settings")]
    [SerializeField] private float disappearDelay;
    [SerializeField] private float reappearDelay;

    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;

    [SerializeField] private Material material1;
    [SerializeField] private Material material2;
    private float duration = 2.0f;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;
    void Start()
    {
        StartCoroutine(MakeDisappearRoutine());
    }

    public IEnumerator MakeDisappearRoutine()
    {
        while(true)
        {
            foreach (GameObject obj in disappearObjects)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

                meshRenderer = obj.GetComponent<MeshRenderer>();
                boxCollider = obj.GetComponent<BoxCollider>();

                boxCollider.enabled = false;

                meshRenderer.material = material2;
            }

            foreach (GameObject obj in disappearObjects)
            {
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));

                meshRenderer = obj.GetComponent<MeshRenderer>();
                boxCollider = obj.GetComponent<BoxCollider>();
                boxCollider.enabled = true;

                meshRenderer.material = material1;
            }
        }
    }
}
