using System.Linq;
using System.Collections.Generic;
using UnityEngine;

#region Player Data

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public float[] rotation;

    public PlayerData()
    {
        position = new float[] { 14, 1, -15 };
        rotation = new float[] { 0, 0, 0 };
    }

    public PlayerData(Transform transform)
    {
        position = new float[] { transform.position.x, transform.position.y, transform.position.z };
        rotation = new float[] { transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z };
    }

    public Vector3 Position => new Vector3(position[0], position[1], position[2]);

    public Quaternion Rotation => Quaternion.Euler(rotation[0], rotation[1], rotation[2]);

    public void SetPosition(Vector3 value) 
    {
        position = new float[] { value.x, value.y, value.z };
    }

    public void SetRotation(Vector3 value)
    {
        rotation = new float[] { value.x, value.y, value.z };
    }
}

#endregion

#region Item Data

[System.Serializable]
public class ItemData
{ 
    #region Used Item Data

    [System.Serializable]
    public class UsedData 
    {
        public string itemName;

        public bool picked;

        public int count;

        public UsedData() 
        {
            itemName = string.Empty;

            picked = false;

            count = 0;
        }

        public UsedData(string name) 
        {
            itemName = name;

            picked = false;

            count = 0;
        }

        public UsedData(string name, bool pick, int num) 
        {
            itemName = name;

            picked = pick;

            count = num;
        }
    }

    #endregion

    public UsedData[] used;

    public ItemData() { }

    public ItemData(UsedData[] data) { used = data; }

    public ItemData(List<ItemState> items) { SetByList(items); }

    public UsedData this[string itemName] 
    {
        get 
        {
            foreach (UsedData data in used) 
            {
                if (data.itemName == itemName) 
                { 
                    return data; 
                }
            }

            return null;
        }
    }

    public void SetByList(List<ItemState> items) 
    {
        used = new UsedData[items.Count];

        for(int i = 0; i < used.Length; i++) { used[i] = items[i].Used; }
    }
}

#endregion

#region Dialogue State

[System.Serializable]
public class DialogueState 
{
    public bool dialogueMode;
    public string currentDialogue;
    public string currentLocate;
    public string[] readDialogue;

    public DialogueState() { }

    public DialogueState(DialogueState state) 
    {
        this.dialogueMode = state.dialogueMode;
        this.currentDialogue = state.currentDialogue;
        this.currentLocate = state.currentLocate;
        this.readDialogue = state.readDialogue;
    }

    #region Set Read Dialogue

    public void SetReadDialogue(string[] list) 
    {
        readDialogue = list;
    }

    public void SetReadDialogue(List<string> list) 
    {
        readDialogue = list.ToArray();
    }

    #endregion
}

#endregion

[System.Serializable]
public class UserData
{
    public bool hasSaved;

    public string scene;

    public PlayerData player;

    public ItemData items;

    public EventList.SceneEvents[] eventGroups;

    public string[] readDialogue;

    #region Construction

    public UserData() 
    {
        this.hasSaved = false;
        this.scene = string.Empty;
        this.player = new PlayerData();
        this.items = null;
        this.eventGroups = null;
        this.readDialogue = null;
    }

    public UserData(string scene, Transform player, List<ItemState> itemList, EventList.SceneEvents[] groups, string[] read)
    {
        this.hasSaved = true;
        this.scene = scene;
        this.player = player == null ? new PlayerData() : new PlayerData(player);
        this.items = new ItemData(itemList);
        this.eventGroups = groups;
        this.readDialogue = read;
    }

    #endregion

    public void Reset(string scene, Transform player, List<ItemState> itemList, EventList.SceneEvents[] groups, string[] read)
    {
        this.hasSaved = true;
        this.scene = scene;
        this.player = player == null ? new PlayerData() : new PlayerData(player);
        this.items = new ItemData(itemList);
        this.eventGroups = groups;
        this.readDialogue = read;
    }

    public void Reset(UserData data) 
    {
        this.hasSaved = data.hasSaved;
        this.scene = data.scene;
        this.player = data.player;
        this.items = data.items;
        this.eventGroups = data.eventGroups;
        this.readDialogue = data.readDialogue;
    }
}
