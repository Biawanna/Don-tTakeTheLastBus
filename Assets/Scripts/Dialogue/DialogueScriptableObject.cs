using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Box", menuName = "Dialogue")]

public class DialogueScriptableObject : ScriptableObject
{
    [Header("Dialogue Settings")]
    public bool dialoguePlayed = false;
    public DialogueSentenceType dialogueSentenceType;
    public DialoguePerson dialogueType;
    [TextArea(3, 5)]
    public string[] sentences;
}

