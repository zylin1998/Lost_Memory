using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region DirectType Enum

[System.Serializable, Flags]
public enum DirectType 
{
    Horizontal = 1,
    Vertical = 2,
    Everything = 4,
}

#endregion

public class SelectionController : MonoBehaviour
{
    #region Parameter Field

    [Header("Controller Name")]
    [SerializeField] private string controllerName;

    [Header("Selectable UI")]
    [SerializeField] private Selectable[] initialSelect;

    [Header("Selectable States")]
    [SerializeField] public bool startSelect = false;

    private bool pause = false;
    private bool canSelect = true;

    private Vector2Int direction;
    
    private Selectable nowSelect;
    private Selectable lastSelect;
    
    [SerializeField] private KeyManager manager;
    [SerializeField] private EventSystem eventSystem;

    private float waitTime = 1.0f;
    private float moveTime = 0.15f;

    #region Reachable Properties
    
    public bool CanSelect => canSelect;

    public Selectable NowSelect => nowSelect;
    public Selectable LastSelect => lastSelect;

    public float WaitTime => waitTime;
    public float MoveTime => moveTime;

    public bool Pause { get => pause; set => pause = value; }

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, controllerName, "ScriptObject");
    }

    private void Start()
    {
        CheckState();
    }

    private void Update()
    {
        if (!pause) { OnSelected(); }
    }

    #endregion

    public void Initialized()
    {
        if(startSelect) { Invoke("StartSelect", 0.05f); }
    }

    #region Interactable Function

    public void DeSelected()
    {
        nowSelect = null;
        lastSelect = null;
        if (eventSystem != null) { eventSystem.SetSelectedGameObject(null); }
    }

    public void PointEnter(Selectable selectable)
    {
        eventSystem.SetSelectedGameObject(null);

        nowSelect = selectable;
        nowSelect.Select();
    }

    public void PointExit()
    {
        lastSelect = nowSelect;
        nowSelect = null;
        eventSystem.SetSelectedGameObject(null);
    }

    public void Selected(Selectable selectable)
    {
        nowSelect = selectable;
    }

    #endregion

    #region Private Function

    private void OnSelected()
    {
        if (!IsPressed()) { return; }

        if (canSelect) { StartSelect(); }

        if (canSelect) { Selecting(); }
    }

    private bool IsPressed()
    {
        direction = manager.ActionDirect + manager.SightDirect;

        if(canSelect) { pressTime = Time.time; }

        bool isPressed = (direction != Vector2Int.zero);

        return isPressed;
    }

    private void StartSelect() 
    {
        if (nowSelect != null) { return; }

        if (lastSelect != null)  { lastSelect.Select(); }

        foreach (Selectable selectable in initialSelect)
        {
            if(lastSelect != null) { break; }

            if (selectable.interactable) { selectable.Select(); break; }
        }

        canSelect = false;

        StartCoroutine(Holding());
    }

    private void Selecting() 
    {
        int horizontal = direction.x;
        int veritcal = direction.y;

        Selectable selectable = null;

        if (veritcal == 1) { selectable = nowSelect.FindSelectableOnUp(); }

        if (veritcal == -1) { selectable = nowSelect.FindSelectableOnDown(); }

        if (horizontal == 1) { selectable = nowSelect.FindSelectableOnRight(); }

        if (horizontal == -1) { selectable = nowSelect.FindSelectableOnLeft(); }

        if (selectable != null) { selectable.Select(); }

        canSelect = false;

        StartCoroutine(Holding());
    }

    private void CheckState() 
    {
        manager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;

        eventSystem = FindObjectOfType<EventSystem>();
    }

    private float pressTime;

    public IEnumerator Holding() 
    {
        float passTime;
        bool keepMoving = false;

        Selectable selectable = null;

        while(true)
        {
            if (direction == Vector2Int.zero) { break; }

            passTime = Time.time - pressTime;
            keepMoving = passTime >= waitTime;

            if (keepMoving) 
            {
                if (direction.y == 1) { selectable = nowSelect.FindSelectableOnUp(); }

                if (direction.y == -1) { selectable = nowSelect.FindSelectableOnDown(); }

                if (direction.x == 1) { selectable = nowSelect.FindSelectableOnRight(); }

                if (direction.x == -1) { selectable = nowSelect.FindSelectableOnLeft(); }
            }

            if(selectable != null) { selectable.Select(); }

            yield return keepMoving ? new WaitForSeconds(moveTime) : null;
        }

        canSelect = true;
    }

    #endregion
}
