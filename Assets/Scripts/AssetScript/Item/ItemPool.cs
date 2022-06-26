using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemState 
{
    public GameObject item;
    public bool picked;
    public int count;

    public string[] appearScene;

    #region Reachable Properties

    public Item Item => item.GetComponentInChildren<ItemPickup>().Item;

    public string ItemName => this.Item.itemName;
    
    public ItemData.UsedData Used => new ItemData.UsedData(ItemName);

    #endregion

    public bool CheckScene(string currentScene) 
    {
        bool check = false;

        if (appearScene.Length <= 0) { return false; }

        foreach (string scene in appearScene) 
        {
            if (currentScene == scene) { check = true; }
        }

        return check;
    }
}

[CreateAssetMenu(fileName = "Item Pool", menuName = "Inventory/Item Pool")]
public class ItemPool : ScriptableObject
{
    public List<ItemState> itemList = new List<ItemState>();

    #region Reachable Properties

    public ItemState this[string itemName] => itemList.Where(item => item.Item.itemName == itemName).First();

    public ItemData InitialItemData => new ItemData(itemList);

    #endregion

    public void SetPickedItem(ItemData list) 
    {
        foreach (ItemData.UsedData data in list.used)
        {
            ItemState itemState = this[data.itemName];

            if (itemState != null)
            {
                itemState.picked = data.picked;
                itemState.count = data.count;
            }
        }
    }
}
