using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnMouseEnterScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color color;
    public Renderer rend;

    public UnityEvent MouseEnterEvent;
    public UnityEvent MouseExitEvent;
    public UnityEvent MouseUpEvent;
    public UnityEvent MouseDownEvent;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        ChangeObjectColor();
        MouseEnterEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        ObjectColorBackToNormal();
        MouseExitEvent?.Invoke();
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        MouseDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        MouseUpEvent?.Invoke();
    }

    public void ChangeObjectColor()
    {
        rend.material.color = color;
    }

    public void ObjectColorBackToNormal()
    {
        rend.material.color = Color.white;
    }
}