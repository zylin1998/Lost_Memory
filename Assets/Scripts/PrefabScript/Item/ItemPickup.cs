using UnityEngine;

public class ItemPickup : EventTrigger
{
    #region Parameter Field

    [Header("Target GameObject")]
    [SerializeField] private GameObject target;
    [Header("Item")]
    [SerializeField] private Item item;

    #endregion

    public Item Item => item;

    #region Private Function

    protected override void OnTriggerEnter(Collider collider)
    {
        if(invokeState != EventInvokeState.Passive) { return; }

        isTriggered = Inventory.instance.Add(item);

        if (isTriggered) 
        {
            PickedUp();

            if(destroy) { Destroy(target); }
        }
    }

    protected override void OnTriggerStay(Collider collider)
    {
        if(invokeState != EventInvokeState.Interact) { return; }

        if(!isTriggered) { InteractHint(true); }

        if (keyManager.EventState)
        {
            isTriggered = Inventory.instance.Add(item);

            if (isTriggered)
            {
                InteractHint(false);

                PickedUp();

                if (destroy) { Destroy(target); }
            }
        }
    }

    protected override void OnTriggerExit(Collider collider)
    {
        isTriggered = false;

        InteractHint(false);
    }

    protected virtual void PickedUp() 
    {
        item.PickUp();
    }

    #endregion
}
