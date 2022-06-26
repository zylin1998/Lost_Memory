[System.Serializable]
public class CameraSightDelta
{
    #region Parameter Field

    public int yDelta;
    public int xDelta;

    #endregion

    #region Construction

    public CameraSightDelta()
    {
        yDelta = 10;
        xDelta = 10;
    }

    public CameraSightDelta(CameraSightDelta delta)
    {
        yDelta = delta.yDelta;
        xDelta = delta.xDelta;
    }

    #endregion

    #region Public Function

    public void Clear() 
    {
        yDelta = 10;
        xDelta = 10;
    }

    #endregion
}