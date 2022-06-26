using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum SaveLoadState
{
    Save = 0,
    Load = 1
}

#region Save Feild

[System.Serializable]
public class SaveFeild 
{
    [SerializeField] private UserData userData;
    private SaveButton saveButton;
    private string id;

    public UserData UserData { get => userData; set => userData = value; }
    public SaveButton SaveButton => saveButton;
    public string ID => id;
    public bool hasSaved => userData.hasSaved;

    public SaveFeild() { }

    public SaveFeild(UserData data, SaveButton button, string id) 
    {
        this.userData = data;
        this.saveButton = button;
        this.id = id;
    }
}

#endregion

public class SavePage : MonoBehaviour
{
    [Header("Title Text")]
    [SerializeField] private string titleTextName;
    [Header("Save Page Detail")]
    [SerializeField] private SaveLoadState state;
    [SerializeField] private SaveFeild[] saveFeilds;
    
    private string select;

    private Text waringText;
    private Text titleText;

    #region Reachable Properties

    public SaveLoadState State => state;

    public string Select { get => select; set => select = value; }

    public SaveFeild this[string id] => saveFeilds.Where(feild => feild.ID == id).First();
    public SaveFeild this[int id] => saveFeilds.Where(feild => System.Convert.ToInt32(feild.ID) == id).First();

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "SavePage", "ScriptObject");
        
        saveFeilds = new SaveFeild[10];
    }

    private void Start()
    {
        waringText = ObjectPool.GetStaff("SaveWarnText", "UI") as Text;
        titleText = ObjectPool.GetStaff(titleTextName, "UI") as Text;

        var warning = ObjectPool.GetStaff("SaveWarnUI", "UI") as WarnigUI;
        
        warning.uIConfirmedCallBack = new WarnigUI.OnUIInteract(ClickEvent);
        warning.uIActiveCallBack = new WarnigUI.OnUIInteract(SetWarnigText);

        if (titleText != null) { SetState((int)state); }
    }

    #endregion

    public void SetState(int value) 
    {
        state = (SaveLoadState)value;

        if(titleText == null) { return; }

        if(state == SaveLoadState.Save) { titleText.text = "Save"; }

        if(state == SaveLoadState.Load) { titleText.text = "Load"; }
    }

    #region Warnibg Text

    private void SetWarnigText() 
    {
        string path = Path.Combine(Application.dataPath, "SaveData", $"UserData{select}");
        
        if (state == SaveLoadState.Save) { waringText.text = File.Exists(path) ? "Replace Current Save?" : "Save At This Feild?"; ; }

        if (state == SaveLoadState.Load) { waringText.text = File.Exists(path) ? "Load Current Save?" : "Load As New Game?"; ; }
    }

    #endregion

    #region Click Event

    public void ClickEvent() 
    {
        if (state == SaveLoadState.Save) { SaveEvent(); }

        if (state == SaveLoadState.Load) { LoadEvent(); }
    }

    private void SaveEvent() 
    {
        var charaState = ObjectPool.GetStaff("CharacterState", "ScriptObject") as CharacterState;
        var eventObjects = ObjectPool.GetStaff("EventObjects", "ScriptObject") as EventObjects;
        var dialogueTrigger = ObjectPool.GetStaff("DialogueTrigger", "ScriptObject") as DialogueTrigger;

        this[select].UserData = StaticValue.userData;

        UserData data = StaticValue.userData;

        data.hasSaved = true;
        data.scene = SceneManager.GetActiveScene().name;
        data.player = new PlayerData(charaState.CurrentCharacter);
        data.eventGroups = eventObjects.GetCurrentEvents().eventGroup;
        data.readDialogue = dialogueTrigger.GetReadDialogue();

        string path = Path.Combine(Application.dataPath, "SaveData");

        SaveSystem.SaveData(StaticValue.userData, path, $"UserData{select}");
    }

    private void LoadEvent() 
    {
        string path = Path.Combine(Application.dataPath, "SaveData");

        var userData = SaveSystem.LoadData<UserData>(Path.Combine(path, $"UserData{select}"));
        
        if(userData == null) { return; }

        StaticValue.targetScene = userData.scene;
        StaticValue.userData.Reset(userData);

        Resources.Load<EventList>(Path.Combine("Events", "Event List")).eventGroup = userData.eventGroups;

        var loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        StartCoroutine(loadScenes.LoadCrossScene());
    }

    #endregion

    public void GetSaveButton(SaveButton button) 
    {
        select = button.ID;

        var id = System.Convert.ToInt32(button.ID);
        var path = Path.Combine(Application.dataPath, "SaveData", $"UserData{button.ID}");
        var data = SaveSystem.LoadData<UserData>(path);
        var saveLocateText = ObjectPool.GetStaff($"SaveLocate{select}", "UI") as Text;
        
        saveLocateText.text = data == null ? "Not Save" : data.scene;
        data = data == null ? new UserData() : data;

        saveFeilds[id] = new SaveFeild(data, button, button.ID);
    }
}
