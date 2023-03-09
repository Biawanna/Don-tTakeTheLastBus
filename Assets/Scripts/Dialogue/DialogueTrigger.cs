using TMPro;
using UnityEngine;

/// <summary>
/// This script triggers dialogue.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogue References")]
    [SerializeField] private DialogueScriptableObject[] dialogueScriptable;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private DialogueManager dialogueManager;

    private NPC npc;
    private DialogueSequence sequence;
    private DialogueScriptableObject currentDialogue;
    private int firstElement = 0;
    private int dialogueIndex = 0;

    public int DialogueIndex 
    { get { return dialogueIndex; } set { dialogueIndex = value; } }

    public DialogueScriptableObject CurrentDialogue
    { get { return currentDialogue; } set { currentDialogue = value; } }


    private void Start()
    {
        npc = GetComponent<NPC>();
        sequence = gameObject.GetComponent<DialogueSequence>();

        if (dialogueText != null)
        {
            sequence.CloseDialogueSequenceImmediately();
            dialogueText.text = " ";
        }

        if(dialogueScriptable != null) { currentDialogue = dialogueScriptable[dialogueIndex]; }
    }

    /// <summary>
    /// Triggers the dialogue open methods
    /// </summary>
    public void TriggerDialogue()
    {
        if (dialogueManager.DialogueText == null && !currentDialogue.dialoguePlayed)
        {
            OpenDialogue();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OpenDialogue()
    {
        //npc.MoveNpcHead();

        SetDialogueTriggerText();
        dialogueManager.OpenPassengerDialogue();
        OpenDialogueSequence();
    }

    /// <summary>
    /// Closes the dialogue sequence.
    /// </summary>
    public void CloseDialogue()
    {
        //npc.ResetNpcHead();

        CloseDialogueSequence();
        ClearDialogueText(dialogueText);
    }

    /// <summary>
    /// Opens a random dialogue sequence.
    /// </summary>
    public void PlayRandomDialogue()
    {
        int randomIndex = Random.Range(0, dialogueScriptable.Length);
        currentDialogue = dialogueScriptable[randomIndex];
    }

    /// <summary>
    /// Sets the current dialogue index.
    /// </summary>
    private void SetCurrentDialogueIndex(int index)
    {
        currentDialogue = dialogueScriptable[index];
    }

    /// <summary>
    /// Clears TextMeshProUGUI text, sets dialogue field to null.
    /// </summary>
    private void ClearDialogueText(TextMeshProUGUI text)
    {
        text.text = " ";
        dialogueManager.DialogueText = null;
    }

    /// <summary>
    /// Sets the dialogue trigger to the object thus script is on.
    /// Sets dialogue twxt to the text on this script.
    /// </summary>
    private void SetDialogueTriggerText()
    {
        dialogueManager.DialogueText = dialogueText;
        dialogueManager.DialogueTrigger = this;
    }

    /// <summary>
    /// Triggers the close dialogue sequence animation method.
    /// </summary>
    private void CloseDialogueSequence()
    {
        sequence.OpenCloseDialogueSequence(false);
    }

    /// <summary>
    /// Triggers the open dialogue sequence animation method.
    /// </summary>
    private void OpenDialogueSequence()
    {
        sequence.OpenCloseDialogueSequence(true);
    }
}
