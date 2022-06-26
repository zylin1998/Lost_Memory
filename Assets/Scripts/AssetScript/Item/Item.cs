using UnityEngine;

#region Catogory Enum

[System.Serializable]
public enum Category
{
    Letter,
    Jewelry,
    Everything
}

#endregion

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    #region Parameter Field

    [Header("Item Details")]
    public string itemName = "Item";
    public Category category = Category.Everything;
    public Sprite icon = null;
    public string detail = "Detail";
    
    public bool isDefaultItem = false;

    #endregion

    #region Public Function

    public virtual void Used ()
    {
         Debug.Log(name + " is using.");
    }

    public virtual void PickUp() 
    {
        Debug.Log($"{name} picked up.");
    }

    #endregion
}
