using UnityEngine;
using UnityEngine.UI;

public class ImageStaff : MonoBehaviour
{
    [SerializeField] private string staffName;
    [SerializeField] private bool visible;

    public Image Target => GetComponent<Image>();

    private void Awake()
    {
        ObjectPool.Add(Target, staffName, "UI");
    }

    private void Start()
    {
        gameObject.SetActive(visible);
    }
}
