using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    #region Parameter Field

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<Item> itemList = new List<Item>();

    private ItemPool itemPool;

    #endregion

    private void Start()
    {
        itemPool = EnviromentManager.instance.ItemPool;

        Initialized();
    }

    private void Initialized()
    {
        GameObject temp = GameObject.Find("Items");

        if (temp == null) { return; }

        var parent = temp.transform;
        var currentScene = SceneManager.GetActiveScene().name;

        foreach(ItemState itemState in itemPool.itemList) 
        {
            ItemData.UsedData data = StaticValue.userData.items[itemState.ItemName];

            if (data.picked) { instance.Store(itemState.Item); }

            if (!itemState.CheckScene(currentScene)) { continue; }

            if (!data.picked) { Instantiate(itemState.item, itemState.item.transform.position, Quaternion.identity, parent); }
        }
    }

    #region Public Function

    public bool Store(Item item) 
    {
        if (item.isDefaultItem) { return false; }

        itemList.Add(item);

        if (OnItemChangedCallback != null) { OnItemChangedCallback.Invoke(); }

        return true;
    }

    public bool Add(Item item) 
    {
        if(item.isDefaultItem) { return false; } 
        
        ItemData.UsedData data = StaticValue.userData.items[item.itemName];

        data.picked = true;
        data.count += 1;

        return Store(item);
    }

    public void Remove(Item item)
    {
        itemList.Remove(item);

        if (OnItemChangedCallback != null) { OnItemChangedCallback.Invoke(); }
    }

    #endregion
}
