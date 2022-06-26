[System.Serializable]
public class ScreenSetting
{
    #region Parameter Field

    public int screenState;

    public int select;

    #endregion

    #region Construction

    public ScreenSetting() 
    {
        screenState = 0;
        select = 0;
    }

    public ScreenSetting(int state, int select)
    {
        screenState = state;
;
        this.select = select;
    }

    #endregion

    #region Public Function

    public void Clear() 
    {
        screenState = 0;
        select = 0;
    }

    #endregion
}