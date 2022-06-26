using UnityEngine;
using UnityEngine.UI;

public class EventTrigger : MonoBehaviour
{
    public enum EventInvokeState 
    {
        Start = 0,
        Passive = 1,
        Interact = 2,
    }

    [Header("Trigger State")]
    [SerializeField] protected EventInvokeState invokeState = EventInvokeState.Interact;

    [Header("Event Hint")]
    [SerializeField] protected string eventType;

    [Header("Destroy After Triggered")]
    [SerializeField] protected bool destroy = false;

    protected InformationUI informationUI;
    protected KeyManager keyManager;
    
    protected GameObject hint;
    protected Text eventTypeText;
    protected Text eventKeyCode;

    protected bool isTriggered = false;

    #region Reachable Properties

    public bool IsTriggered => isTriggered;

    public bool IsTriggeredDestroy => destroy;

    #endregion

    protected virtual void Start()
    {
        informationUI = ObjectPool.GetStaff("InformationUI", "ScriptObject") as InformationUI;

        keyManager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;
    }

    #region Trigger Event

    protected virtual void OnTriggerEnter(Collider collider) 
    {
        Debug.Log($"{collider.name} Enter.");
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
        Debug.Log($"{collider.name} Stay.");
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        Debug.Log($"{collider.name} Exit.");
    }

    #endregion

    protected virtual void StartInvoke() 
    {
        Debug.Log("Start Invoking");
    }

    protected virtual void InteractHint(bool state)
    {
        if (hint == null) { hint = informationUI.EventHint; }

        if (eventTypeText == null) { eventTypeText = informationUI.EventType; }

        if (eventKeyCode == null) { eventKeyCode = informationUI.EventKeyCode; }

        hint.SetActive(state);

        eventTypeText.text = eventType;

        eventKeyCode.text = $"{keyManager[KeyState.Event]}";
    }
}
