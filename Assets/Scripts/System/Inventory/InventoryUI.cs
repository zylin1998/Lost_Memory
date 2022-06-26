using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region Parameter Field

    [Header("Game Components")]
    public Transform ballParent;
    public Transform letterParent;

    [Header("Item Detail Text")]
    public Text jewelryDetail;
    public Text letterDetail;

    [Header("Script Components")]
    [SerializeField] private JewelrySlot[] jewelrySlots;
    [SerializeField] private LetterSlot[] letterSlots;

    Inventory inventory;

    #endregion

    #region Script Behaviour

    private void Start()
    {
        inventory = Inventory.instance;

        inventory.OnItemChangedCallback += UpdateUI;

        jewelrySlots = ballParent.GetComponentsInChildren<JewelrySlot>();
        letterSlots = letterParent.GetComponentsInChildren<LetterSlot>();

        if(inventory.itemList.Count == 0) { return; }

        UpdateUI();
    }

    #endregion

    #region Private Function

    private void UpdateUI()
    {
        foreach(Item item in inventory.itemList)
        {
            if(item.category == Category.Jewelry) { Add(item as Jewelry); continue; }

            if(item.category == Category.Letter) { Add(item as Letter); continue; }
        }
    }

    #endregion

    #region Public Function

    public void Add(Jewelry jewelry) 
    {
        if(jewelry == null) { return; }

        foreach (JewelrySlot slot in jewelrySlots) 
        { 
            if(jewelry.jewelryColor != slot.jewelryColor) { continue; }

            slot.AddItem(jewelry);
            slot.CheckSlot();
        }
    }

    public void Add(Letter letter)
    {
        if (letter == null) { return; }

        foreach (LetterSlot slot in letterSlots)
        {
            if (letter.id != slot.id) { continue; }

            slot.AddItem(letter);
            slot.CheckSlot();
        }
    }

    public void DisplayJewel(JewelrySlot slot)
    {
        jewelryDetail.text = slot.item.detail;
    }

    public void DisplayLetter(LetterSlot slot)
    {
        letterDetail.text = slot.item == null ? "©|¥¼¨ú±o" : slot.item.detail;
    }

    #endregion
}
