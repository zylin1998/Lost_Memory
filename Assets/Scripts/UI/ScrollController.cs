using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    #region Parameter Field

    [Header("Controller Name")]
    [SerializeField] private string controllerName;

    [Header("Visible Content Counts")]
    [SerializeField] private Vector2 startLocate;
    [SerializeField] private Vector2 viewCount;

    [Header("Scroll Speed")]
    [SerializeField] private float horizontalSpeed = 0.2f;
    [SerializeField] private float verticalSpeed = 0.2f;

    [Header("Content Scale Control")]
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 2.5f;
    [SerializeField] private float scaleSpeed;

    private ScrollRect scrollRect;

    private Vector2 first;
    private Vector2 final;

    #region Reachable Properties

    public Vector2 First => first;
    public Vector2 Final => final;

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake() => ObjectPool.Add(this, controllerName, "ScriptObject");

    private void Start() => Initialized();

    #endregion

    #region Public Function

    public void Initialized()
    {
        scrollRect = GetComponent<ScrollRect>();

        scrollRect.normalizedPosition = Vector2.one;

        first = startLocate;
        final = first + viewCount - Vector2.one;
    }

    public void Selected(Vector2 locate)
    {
        Vector2 delta = Vector2.zero;

        if (locate.x < first.x) { delta.x = locate.x - first.x; }

        if (locate.y < first.y) { delta.y = locate.y - first.y; }

        if (locate.x > final.x) { delta.x = locate.x - final.x; }

        if (locate.y > final.y) { delta.y = locate.y - final.y; }

        if(delta == Vector2.zero) { return; }

        ScrollRectPosition(delta);

        first.x += delta.x;
        final.x += delta.x;
        
        first.y += delta.y;
        final.y += delta.y;
    }

    public void ScrollRectPosition(Vector2 delta) 
    {
        float deltaX = horizontalSpeed * delta.x;
        float deltaY = verticalSpeed * delta.y;

        scrollRect.horizontalNormalizedPosition -= deltaX;
        scrollRect.verticalNormalizedPosition -= deltaY;
    }

    public void ContentScale(float delta) 
    {
        RectTransform content = scrollRect.content;

        float newScale = content.localScale.x + delta * scaleSpeed;

        if (newScale >= maxScale) { newScale = maxScale; }

        if (newScale <= minScale) { newScale = minScale; }

        content.localScale = new Vector3(newScale, newScale, 0);
    }

    #endregion
}