[System.Serializable]
public class TextSetting
{
    #region Parameter Field

    public int textSpeed;
    public int autoSpeed;
    public bool skipOption;

    #endregion

    #region Construction

    public TextSetting()
    {
        textSpeed = 0;
        autoSpeed = 0;
        skipOption = false;
    }

    public TextSetting(TextSetting setting)
    {
        textSpeed = setting.textSpeed;
        autoSpeed = setting.autoSpeed;
        skipOption = setting.skipOption;
    }

    #endregion

    #region Public Function

    public void Clear() 
    {
        textSpeed = 0;
        autoSpeed = 0;
        skipOption = false;
    }

    #endregion
}