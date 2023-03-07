using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueScriptableObject dialogue;

    public void TriggerDialogue()
    {
        if (!dialogue.dialoguePlayed)
        {
            Dialogue.instance.StartDialogue(dialogue);
            dialogue.dialoguePlayed = true;
        }
    }
}
