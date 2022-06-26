using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PageController : MonoBehaviour
{
    #region Parameter Field

    [Header("Controller Name")]
    [SerializeField] private string controllerName = string.Empty;

    [Header("Title Text")]
    [SerializeField] private string titleTextName;
    [SerializeField] private string title;
    
    [Header("Child Pages")]
    [SerializeField] private GameObject initialPage;
    [SerializeField] private GameObject[] children;
    
    [Header("Selection Controller")]
    [SerializeField] private string slcCtrName;

    [Header("Page Initializing Event")]
    [SerializeField] private UnityEvent initialEvent;
    [SerializeField] private UnityEvent exitEvent;
    
    private SelectionController selectionController;
    
    private Text titleText;
    
    private bool isChildOpened = false;

    private bool isStart = false;

    private bool pause = false;

    #region Reachable Properties

    public bool IsChildOpened => isChildOpened;

    public bool Pause { get => pause; set => pause = value; }

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        if(controllerName != string.Empty) { ObjectPool.Add(this, controllerName, "ScriptObject"); }
    }

    private void Start() 
    {
        if (titleTextName != string.Empty) { titleText = ObjectPool.GetStaff(titleTextName, "UI") as Text; }

        selectionController = ObjectPool.GetStaff(slcCtrName, "ScriptObject") as SelectionController;

        initialEvent.AddListener(selectionController.Initialized);
        initialEvent.AddListener(OpenPage);

        initialEvent.Invoke();

        OpenPage();

        isStart = true;
    }

    private void OnEnable() 
    {
        if (isStart) { initialEvent.Invoke(); }
    }

    private void OnDisable()
    {
        exitEvent.Invoke();

        if (!isChildOpened) { selectionController.DeSelected(); }

        if (isChildOpened) { selectionController.PointExit(); }
    }

    private void Update()
    {
        if (!pause) { PageState(); }
    }

    #endregion

    #region Private Function

    private void PageState() 
    {
        if (isChildOpened) { return; }

        if(!IsEscape() && !IsMouse1()) { return; }

        selectionController.DeSelected();

        ClosePage();
    }

    #region Key Checking

    private bool IsEscape() => Input.GetKeyDown(KeyCode.Escape);

    private bool IsMouse1() => Input.GetKeyDown(KeyCode.Mouse1);

    #endregion

    #region Page Operating IEnumerator

    protected IEnumerator PageState(GameObject page, bool state) 
    {
        if (state) { page.SetActive(true); }

        int start = state ? 0 : 10;
        int end = state ? 10 : 0;
        int delta = state ? 1 : -1;

        CanvasGroup canvasGroup = page.GetComponent<CanvasGroup>();

        for (int i = start; i != end; i += delta)
        {
            canvasGroup.alpha = i * 0.1f;

            yield return new WaitForSeconds(0.025f);
        }

        canvasGroup.alpha = end * 0.1f;
        if (!state) { page.SetActive(false); }
    }

    public IEnumerator ChildPage(int page) 
    {
        selectionController.PointExit();

        yield return PageState(initialPage, false);
        children[page].SetActive(true);

        isChildOpened = true;
    }

    #endregion

    #endregion

    #region Public Function

    public virtual void OpenChildPage(int page)
    {
        StartCoroutine(ChildPage(page));
    }

    public virtual void OpenInitialPage() 
    {
        if (titleText != null) { titleText.text = title; }

        StartCoroutine(PageState(initialPage, true));

        isChildOpened = false;
    }

    public virtual void CloseInitialPage() 
    {
        StartCoroutine(PageState(initialPage, false));
    }

    public virtual void OpenPage() 
    {
        if (titleText != null) { titleText.text = title; }

        StartCoroutine(PageState(gameObject, true));
    }

    public virtual void ClosePage()
    {
        if(isChildOpened) { return; }

        StartCoroutine(PageState(gameObject, false));
    }

    #endregion
}