using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public bool checkPointOne = false;
    public bool checkPointTwo = false;

    public void ChangeCheckPointOneBool()
    {
        checkPointOne = true;
    }
    public void ChangeCheckPointTwoBool()
    {
        checkPointTwo = true;
    }
    public void SetPlayerTransform()
    {
        GameManager.instance.autoHandPlayer.transform.position = GameManager.instance.playerSpawnPoint.position;
        GameManager.instance.autoHandPlayer.transform.rotation = GameManager.instance.playerSpawnPoint.rotation;
    }

    public void ChangeCheckPoint()
    {
        if (checkPointOne)
        {
            GameManager.instance.playerSpawnPoint = GameManager.instance.checkPointOne;
        }
        if (checkPointTwo)
        {
            GameManager.instance.playerSpawnPoint = GameManager.instance.checkPointTwo;
        }
    }
    public void ObjectDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
