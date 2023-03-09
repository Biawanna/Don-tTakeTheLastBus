
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private TMP_Text textRewardPrefab;
    [SerializeField] private float textMoveSpeed;
    [SerializeField] private float destroyTime;
    private float _timer;
    private Color _startColor;

    private void Start()
    {
        _startColor = textRewardPrefab.color;
    }
    void Update()
    {
        transform.Translate(0, textMoveSpeed * Time.deltaTime, 0);

        _timer += Time.deltaTime;
        textRewardPrefab.color = Color.Lerp(_startColor, Color.clear, _timer / destroyTime);

        if (_timer >= destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
