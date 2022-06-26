using UnityEngine;
using UnityEngine.UI;

public class LetterSlot : InventorySlot
{
    #region Parameter Field

    [Header("Game Components")]
    public Text text;

    [Header("Letter Details")]
    public int id = 0;

    #endregion

    #region Public Function

    public override void AddItem(Item item)
    {
        base.AddItem(item);

        text.text = item.itemName;
    }

    #endregion
}
