[System.Serializable]
public class VolumeSetting
{
    #region Parameter Field

    public int mainVolume;
    public int backgroundVolume;
    public int effectVolume;

    #endregion

    #region Construction

    public VolumeSetting()
    {
        mainVolume = 50;
        backgroundVolume = 50;
        effectVolume = 50;
    }

    public VolumeSetting(int main, int back, int effect)
    {
        mainVolume = main;
        backgroundVolume = back;
        effectVolume = effect;
    }

    #endregion

    #region Public Function

    public void Clear() 
    {
        mainVolume = 50;
        backgroundVolume = 50;
        effectVolume = 50;
    }

    #endregion
}