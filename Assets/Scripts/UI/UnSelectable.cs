using UnityEngine;
using UnityEngine.EventSystems;

public class UnSelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Target Selection")]
    [SerializeField] private Selection selection;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        selection.OnPointerEnter(eventData);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        selection.OnPointerExit(eventData);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        selection.Select();
    }
}
