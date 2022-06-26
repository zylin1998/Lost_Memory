using UnityEngine;
using UnityEngine.UI;

public class InformationText : MonoBehaviour
{
    [Header("Information Text")]
    [SerializeField] private Text informationText;

    public void Initialized()
    {
        informationText.text = string.Empty;
    }
}
