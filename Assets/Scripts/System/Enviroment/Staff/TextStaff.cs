using UnityEngine;
using UnityEngine.UI;

public class TextStaff : MonoBehaviour
{
    [SerializeField] string staffName;

    public Text Target => GetComponent<Text>();

    private void Awake() => ObjectPool.Add(Target, staffName, "UI");
}
