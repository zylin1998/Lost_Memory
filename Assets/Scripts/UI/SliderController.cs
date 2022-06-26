using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [Header("Slider UI")]
    [SerializeField] private Text rateText;
    [SerializeField] private Slider slider;

    [Header("Background Image")]
    [SerializeField] private Image targetGraphic;
    [SerializeField] private Color normalColor = new Vector4(1, 1, 1, 0f);
    [SerializeField] private Color selectColor = new Vector4(0.6980f, 0.6980f, 0.6980f, 0.5882f);

    private Selection selection;

    private KeyManager manager;

    private bool canSelect = true;
    [SerializeField] private int direction = 0;

    public Text RateText => rateText;

    public Slider Slider => slider;

    private void Start()
    {
        manager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;

        selection = slider.GetComponent<Selection>();
    }

    private void Update()
    {
        targetGraphic.color = selection.Selected ? selectColor : normalColor;

        ChangingValue();
    }

    private void ChangingValue() 
    {
        direction = manager.ActionDirect.x + manager.SightDirect.x;

        if(!canSelect || !selection.Selected) { return; }

        slider.value += direction;

        StartCoroutine(Holding());
    }

    public IEnumerator Holding() 
    {
        canSelect = false;

        while (direction != 0) { yield return null; }

        canSelect = true;
    }
}
