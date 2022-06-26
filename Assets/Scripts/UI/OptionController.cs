using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [Header("Selectable Image")]
    [SerializeField] private Selection selection;

    [Header("Option Buttons")]
    [SerializeField] private Toggle upButton;
    [SerializeField] private Toggle downButton;
    [SerializeField] private Toggle leftButton;
    [SerializeField] private Toggle rightButton;

    [Header("Function Text")]
    [SerializeField] private Text functionText;

    private Vector2Int direction;

    private bool btnState = false;
    private bool canClick = false;

    private KeyManager manager;

    public Text FunctionText => functionText;

    #region Script Behaviour

    private void Start()
    {
        manager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;
    }

    private void Update()
    {
        if (selection.Selected != btnState) { ButtonState(); }

        if (!selection.Selected) { return; }

        direction = manager.ActionDirect + manager.SightDirect;

        if (direction == Vector2Int.zero) { canClick = true; }

        if (canClick) { Input(); }
    }

    #endregion

    private void Input() 
    {
        Toggle toggle = null;

        if (direction.y == 1) { toggle = upButton; }

        if (direction.y == -1) { toggle = downButton; }

        if (direction.x == 1) { toggle = rightButton; }

        if (direction.x == -1) { toggle = leftButton; }

        if(toggle == null) { return; }

        canClick = false;

        toggle.onValueChanged.Invoke(true);
    }

    public void ButtonState()
    {
        btnState = !btnState;

        canClick = btnState;

        if (upButton != null) { upButton.gameObject.SetActive(btnState); }

        if (downButton != null) { downButton.gameObject.SetActive(btnState); }

        if (leftButton != null) { leftButton.gameObject.SetActive(btnState); }

        if (rightButton != null) { rightButton.gameObject.SetActive(btnState); }
    }
}
