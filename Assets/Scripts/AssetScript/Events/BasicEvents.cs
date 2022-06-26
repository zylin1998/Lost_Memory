using UnityEngine;

[CreateAssetMenu(fileName = "Basic Event", menuName = "Events/Basic Event",order = 1)]
public class BasicEvents : ScriptableObject
{
    [Header("Instantiate GameObject")]
    [SerializeField] protected bool hasGameObject = false;
    [SerializeField] protected GameObject gameObject;

    [SerializeField] protected BasicEvents[] newEvents;

    public virtual void EventInitialize() 
    {
        Debug.Log($"Event {name} Initialized.");

        if(!hasGameObject) { return; }

        Transform transform = gameObject.transform;

        GameObject parent = GameObject.Find("EventObjects");

        Instantiate(gameObject, transform.position, transform.rotation, parent.transform);
    }

    public virtual void EventInvoke() 
    {
        Debug.Log($"Event {name} Invoked.");
    }

    public virtual void EventEnded() 
    {
        Debug.Log($"Event {name} Ended.");
    }
}
