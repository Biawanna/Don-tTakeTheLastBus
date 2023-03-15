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

    [Header("UI References")]
    [SerializeField] private GameObject yesButton;
    [SerializeField] private GameObject noButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject skipButton;

    private DialogueManager dialogueManager;
    private NPC npc;
    private DialogueSequence sequence;
    private DialogueScriptableObject currentDialogue;
    private int firstElement = 0;
    private int dialogueIndex = 0;

    public int DialogueIndex 
    { get { return dialogueIndex; } set { dialogueIndex = value; } }

    public DialogueScriptableObject CurrentDialogue
    { get { return currentDialogue; } set { currentDialogue = value; } }

    public Sprite Icon
        { get { return currentDialogue.icon; } private set { } }


    private void Start()
    {
        dialogueManager = DialogueManager.instance;

        npc = GetComponent<NPC>();

        sequence = gameObject.GetComponent<DialogueSequence>();

        if (dialogueText != null)
        {
            sequence.CloseDialogueSequenceImmediately();
        }

        if (dialogueScriptable != null) { currentDialogue = dialogueScriptable[dialogueIndex]; }

        HideOnStart();
    }

    /// <summary>
    /// Triggers the dialogue open methods
    /// </summary>
    public void TriggerDialogue()
    {
        OpenDialogue();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OpenDialogue()
    {
        if (!dialogueManager.DialogueInPlay)
        {
            SetDialogueTriggerText();
            dialogueManager.OpenPassengerDialogue();
            OpenDialogueSequence();
            dialogueManager.DialogueInPlay = true;
        }
    }

    /// <summary>
    /// Closes the dialogue sequence.
    /// </summary>
    public void CloseDialogue()
    {
        CloseDialogueSequence();
        ClearDialogueText(dialogueText);
        dialogueManager.DialogueInPlay = false;
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
    /// Increase the current dialogue index.
    /// </summary>
    public void IncreaseCurrentDialogueIndex()
    {
        if (dialogueIndex + 1 > dialogueScriptable.Length -1)
        {
            return;
        }
        else
        {
            currentDialogue = dialogueScriptable[dialogueIndex + 1];
            dialogueIndex++;
        }
    }

    /// <summary>
    /// Toggles the yes, no, skip and nxt buttons.
    /// True sets the yes and no buttons visible.
    /// </summary>
    /// <param name="showOptions"></param>
    public void ToggleDialogueOptions(bool showOptions)
    {
        yesButton.SetActive(showOptions);
        noButton.SetActive(showOptions);
        skipButton.SetActive(!showOptions);
        nextButton.SetActive(!showOptions);
    }

    public void ToggleYesNoButtons(bool showOptions)
    {
        yesButton.SetActive(showOptions);
        noButton.SetActive(showOptions);
    }

    public void ToggleObjectsVisibilty(bool visibility, GameObject[] gameObject)
    {
        foreach (GameObject obj in gameObject)
        {
            obj.SetActive(visibility);
        }
    }
    public void ToggleObjectVisibilty(bool visibility, GameObject gameObject)
    {
        gameObject.SetActive(visibility);
    }

    /// <summary>
    /// Clears TextMeshProUGUI text, sets dialogue field to null.
    /// </summary>
    public void ClearDialogueText(TextMeshProUGUI text)
    {
        text.text = " ";
        dialogueManager.DialogueText = null;
        dialogueManager.DialogueInPlay = false;
    }

    /// <summary>
    /// Sets the dialogue trigger to the object thus script is on.
    /// Sets dialogue twxt to the text on this script.
    /// </summary>
    private void SetDialogueTriggerText()
    {
        dialogueManager.DialogueText = dialogueText;
        dialogueManager.CurrentDialogueTrigger = this;
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

    private void HideOnStart()
    {
        ToggleDialogueOptions(false);
    }
}
