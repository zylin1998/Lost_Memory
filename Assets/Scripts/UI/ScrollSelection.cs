using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollSelection : Selection, ISelectHandler
{
    [SerializeField] protected string scrCtrName;

    [Header("Button Detail")]
    [SerializeField] protected Vector2Int locate;

    protected ScrollController scrollController;

    public Vector2Int Locate => locate;

    protected override void Start()
    {
        base.Start();

        scrollController = ObjectPool.GetStaff(scrCtrName, "ScriptObject") as ScrollController;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        scrollController.Selected(locate);
    }
}
