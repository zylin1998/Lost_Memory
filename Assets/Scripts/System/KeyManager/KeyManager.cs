using UnityEngine;

public class KeyManager : MonoBehaviour
{
    #region Parameter Field

    [Header("Custom Key Input")]
    [SerializeField] private KeyConfig keyConfig;
    
    [Header("Direction Invert")]
    [SerializeField] private bool yAxisInvert = true;
    [SerializeField] private bool xAxisInvert = false;

    private bool hasKeyConfig = false;
    
    private Vector2Int actionDirect = new Vector2Int(0, 0);
    private Vector2Int sightDirect = new Vector2Int(0, 0);

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "KeyManager", "ScriptObject");
    }

    private void Start()
    {
        keyConfig = EnviromentManager.instance.Data.keyConfig;

        hasKeyConfig = keyConfig != null;
        
        Initialized();
    }

    private void Update()
    {
        if(!hasKeyConfig) { return; }

        ActionDirectionInput();
        SightDirectionInput();
    }

    #endregion

    #region Key System Initialized

    public void Initialized()
    {
        if(!hasKeyConfig) { return; }

        if (keyConfig.keyFrames.Length < keyConfig.maxKeyCounts) { ResetKeys(); }
    }

    #endregion

    #region Key Parameters Reset

    public void ResetKeys()
    {
        keyConfig.DefautConfig();
    }

    #endregion

    #region Direction Confirm

    private void ActionDirectionInput()
    {
        if (actionDirect.y == 0)
        {
            if (Input.GetKeyDown(this[KeyState.Forward])) { actionDirect.y = 1; }
            if (Input.GetKeyDown(this[KeyState.Back])) { actionDirect.y = -1; }
        }

        if (actionDirect.x == 0)
        {
            if (Input.GetKeyDown(this[KeyState.Right])) { actionDirect.x = 1; }
            if (Input.GetKeyDown(this[KeyState.Left])) { actionDirect.x = -1; }
        }

        if (Input.GetKeyUp(this[KeyState.Forward]) && actionDirect.y == 1) { actionDirect.y = 0; }
        if (Input.GetKeyUp(this[KeyState.Back]) && actionDirect.y == -1) { actionDirect.y = 0; }

        if (Input.GetKeyUp(this[KeyState.Right]) && actionDirect.x == 1) { actionDirect.x = 0; }
        if (Input.GetKeyUp(this[KeyState.Left]) && actionDirect.x == -1) { actionDirect.x = 0; }
    }

    private void SightDirectionInput()
    {
        if (sightDirect.y == 0)
        {
            if (Input.GetKeyDown(this[KeyState.YAxisUp])) { sightDirect.y = 1; }
            if (Input.GetKeyDown(this[KeyState.YAxisDown])) { sightDirect.y = -1; }
        }

        if (sightDirect.x == 0)
        {
            if (Input.GetKeyDown(this[KeyState.XAxisRight])) { sightDirect.x = 1; }
            if (Input.GetKeyDown(this[KeyState.XAxisLeft])) { sightDirect.x = -1; }
        }

        if (Input.GetKeyUp(this[KeyState.YAxisUp]) && sightDirect.y == 1) { sightDirect.y = 0; }
        if (Input.GetKeyUp(this[KeyState.YAxisDown]) && sightDirect.y == -1) { sightDirect.y = 0; }

        if (Input.GetKeyUp(this[KeyState.XAxisRight]) && sightDirect.x == 1) { sightDirect.x = 0; }
        if (Input.GetKeyUp(this[KeyState.XAxisLeft]) && sightDirect.x == -1) { sightDirect.x = 0; }
    }

    #endregion

    #region Property

    #region Key Setting

    public KeyCode this[int state] => keyConfig[state].keyCode;

    public KeyCode this[KeyState state] => keyConfig[state].keyCode;

    public KeyCode this[string state] => keyConfig[state].keyCode;

    #endregion

    #region State Setting

    public Vector2Int ActionDirect => actionDirect;

    public Vector2Int SightDirect => sightDirect;

    public int Vertical => actionDirect.y;

    public int Horizontal => actionDirect.x;

    public int YAxis => sightDirect.y * (yAxisInvert ? -1 : 1);

    public int XAxis => sightDirect.x * (xAxisInvert ? -1 : 1);

    public bool JumpState => Input.GetKeyDown(this[KeyState.Jump]);

    public bool SprintState => Input.GetKey(this[KeyState.Sprint]);

    public bool EventState => Input.GetKeyDown(this[KeyState.Event]);

    public bool InventoryState => Input.GetKeyDown(this[KeyState.Inventory]);

    public bool AttackState => Input.GetKeyDown(this[KeyState.Attack]);

    public bool EscapeState => Input.GetKeyDown(this[KeyState.Escape]);

    public bool TabState => Input.GetKeyDown(this[KeyState.Tab]);

    #endregion

    #endregion
}