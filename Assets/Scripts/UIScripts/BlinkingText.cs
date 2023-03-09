using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BlinkingText : MonoBehaviour
{
    [Header("Blinking Text Settings")]
    [SerializeField] private TextMeshProUGUI instructionText;
    [SerializeField] private int blinkWaitTime, blinkLength;
    public Color startColor = Color.green;
    public Color endColor = Color.black;
    [Range(0, 10)]
    public float speed = 1;
    private bool blink = true;
    private float blinkTimer;

    Canvas canvas;

    void Awake()
    {
        instructionText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        StartCoroutine(BlinkTimer());
    }
    public IEnumerator BlinkTimer()
    {
        while (blink)
        {
            blinkTimer += Time.deltaTime;

            if (blinkTimer <= blinkLength)
            {
                instructionText.enabled = true;
                instructionText.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
                yield return new WaitForSeconds(0);
            }

            if (blinkTimer >= blinkWaitTime)
            {
                instructionText.enabled = false;
                blinkTimer = 0;
                yield return new WaitForSeconds(blinkWaitTime);
            }
        }
    }
}