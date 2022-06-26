using System.Linq;
using UnityEngine;

#region Key Types Enum

public enum KeyState
{
    Forward = 0,
    Back = 1,
    Left = 2,
    Right = 3,
    XAxisLeft = 4,
    XAxisRight = 5,
    YAxisUp = 6,
    YAxisDown = 7,
    Sprint = 8,
    Jump = 9,
    Attack = 10,
    Event = 11,
    Inventory = 12,
    Escape = 13,
    Tab = 14,
    None
}

#endregion

#region Key Frame

[System.Serializable]
public class KeyFrame
{
    #region Parameter Field

    public KeyState keyState;
    public KeyCode keyCode;

    #endregion

    #region Construction

    public KeyFrame() => SetFrame(KeyState.None, KeyCode.None);

    public KeyFrame(KeyFrame frame) => SetFrame(frame.keyState, frame.keyCode);

    public KeyFrame(int keyState, int keyCode) => SetFrame((KeyState)keyState, (KeyCode)keyCode);

    public KeyFrame(int keyState, KeyCode keyCode) => SetFrame((KeyState)keyState, keyCode);
    
    public KeyFrame(KeyState keyState, int keyCode) => SetFrame(keyState, (KeyCode)keyCode);
    
    public KeyFrame(KeyState keyState, KeyCode keyCode) => SetFrame(keyState, keyCode);

    #endregion

    #region Private Function

    private void SetFrame(KeyState state, KeyCode code) 
    {
        keyState = state;
        keyCode = code;
    }

    #endregion
}

#endregion

#region Data Asset Class

[System.Serializable]
public class KeyConfig
{
    #region Parameter Field

    public int maxKeyCounts = 15;

    public KeyFrame[] keyFrames = null;

    [SerializeField] private KeyCode[] defaultKeyList = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D,
                                        KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow,
                                        KeyCode.LeftShift, KeyCode.Space, KeyCode.Q, KeyCode.I, KeyCode.E, KeyCode.Escape, KeyCode.Tab };

    #endregion

    #region Construction

    public KeyConfig() => DefautConfig();

    public KeyConfig(KeyConfig config) 
    {
        maxKeyCounts = config.maxKeyCounts;
        keyFrames = config.keyFrames;
    }

    #endregion

    #region Public Function

    public void DefautConfig() 
    {
        maxKeyCounts = 15;

        keyFrames = new KeyFrame[maxKeyCounts];

        for(int i = 0; i < maxKeyCounts; i++) { keyFrames[i] = new KeyFrame(i, defaultKeyList[i]); }
    }

    public KeyConfig Copy()
    {
        KeyConfig keyConfig = new KeyConfig();

        keyConfig.maxKeyCounts = maxKeyCounts;

        keyConfig.keyFrames = new KeyFrame[maxKeyCounts];

        for (int i = 0; i < maxKeyCounts; i++) { keyConfig.keyFrames[i] = new KeyFrame(keyFrames[i]); }

        return keyConfig;
    }

    public void Clear() 
    {
        maxKeyCounts = 0;

        keyFrames = null;
    }

    public void Refresh(KeyConfig config) 
    {
        maxKeyCounts = config.maxKeyCounts;

        keyFrames = config.keyFrames;
    }

    #endregion

    #region Property
    //integer Input
    public KeyFrame this[int state] => keyFrames.Where(frame => frame.keyState == (KeyState)state).First();
    
    //Enum Key State Input
    public KeyFrame this[KeyState state] => keyFrames.Where(frame => frame.keyState == state).First();
    
    //Enum Key Code Input
    public KeyFrame this[KeyCode state] => keyFrames.Where(frame => frame.keyCode == state).First();
    
    //String Key State Input
    public KeyFrame this[string state] => keyFrames.Where(frame => frame.keyState.ToString().ToLower() == state.ToLower()).First();
    
    #endregion
}

#endregion