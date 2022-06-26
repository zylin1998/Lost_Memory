using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] private string id;

    private Button button;
    private Text text;
    private Image image;

    private SavePage savePage;
    private ScrollSelection scrollSelection;

    public string ID => id;
    
    private void Start()
    {
        button = GetComponent<Button>();
        scrollSelection = GetComponent<ScrollSelection>();

        text = ObjectPool.GetStaff($"SaveBtn{id}", "UI") as Text;
        image = ObjectPool.GetStaff($"SaveImg{id}", "UI") as Image;
        savePage = ObjectPool.GetStaff("SavePage", "ScriptObject") as SavePage;

        button.onClick.AddListener(SetID);

        if (text != null) { text.text = $"Save{id}"; }
        if (savePage != null) { savePage.GetSaveButton(this); }
    }

    private void Update()
    {
        image.gameObject.SetActive(scrollSelection.Selected);

        Interactable();
    }

    private void SetID() 
    {
        savePage.Select = id;

        ObjectPool.GetStaff("SaveWarnUI", "UI").gameObject.SetActive(true);
    }

    public void Interactable() 
    {
        SaveFeild feild = savePage[id];

        if (feild == null) { return; }

        if (!feild.hasSaved && savePage.State == SaveLoadState.Load) { button.interactable = false; }

        if (savePage.State == SaveLoadState.Save) { button.interactable = true; }
    }
}
