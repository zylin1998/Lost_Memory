using UnityEngine;

[CreateAssetMenu(fileName = "Event List", menuName = "Events/Event List", order = 1)]
public class EventList : ScriptableObject
{
    [System.Serializable]
    public class SceneEvents 
    {
        public string targetScene;

        public string[] eventObjects;

        #region  Construction

        public SceneEvents() 
        {
            this.targetScene = string.Empty;
            this.eventObjects = null;
        }

        public SceneEvents(string name, string[] gameObjects) 
        {
            this.targetScene = name;
            this.eventObjects = gameObjects;
        }

        #endregion

        public void Reset(string[] gameObjects) 
        {
            eventObjects = gameObjects;
        }
    }

    public SceneEvents[] eventGroup;

    #region Reachable Properties

    public SceneEvents this[int num] => eventGroup[num];

    public SceneEvents this[string sceneName] 
    {
        get 
        {
            foreach(SceneEvents events in eventGroup)
            {
                if(events.targetScene.ToLower() == sceneName.ToLower()) { return events; }
            }

            return null;
        }
    }

    #endregion

    public void ResetGroup(SceneEvents group) 
    {
        this[group.targetScene].Reset(group.eventObjects);
    }
}
