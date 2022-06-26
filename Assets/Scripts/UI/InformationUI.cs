using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
    [Header("Event Hint")]
    [SerializeField] private GameObject eventHint;
    [SerializeField] private Text eventType;
    [SerializeField] private Text eventKeyCode;

    #region Reachable Properties

    public GameObject EventHint => eventHint;
    public Text EventType => eventType;
    public Text EventKeyCode => eventKeyCode;

    #endregion

    private void Awake() => ObjectPool.Add(this, "InformationUI", "ScriptObject");
}
