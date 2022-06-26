using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;

    private EventTrigger eventTrigger;

    private void Awake()
    {
        eventTrigger = this.GetComponent<EventTrigger>();
    }

    private void OnTriggerStay(Collider collider)
    {
        if (eventTrigger.IsTriggered) { Teleport(); }
    }

    private void Teleport() 
    {
        if(targetScene == string.Empty) { return; }

        StaticValue.targetScene = targetScene;

        StaticValue.userData.player.SetPosition(position);
        StaticValue.userData.player.SetRotation(rotation);

        var loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        if (loadScenes != null) { loadScenes.LoadNewScene(); }
    }
}
