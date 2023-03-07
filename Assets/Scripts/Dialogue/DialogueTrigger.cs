using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueScriptableObject dialogueScriptable;
    [SerializeField] private GameObject dialoguePopup;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Dialogue dialogue;

    private DialogueSequence sequence;

    public GameObject DialoguePopup
    {
        get { return dialoguePopup; }
    }


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
        if (dialogue.DialogueText == null)
        {
            OpenDialogue();
        }
        //}
    }

    public void OpenDialogue()
    {
        dialogue.DialogueText = dialogueText;
        dialogue.DialogueTrigger = this;

        dialogue.StartDialogue(dialogueScriptable);
        dialogueScriptable.dialoguePlayed = true;
        sequence.OpenCloseDialogueSequence(true);
    }

    public void CloseDialogue()
    {
        sequence.OpenCloseDialogueSequence(false);
        dialogueText.text = " ";
        dialogue.DialogueText = null;
    }
}
