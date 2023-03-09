using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private Transform passengerHead;

    private Transform originalHeadPostition;
    private Transform playerTranform;
    private AnimatorOverrideController overrideController;

    void Start()
    {
        originalHeadPostition = passengerHead.transform;

        // Get a reference to the NPC's animator controller
        Animator animator = GetComponent<Animator>();

        // Create a new Animation Override Controller
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        // Replace the original animator controller with the override controller
        animator.runtimeAnimatorController = overrideController;
        MoveNpcHead();
    }

    public void ResetNpcHead()
    {
        passengerHead = originalHeadPostition.transform;
    }

    /// <summary>
    /// Moves npcs head towards the playerTranform
    /// </summary>
    public void MoveNpcHead()
    {
        playerTranform = GameObject.FindWithTag("Player").transform;

        // Calculate the direction from the NPC's head to the playerTranform
        Vector3 directionToPlayer = (playerTranform.position - passengerHead.position).normalized;

        // Rotate the NPC's head to look at the playerTranform
        passengerHead.LookAt(playerTranform);

        // Use the original position of the NPC's head as a pivot point to rotate around
        passengerHead.RotateAround(originalHeadPostition.position, Vector3.up, 90f);

         // Override the head animation to prevent interference
        overrideController["BasicMotions@SitHigh01_A - Loop"] = null;
    }
}
