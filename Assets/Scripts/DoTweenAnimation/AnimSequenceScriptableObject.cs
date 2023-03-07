using UnityEngine;

[CreateAssetMenu(fileName = "New Anim Sequence", menuName = "Anim Sequence")]
public class AnimSequenceScriptableObject : ScriptableObject
{
    [SerializeField] public bool animOpenReverse;
    [SerializeField] public bool animCloseReverse;
    public Vector3 originalPosition;
    public float animOpenDuration;
    public float animCloseDuration;
    public float openAnimDelay;
    public float closeAnimDelay;
}
