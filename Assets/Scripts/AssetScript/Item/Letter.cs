using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "Inventory/Letter"), System.Serializable]
public class Letter : Item
{
    #region Parameter Field

    public int id = 0;

    [SerializeField] protected bool hasDialogue = false;
    [SerializeField] protected string dialogueID;

    public override void Used()
    {
        Debug.Log($"Letter {name} is used!");
    }

    public override void PickUp()
    {
        if(hasDialogue) { (ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger).TriggerDialogue(dialogueID); }

        EvenManager.manager.LetterID = id;
    }

    #endregion
}
