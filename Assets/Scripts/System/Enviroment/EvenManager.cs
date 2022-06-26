using System.Collections.Generic;
using UnityEngine;

public class EvenManager : MonoBehaviour
{
    #region Singleton

    public static EvenManager manager;

    private void Awake()
    {
        if (manager != null)
        {
            Debug.Log("More than one Event Manager fond."); ;
            return;
        }

        manager = this;
    }

    #endregion

    private LoadScenes loadScenes;
    private DialogueManager dialogueManager;

    private int jewelryCount = 0;
    private int letterID = 0;

    #region Reachable Properties

    public int JewelryCount
    {
        get => jewelryCount;

        set
        {
            jewelryCount = value;
            CheckJewelryCount();
        }
    }

    public int LetterID
    {
        get => letterID;

        set
        {
            letterID = value;
            CheckLetterID();
        }
    }

    #endregion

    private void Start()
    {
        loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        dialogueManager = ObjectPool.GetStaff("DialogueManager", "ScriptObject") as DialogueManager;
    }

    private void CheckJewelryCount() 
    {
        if (jewelryCount == 2) { GameObject.Find("閣樓入口").GetComponent<Animator>().SetBool("isOpen", true); }

        if (jewelryCount == 3) { GameObject.Find("地下室入口").GetComponent<Animator>().SetBool("isOpen", true); }
    }

    private void CheckLetterID() 
    {
        if (letterID != 5) { return; }

        StaticValue.targetScene = "Ended";

        StaticValue.userData.player.SetPosition(Vector3.zero);
        StaticValue.userData.player.SetRotation(Vector3.zero);

        dialogueManager.dialogueEndCallBack = new DialogueManager.DialogueEnd(loadScenes.LoadNewScene);
    }

    public void EndGame() 
    {
        Debug.Log("Ending Game!");

        StaticValue.targetScene = "Menu";

        loadScenes.LoadNewScene();
    }
}
