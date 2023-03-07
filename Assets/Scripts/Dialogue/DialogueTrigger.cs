using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueScriptableObject dialogueScriptable;
    [SerializeField] private GameObject dialoguePopup;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Dialogue dialogue;

    public GameObject DialoguePopup
    {
        get { return dialoguePopup; }
    }


    private void Start()
    {
        if(dialogueText != null)
        {
            dialogueText.text = " ";
        }
    }

    public void TriggerDialogue()
    {
        //if (!dialogueScriptable.dialoguePlayed)
        //{
        if (dialogue.DialogueText == null)
        {
            dialogue.DialogueText = dialogueText;
            dialogue.StartDialogue(dialogueScriptable);
            dialogueScriptable.dialoguePlayed = true;
        }
        //}
    }
}
