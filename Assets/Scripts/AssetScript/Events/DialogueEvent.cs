using UnityEngine;

[CreateAssetMenu(fileName = "Basic Event", menuName = "Events/Dialogue Event", order = 1)]
public class DialogueEvent : BasicEvents
{
    [SerializeField] protected string id;

    public override void EventInvoke()
    {
        base.EventInvoke();

        DialogueTrigger trigger = ObjectPool.GetStaff("DialogueTrigger", "SciptObject") as DialogueTrigger;

        trigger.TriggerDialogue(id);
    }

    public override void EventEnded()
    {
        base.EventEnded();

        if(newEvents.Length >= 0) { return; }

        foreach (BasicEvents events in newEvents) { events.EventInitialize(); }
    }
}
