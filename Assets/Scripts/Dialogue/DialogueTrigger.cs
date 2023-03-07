using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueScriptableObject dialogueScriptable;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Dialogue dialogueManager;

    private DialogueSequence sequence;

    private void Start()
    {
        sequence = gameObject.GetComponent<DialogueSequence>();

        if (dialogueText != null)
        {
            sequence.CloseDialogueSequenceImmediately();
            dialogueText.text = " ";
        }
    }

    public void TriggerDialogue()
    {
        //if (!dialogueScriptable.dialoguePlayed)
        //{
        if (dialogueManager.DialogueText == null)
        {
            OpenDialogue();
        }
        //}
    }

    public void OpenDialogue()
    {
        dialogueManager.DialogueText = dialogueText;
        dialogueManager.DialogueTrigger = this;

        dialogueManager.StartDialogue(dialogueScriptable);
        dialogueScriptable.dialoguePlayed = true;
        sequence.OpenCloseDialogueSequence(true);
    }

    public void CloseDialogue()
    {
        sequence.OpenCloseDialogueSequence(false);
        dialogueText.text = " ";
        dialogueManager.DialogueText = null;
    }
}
