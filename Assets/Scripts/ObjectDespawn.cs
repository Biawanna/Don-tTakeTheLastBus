using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawn : MonoBehaviour
{
    public float timeout = 5f;

    void Start()
    {
        StartCoroutine(Timeout());
    }

    IEnumerator Timeout()
    {
        yield return new WaitForSeconds(timeout);
        Destroy(this.gameObject);
    }
}