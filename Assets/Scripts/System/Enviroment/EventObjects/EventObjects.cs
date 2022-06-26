using System.Collections.Generic;
using UnityEngine;

public class EventObjects : MonoBehaviour
{
    [SerializeField] private string currentSceneName;
    [SerializeField] private List<GameObject> onSceneEvents;

    private EventList eventList;
    
    private Transform eventParent;

    public EventList EventList => eventList;

    private void Awake()
    {
        ObjectPool.Add(this, "EventObjects", "ScriptObject");
    }

    void Start()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        eventList = Resources.Load<EventList>(System.IO.Path.Combine("Events", "Event List"));
    
        eventParent = GameObject.Find("EventObjects").transform;

        Initialized();
    }

    public void Initialized()
    {
        eventList.eventGroup = StaticValue.userData.eventGroups;

        EventList.SceneEvents list = eventList[currentSceneName];

        if (list == null) { return; }

        foreach (string objectName in list.eventObjects) 
        {
            GameObject gameObject = Resources.Load<GameObject>(System.IO.Path.Combine("EventObjects", currentSceneName, objectName));

            GameObject added = Instantiate(gameObject, eventParent);
            added.name = objectName;

            added.name = gameObject.name;

            onSceneEvents.Add(added);
        }
    }

    public EventList GetCurrentEvents() 
    {
        List<string> list = new List<string>();

        foreach (GameObject gameObject in onSceneEvents) 
        {
            if (gameObject != null) 
            {
                string temp = gameObject.name;

                list.Add(temp);
            }
        }

        var sceneEvents = new EventList.SceneEvents(currentSceneName, list.ToArray());

        eventList.ResetGroup(sceneEvents);

        return eventList;
    }
}
