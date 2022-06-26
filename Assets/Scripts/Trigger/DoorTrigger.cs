using System.Collections;
using UnityEngine;

public class DoorTrigger : EventTrigger
{
    [Header("Door State")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool canOpen = true;

    protected float stayTime = -1f;
    protected float exitTime = 2.5f;

    #region Reachable Properties

    public bool CanOpen { get => canOpen; set => canOpen = value; }
    
    public float StayTime => stayTime;

    #endregion

    protected override void Start()
    {
        base.Start();
    }

    #region Trigger Event

    protected override void OnTriggerEnter(Collider collider)
    {
        //base.OnTriggerEnter(collider);

        if (invokeState != EventInvokeState.Passive) { return; }

        isTriggered = true;

        animator.SetBool("isOpen", isTriggered);

        StartCoroutine(StayOpen());
    }

    protected override void OnTriggerStay(Collider collider)
    {
        //base.OnTriggerStay(collider);

        if (invokeState != EventInvokeState.Interact) { return; }

        if (!isTriggered) { InteractHint(true); }

        if(Input.GetKeyDown(KeyCode.E) && !isTriggered) 
        {
            if (!canOpen) { Debug.Log("Could not Open."); return; }

            isTriggered = true;

            InteractHint(false);

            animator.SetBool("isOpen", isTriggered);

            StartCoroutine(StayOpen());
        }
    }

    protected override void OnTriggerExit(Collider collider)
    {
        //base.OnTriggerExit(collider);

        isTriggered = false;

        InteractHint(false);

        animator.SetBool("isOpen", false);
    }

    #endregion

    #region Door Operate

    public IEnumerator StayOpen() 
    {
        stayTime = 0f;

        while (true)
        {
            if (stayTime >= 0f) { stayTime += Time.deltaTime; }

            if (stayTime >= exitTime || stayTime <= -1f) { break; }

            yield return new WaitForEndOfFrame();
        }

        if (stayTime >= exitTime) { animator.SetBool("isOpen", false); }

        stayTime = -1;

        isTriggered = false;
    }

    #endregion
}
