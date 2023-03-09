using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRise : MonoBehaviour
{
    public float speed = 0.1f;

    Vector3 tempPos = new Vector3();

    private void Update()
    {
        tempPos = transform.position;
        tempPos.y += speed * Time.deltaTime;
        transform.position = tempPos;
    }
}
