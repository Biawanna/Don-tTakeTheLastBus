
using UnityEngine;
public class HandVisuals : MonoBehaviour
{
    [SerializeField] private Animator handAnim;
    [SerializeField] private string gripButton;

    void Update()
    {
        if (Input.GetButtonDown(gripButton))
        {
            handAnim.SetBool("Gripped", true);
        }

        if (Input.GetButtonUp(gripButton))
        {
            handAnim.SetBool("Gripped", false);
        }
    }
}
