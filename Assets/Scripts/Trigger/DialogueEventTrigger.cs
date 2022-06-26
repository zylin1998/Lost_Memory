using UnityEngine;

public class DialogueEventTrigger : EventTrigger
{
    [SerializeField] protected string id;

    private bool isDialogue = false;

    public string ID => id;

    public bool IsDialogue => isDialogue;

    protected override void Start()
    {
        base.Start();

        StartInvoke();
    }

    #region Trigger Event

    protected override void OnTriggerEnter(Collider collider)
    {
        //base.OnTriggerEnter(collider);

        if (invokeState != EventInvokeState.Passive) { return; }

        (ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger).TriggerDialogue(id);

        if (destroy) { Destroy(gameObject); }
    }

    protected override void OnTriggerStay(Collider collider)
    {
        //base.OnTriggerStay(collider);

        if (invokeState != EventInvokeState.Interact) { return; }

        if (!isDialogue) { InteractHint(true); }

        if (Input.GetKeyDown(KeyCode.E) && !isDialogue)
        {
            isDialogue = true;

            (ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger).TriggerDialogue(id);

            InteractHint(false);

            if (destroy) { Destroy(gameObject); }
        }
    }

    protected override void OnTriggerExit(Collider collider)
    {
        //base.OnTriggerExit(collider);

        isTriggered = false;

        InteractHint(false);
    }

    #endregion

    protected override void StartInvoke()
    {
        //base.StartInvoke();

        if (invokeState != EventInvokeState.Start) { return; }

        (ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger).TriggerDialogue(id);

        if (destroy) { Destroy(gameObject); }
    }
}
