using UnityEngine;
using DG.Tweening;
public class DialogueSequence : MonoBehaviour
{
    /// <summary>
    /// This script handles dialogueManager animations
    /// </summary>
    [SerializeField] private AnimSequenceScriptableObject animSequence;
    [SerializeField] private GameObject dialogueObject;
    public void OpenCloseDialogueSequence(bool show)
    {
        if (show)
        {
            dialogueObject.transform.DOScale(animSequence.originalPosition, animSequence.animOpenDuration).SetEase(Ease.InOutSine).SetDelay(animSequence.openAnimDelay);
        }
        else
        {
            CloseDialogueSequence();
        }
    }

    /// <summary>
    /// Closes the dialogueManager immediately
    /// </summary>
    public void CloseDialogueSequenceImmediately()
    {
        dialogueObject.transform.DOScale(Vector3.zero, 0).SetEase(Ease.InElastic).SetDelay(0);
    }

    /// <summary>
    /// Closes the dialogueManager with delay/duration parameters
    /// </summary>
    private void CloseDialogueSequence()
    {
        dialogueObject.transform.DOScale(Vector3.zero, animSequence.animCloseDuration).SetEase(Ease.InBack).SetDelay(animSequence.closeAnimDelay);
    }
}
