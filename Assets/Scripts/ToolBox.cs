using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ToolBox
{
    /// <summary>
    /// Enables a gameobject.
    /// </summary>
    public static void EnableGameObject(GameObject gameObject, bool enable)
    {
        gameObject.SetActive(enable);
    }

    /// <summary>
    /// Disables text after a certain time.
    /// </summary>
    public static IEnumerator TextRoutine(float destroyTime, TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(destroyTime);
        text.gameObject.SetActive(false);

        yield break;
    }

    /// <summary>
    /// Fades screen in/out and loads a new scene.
    /// </summary>
    public static IEnumerator GoToSceneAsyncRoutine(int sceneIndex, FadeScreen fadeScreen)
    {
        fadeScreen.FadeOut();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}
