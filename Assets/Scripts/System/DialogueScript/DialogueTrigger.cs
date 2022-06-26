using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    #region Parameter Field

    private string fileName;

    private DialogueManager dialogueManager;

    private List<string> readDialogue;

    #region Reachable Properties

    public string FileName => fileName;

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake() => ObjectPool.Add(this, "DialogueTrigger", "ScriptObject");

    private void Start()
    {
        dialogueManager = ObjectPool.GetStaff("DialogueManager", "ScriptObject") as DialogueManager;

        ReadDialogueInitialize();
    }

    #endregion

    #region Public Function

    public void TriggerDialogue(string id) 
    {
        if(IsDialogueRead(id)) { return; }

        fileName = "D" + id;

        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "DialogueData", fileName);
        DialogueData data = SaveSystem.LoadData<DialogueData>(path);

        if (data != null)
        {
            dialogueManager.SetDialogue(data);
            dialogueManager.StartDialogue();

            SetReadDialogue(id);
        }
    }

    public string[] GetReadDialogue()
    {
        return readDialogue.ToArray();
    }

    #endregion

    private void ReadDialogueInitialize() 
    {
        readDialogue = StaticValue.userData.readDialogue != null ? new List<string>(StaticValue.userData.readDialogue) : new List<string>();
    }

    private bool IsDialogueRead(string id) 
    {
        bool isRead = false;

        if(readDialogue.Count <= 0) { return false; }

        foreach (string read in readDialogue)
        {
            if (id != read) { continue; }

            isRead = true;
            break;
        }

        return isRead;
    }

    private void SetReadDialogue(string id) 
    {
        readDialogue.Add(id);

        StaticValue.userData.readDialogue = readDialogue.ToArray();
    }
}
