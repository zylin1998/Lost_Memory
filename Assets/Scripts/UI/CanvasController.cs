using UnityEngine;

public class CanvasController : MonoBehaviour
{
    #region Parameter Field

    [Header("Main UI")]
    public GameObject inventoryUI;
    public GameObject dialogueUI;
    public GameObject imformationUI;

    private KeyManager manager;

    #endregion

    #region Script Behaviour

    private void Awake() => ObjectPool.Add(this, "CanvasController", "ScriptObject");

    private void Start()
    {
        manager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;
    }

    private void Update()
    {
        if (EnviromentManager.instance.ImageMode) { return; }

        InventoryPage();

        if (imformationUI != null) { imformationUI.SetActive(!InformationActive()); }
    }

    #endregion


    #region Inventory Page

    private PageController inventory;

    private void InventoryPage() 
    {
        bool isEscape = manager.EscapeState;
        bool isInventory = manager.InventoryState;

        EnviromentManager.instance.InventoryMode = inventoryUI.activeSelf;

        if (!isEscape && !isInventory) { return; }

        if (!inventoryUI.activeSelf) 
        { 
            inventoryUI.SetActive(true);
            if (inventory == null) { inventory = inventoryUI.GetComponent<PageController>(); }
            return;
        }

        if (inventoryUI.activeSelf) { inventory.ClosePage(); }
    }

    #endregion

    #region Dialogue Page

    public void DialoguePage(bool state) 
    {
        dialogueUI.SetActive(state);
    }

    #endregion

    #region InformationUI

    private bool InformationActive() => inventoryUI.activeSelf || dialogueUI.activeSelf;

    #endregion
}
