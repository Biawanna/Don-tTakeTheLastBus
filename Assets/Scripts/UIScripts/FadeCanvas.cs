using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    [field: SerializeField] public float FadeInDuration { get; private set; }
    [field: SerializeField] public float FadeOutDuration { get; private set; }

    [SerializeField] private float delayBeforeFadeOut;
    [SerializeField] private CanvasGroup canvasGroup;

    Tween fadeTween;
    Coroutine fadeInOutRoutine;

    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    private void FadeInSprite(float duration, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(1f, duration);
    }


    private void FadeOutSprite(float duration, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(0f, duration);
    }
    private void FadeInTMP(float duration, TextMeshPro tmp)
    {
        tmp.DOFade(1f, duration);
    }


    private void FadeOutTMP(float duration, TextMeshPro tmp)
    {
        tmp.DOFade(0f, duration);
    }

    private void FadeOutSpriteImmediately(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(0f, 0f);
    }


    public void StartFadeInFadeOutRoutine()
    {
        fadeInOutRoutine = null;
        fadeInOutRoutine = StartCoroutine(FadeInOutSpriteRoutine());
    }

    private IEnumerator FadeInOutSpriteRoutine()
    {
        FadeIn(FadeInDuration);

        yield return new WaitForSeconds(delayBeforeFadeOut);

        FadeOut(FadeOutDuration);
        yield break;
    }

    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}