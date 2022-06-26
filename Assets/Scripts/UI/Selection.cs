using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Selection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    [Header("Selection Controller")]
    [SerializeField] protected string slcCtrName;
    
    protected SelectionController selectionController;

    protected Selectable selectable;

    protected bool selected = false;

    public bool Selected => selected;

    protected virtual void Start()
    {
        selectionController = ObjectPool.GetStaff(slcCtrName, "ScriptObject") as SelectionController;

        selectable = GetComponent<Selectable>();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        selectable.Select();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        selectionController.PointExit();
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        selectionController.Selected(GetComponent<Selectable>());

        selected = true;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        selectable.Select();
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        selected = false;
    }

    public virtual void Select() 
    {
        selectable.Select();
    }
}
