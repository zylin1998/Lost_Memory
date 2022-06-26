using System.Collections;
using UnityEngine;

public class TestingDialogue : MonoBehaviour
{
    #region Parameter Field

    [Header("Dialogue Detail")]
    [SerializeField] private string dialogueID;

    [SerializeField] private bool changeScene;
    [SerializeField] private string sceneName;

    private DialogueTrigger dialogueTrigger;

    #endregion

    #region Script Behaviour

    void Start()
    {
        dialogueTrigger = ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger;

        Invoke("StartFirstDialogue", 0.05f);
    }
    #endregion

    #region Public Function

    public void StartFirstDialogue() 
    {
        dialogueTrigger.TriggerDialogue(dialogueID);

        if(!changeScene) { return; }

        StaticValue.targetScene = sceneName;

        LoadScenes loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        StartCoroutine(loadScenes.LoadAtDialogueEnd());
    }

    #endregion
}
