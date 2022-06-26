using UnityEngine;

public class SliderCtrStaff : MonoBehaviour
{
    [SerializeField] string staffName;

    private SliderController Target => GetComponent<SliderController>();

    private void Awake()
    {
        ObjectPool.Add(Target, staffName, "ScriptObject");
    }
}
