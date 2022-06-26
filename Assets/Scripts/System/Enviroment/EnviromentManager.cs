using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Data Collection

[System.Serializable]
public class DataCollection 
{
    public DataCollection() 
    {
        text = new TextSetting();
        keyConfig = new KeyConfig();
        screen = new ScreenSetting();
        volume = new VolumeSetting();
        imageData = new MIFramesData();
        cameraSight = new CameraSightDelta();
    }

    public TextSetting text;
    public KeyConfig keyConfig;
    public ScreenSetting screen;
    public VolumeSetting volume;
    public MIFramesData imageData;
    public CameraSightDelta cameraSight;
}

#endregion

public class EnviromentManager : MonoBehaviour
{
    #region Singleton

    public static EnviromentManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Enviroment found!");
            return;
        }

        instance = this;

        itemPool = Resources.Load<ItemPool>(Path.Combine("Inventory", "Item Pool"));
        mIFrames = Resources.Load<MIFrames>(Path.Combine("System", "Memory Images"));
        windowSize = Resources.Load<WindowSize>(Path.Combine("System", "Window Size"));

        DataCollection data = SaveSystem.LoadData<DataCollection>(Path.Combine(Application.dataPath, "SaveData", "SystemData"));

        groupList = ObjectPool.Pool;

        bool hasData = data != null;

        this.data = hasData ? data : new DataCollection();

        if (hasData) { mIFrames.SetCGFramesData(data.imageData); }

        UserDataInitialize();
    }

    #endregion

    #region Parameter Field

    [Header("Data Collection")]
    [SerializeField] private DataCollection data;
    [SerializeField] private List<StaffGroup> groupList;
    [SerializeField] private UserData userData;

    private bool dialogueMode = false;
    private bool inventoryMode = false;
    private bool imageMode = false;

    private MIFrames mIFrames;
    private WindowSize windowSize;
    private ItemPool itemPool;

    #region Reachable Properties

    public bool DialogueMode { get => dialogueMode; set => dialogueMode = value;  }

    public bool InventoryMode { get => inventoryMode; set => inventoryMode = value; }

    public bool ImageMode { get => imageMode; set => imageMode = value; }

    public bool Pause => dialogueMode || inventoryMode || imageMode;

    public DataCollection Data => data;

    public MIFrames MIFrames => mIFrames;

    public ItemPool ItemPool => itemPool;

    #endregion

    #endregion

    #region Public Function

    #region Screen Setting

    private Text resolutionText;
    private Text fullScreenText;

    public void ChangeResolution(int value)
    {
        if (data.screen.select + value < 0) { return; }
        if (data.screen.select + value >= windowSize.Count) { return; }

        data.screen.select += value;

        SetResolution();
        ChangeResolution();
    }

    public void IsFullScreen(int value)
    {
        if (data.screen.screenState + value < 0) { return; }
        if (data.screen.screenState + value > 3) { return; }

        data.screen.screenState += value;

        SetResolution();
        IsFullScreen();
    }

    public void ChangeResolution() => resolutionText.text = windowSize[data.screen.select].GetResolution();

    public void IsFullScreen() => fullScreenText.text = ((FullScreenMode)data.screen.screenState).ToString();

    public void SetResolution()
    {
        int width = windowSize[data.screen.select].width;
        int height = windowSize[data.screen.select].height;
        int screenState = data.screen.screenState;

        Screen.SetResolution(width, height, (FullScreenMode)screenState);
    }

    public void WindowInitialize()
    {
        if (resolutionText == null) { resolutionText = ObjectPool.GetStaff("Resolution", "UI") as Text; }
        if (fullScreenText == null) { fullScreenText = ObjectPool.GetStaff("FullScreen", "UI") as Text; }

        ChangeResolution();
        IsFullScreen();
    }


    #endregion

    #region Volume Setting

    private SliderController mainVolume;
    private SliderController bgmVolume;
    private SliderController effectVolume;

    public void InitialVolumeRate()
    {
        mainVolume = ObjectPool.GetStaff("MainVolume", "ScriptObject") as SliderController;
        bgmVolume = ObjectPool.GetStaff("BGMVolume", "ScriptObject") as SliderController;
        effectVolume = ObjectPool.GetStaff("EffectVolume", "ScriptObject") as SliderController;

        mainVolume.Slider.value = data.volume.mainVolume;
        bgmVolume.Slider.value = data.volume.backgroundVolume;
        effectVolume.Slider.value = data.volume.effectVolume;

        mainVolume.RateText.text = $"{data.volume.mainVolume}";
        bgmVolume.RateText.text = $"{data.volume.backgroundVolume}";
        effectVolume.RateText.text = $"{data.volume.effectVolume}";
    }

    public void MainVolume(float value)
    {
        data.volume.mainVolume = System.Convert.ToInt32(value);

        mainVolume.RateText.text = $"{data.volume.mainVolume}";
    }

    public void BackGroundVolume(float value)
    {
        data.volume.backgroundVolume = System.Convert.ToInt32(value);

        bgmVolume.RateText.text = $"{data.volume.backgroundVolume}";
    }

    public void EffectVolume(float value)
    {
        data.volume.effectVolume = System.Convert.ToInt32(value);

        effectVolume.RateText.text = $"{data.volume.effectVolume}";
    }

    #endregion

    #region Sight Setting

    private SliderController horizSight;
    private SliderController vertiSight;

    public void InitialSightRate()
    {
        horizSight = ObjectPool.GetStaff("HorizSight", "ScriptObject") as SliderController;
        vertiSight = ObjectPool.GetStaff("VertiSight", "ScriptObject") as SliderController;

        horizSight.Slider.value = data.cameraSight.xDelta;
        vertiSight.Slider.value = data.cameraSight.yDelta;

        horizSight.RateText.text = $"{data.cameraSight.xDelta}";
        vertiSight.RateText.text = $"{data.cameraSight.yDelta}";
    }

    //Range Y: 1~5 X: 100~500
    public void HorizSight(float value)
    {
        data.cameraSight.xDelta = System.Convert.ToInt32(value);

        horizSight.RateText.text = $"{data.cameraSight.xDelta}";
    }

    public void VertiSight(float value)
    {
        data.cameraSight.yDelta = System.Convert.ToInt32(value);

        vertiSight.RateText.text = $"{data.cameraSight.yDelta}";
    }

    #endregion

    #region Text Setting

    private SliderController textSpeed;
    private SliderController autoSpeed;

    public void InitialTextRate()
    {
        textSpeed = ObjectPool.GetStaff("TextSpeed", "ScriptObject") as SliderController;
        autoSpeed = ObjectPool.GetStaff("AutoSpeed", "ScriptObject") as SliderController;

        textSpeed.Slider.value = data.text.textSpeed;
        autoSpeed.Slider.value = data.text.autoSpeed;

        textSpeed.RateText.text = $"{data.text.textSpeed}";
        autoSpeed.RateText.text = $"{data.text.autoSpeed}";
    }

    public void TextSpeed(float value)
    {
        data.text.textSpeed = System.Convert.ToInt32(value);

        textSpeed.RateText.text = $"{data.text.textSpeed}";
    }

    public void AutoSpeed(float value)
    {
        data.text.autoSpeed = System.Convert.ToInt32(value);

        autoSpeed.RateText.text = $"{data.text.autoSpeed}";
    }

    #endregion

    #region Data Saved

    public void DataSaved() => SaveSystem.SaveData(data, Path.Combine(Application.dataPath, "SaveData"), "SystemData");

    #endregion

    #endregion

    private void UserDataInitialize() 
    {
        if (StaticValue.userData == null) { StaticValue.userData = new UserData(); }

        userData = StaticValue.userData;

        if (userData.items == null) { userData.items = itemPool.InitialItemData; }

        if (userData.eventGroups == null) 
        {
            EventList defaultList = Resources.Load<EventList>(Path.Combine("Events", "Default Events"));

            userData.eventGroups = defaultList.eventGroup; 
        }
    }
}
