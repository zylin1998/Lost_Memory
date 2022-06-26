using UnityEngine;

public class WarnigUI : MonoBehaviour
{
    [SerializeField] private string uIName;

    [SerializeField] private string disSlcCtrName;
    [SerializeField] private string disPagCtrName;

    private PageController disPagCtr;
    private SelectionController disSlcCtr;

    private SelectionController warnSlcCtr;

    public delegate void OnUIInteract();

    public OnUIInteract uIConfirmedCallBack;
    public OnUIInteract uIActiveCallBack;

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, uIName, "UI");
    }

    private void OnEnable()
    {
        if (uIActiveCallBack != null) { uIActiveCallBack.Invoke(); }

        if (disSlcCtr != null) { disSlcCtr.Pause = true; }
        if (disPagCtr != null) { disPagCtr.Pause = true; }

        if (warnSlcCtr != null) { warnSlcCtr.Initialized(); }
    }

    private void Start()
    {
        if (disSlcCtrName != string.Empty) { disSlcCtr = ObjectPool.GetStaff(disSlcCtrName, "ScriptObject") as SelectionController; }
        if (disPagCtrName != string.Empty) { disPagCtr = ObjectPool.GetStaff(disPagCtrName, "ScriptObject") as PageController; }

        warnSlcCtr = GetComponent<SelectionController>();

        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (disSlcCtr != null) { disSlcCtr.Pause = false; }
        if (disPagCtr != null) { disPagCtr.Pause = false; }

        if (warnSlcCtr != null) { warnSlcCtr.DeSelected(); }
    }

    #endregion

    public void ConfirmButton() 
    {
        if (uIConfirmedCallBack != null) { uIConfirmedCallBack.Invoke(); }

        gameObject.SetActive(false);
    }

    public void CancelButton() 
    {
        gameObject.SetActive(false);
    }
}
