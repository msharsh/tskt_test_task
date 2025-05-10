using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnHoverBegin;
    public UnityEvent OnHoverEnd;

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverBegin.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverEnd.Invoke();
    }
}
