using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    #region Parameter Field

    [Header("Game Components")]
    public Button slotButton;
    public Image icon;

    public Category category = Category.Everything;
    public Item item;

    #endregion

    #region Public Function

    public virtual void AddItem(Item item)
    {
        this.item = item;

        icon.sprite = item.icon;
        icon.preserveAspect = true;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void CheckSlot()
    {
        if(item == null) { slotButton.interactable = false; }

        else { slotButton.interactable = true; }
    }

    #endregion
}
