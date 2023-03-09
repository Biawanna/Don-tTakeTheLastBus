using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{
    [Header("Blinking Image Settings")]
    [SerializeField] private Image imgComp;
    public Color startColor = Color.green;
    public Color endColor = Color.black;
    [Range(0, 10)]
    public float speed = 1;

    void Awake()
    {
        imgComp = GetComponent<Image>();
    }
    void Update()
    {
        imgComp.color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}