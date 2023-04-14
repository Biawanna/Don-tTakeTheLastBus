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

    /// <summary>
    /// Fade in a canvas group.
    /// </summary>
    public void FadeIn(float duration)
    {
        Fade(1f, duration, () =>
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        });
    }

    /// <summary>
    /// Fade out a canvas group
    /// </summary>
    public void FadeOut(float duration)
    {
        Fade(0f, duration, () =>
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        });
    }

    /// <summary>
    /// Fade in a sprite.
    /// </summary>
    private void FadeInSprite(float duration, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(1f, duration);
    }

    /// <summary>
    /// Fade out a sprite.
    /// </summary>
    private void FadeOutSprite(float duration, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(0f, duration);
    }

    /// <summary>
    /// Fade in a Tmp.
    /// </summary>
    private void FadeInTMP(float duration, TextMeshPro tmp)
    {
        tmp.DOFade(1f, duration);
    }

    /// <summary>
    /// Fade out a Tmp.
    /// </summary>
    private void FadeOutTMP(float duration, TextMeshPro tmp)
    {
        tmp.DOFade(0f, duration);
    }

    /// <summary>
    /// Fade out a sprite immediately
    /// </summary>
    private void FadeOutSpriteImmediately(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.DOFade(0f, 0f);
    }

    /// <summary>
    /// Fade in coroutine.
    /// </summary>
    public void StartFadeInFadeOutRoutine()
    {
        fadeInOutRoutine = null;
        fadeInOutRoutine = StartCoroutine(FadeInOutSpriteRoutine());
    }

    /// <summary>
    /// Fade out sprite coroutine.
    /// </summary>
    private IEnumerator FadeInOutSpriteRoutine()
    {
        FadeIn(FadeInDuration);

        yield return new WaitForSeconds(delayBeforeFadeOut);

        FadeOut(FadeOutDuration);
        yield break;
    }

    /// <summary>
    /// Fade a canvas group.
    /// </summary>
    private void Fade(float endValue, float duration, TweenCallback onEnd)
    {
        fadeTween = canvasGroup.DOFade(endValue, duration);
        fadeTween.onComplete += onEnd;
    }
}