using UnityEngine;

#region Jewelry Color Enum

public enum JewelryColor 
{
    Red,
    Blue,
    Green,
    None
}

#endregion

[CreateAssetMenu(fileName = "Jewelry", menuName = "Inventory/Jewelry"), System.Serializable]
public class Jewelry : Item
{
    #region Parameter Field

    [Header("Jewelry Color")]
    public JewelryColor jewelryColor = JewelryColor.None;

    #endregion

    #region Public Function

    public override void Used()
    {
        Debug.Log($"Jewelry {name} is used!");
    }

    public override void PickUp()
    {
        EvenManager.manager.JewelryCount++;

        int jewelryCount = EvenManager.manager.JewelryCount;

        if (jewelryCount <= 1) { return; }

        DialogueTrigger trigger = ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger;

        if (jewelryCount == 2) { trigger.TriggerDialogue("0011"); }

        if (jewelryCount == 3) { trigger.TriggerDialogue("0012"); }
    }

    #endregion
}
