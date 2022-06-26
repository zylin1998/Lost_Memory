using System.Collections;
using UnityEngine;

public class PictureTrigger : EventTrigger
{
    [Header("Image State")]
    [SerializeField] protected int imageID;
    [SerializeField] protected bool hasImage;
    
    [SerializeField] protected InformImage image;

    #region Reachable Properties

    public bool HasImage => hasImage;

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
    }

    protected override void OnTriggerStay(Collider collider)
    {
        //base.OnTriggerStay(collider);

        if (invokeState != EventInvokeState.Interact) { return; }

        if (!isTriggered) { InteractHint(true); }

        if (Input.GetKeyDown(KeyCode.E) && !isTriggered)
        {
            isTriggered = true;

            if (hasImage) { StartCoroutine(OpenImage()); }

            if (!hasImage) { Debug.Log("Has No Image"); }
        }
    }

    protected override void OnTriggerExit(Collider collider)
    {
        //base.OnTriggerExit(collider);

        isTriggered = false;

        InteractHint(false);
    }

    #endregion

    protected IEnumerator OpenImage() 
    {
        InteractHint(false);

        Cursor.lockState = CursorLockMode.None;
        EnviromentManager.instance.ImageMode = true;
        
        image.gameObject.SetActive(true);
        image.targetImage.sprite = EnviromentManager.instance.MIFrames.frames[imageID].sprite;
        StartCoroutine(image.PictureDisplay());

        while (image.gameObject.activeSelf) { yield return null; }

        Cursor.lockState = CursorLockMode.Locked;
        EnviromentManager.instance.ImageMode = false;

        isTriggered = false;
    }
}
