using System.Collections;
using UnityEngine;

public class KeyInputManager : MonoBehaviour
{
    [Header("Key Input Components")]
    public Transform keyParent;
    public KeyConfig keyConfig;

    private bool waitKey = false;
    private bool isChanged = false;

    private KeyState keyState;
    private KeyCode keyCode;

    private Event keyEvent;
    private KeyInputFeild[] inputFeilds;

    void Start()
    {
        inputFeilds = keyParent.GetComponentsInChildren<KeyInputFeild>();
        keyConfig = EnviromentManager.instance.Data.keyConfig.Copy();
    }

    public void Initialize() 
    {
        foreach (KeyInputFeild field in inputFeilds) { field.Reset(keyConfig[field.keyState].keyCode); }
    }

    public void SaveChanged() 
    {
        if (isChanged) { EnviromentManager.instance.Data.keyConfig.Refresh(keyConfig); }

        isChanged = false;
    }

    #region Changing Key

    public void GetKey(int keyState) {

        InputFieldState(false);

        this.keyState = (KeyState)keyState;
        
        Invoke("DelayGetKey", 0.3f);
    }

    public void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitKey)
        {
            if (keyEvent.keyCode != 0) { keyCode = keyEvent.keyCode; }
            
            waitKey = false;
        }

    }

    private void DelayGetKey() 
    {
        if (!waitKey) { StartCoroutine(AssignKey()); }
    }

    private IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey) { yield return null; }
    }

    private IEnumerator AssignKey()
    {
        waitKey = true;

        yield return WaitForKey();

        KeyChanged();

        InputFieldUpdate();

        InputFieldState(true);

        keyState = KeyState.None;
        keyCode = KeyCode.None;
    }

    private void KeyChanged() 
    {
        KeyCode temp = keyConfig[keyState].keyCode;

        if (keyCode == temp) { return; } 

        keyConfig[keyState].keyCode = keyCode;
        
        isChanged = true;

        foreach (KeyFrame frame in keyConfig.keyFrames)
        {
            if (frame.keyCode != keyCode) { continue; }

            if (frame.keyState == keyState) { continue; }

            frame.keyCode = temp;
            break;
        }
    }

    #endregion

    private void InputFieldState(bool value)
    {
        foreach (KeyInputFeild field in inputFeilds) { field.keyInput.interactable = value; }
    }

    private void InputFieldUpdate() 
    {
        if (!isChanged) { return; }

        foreach (KeyInputFeild field in inputFeilds)
        {
            KeyCode temp = keyConfig[field.keyState].keyCode;

            if (field.keyCode != temp) { field.Reset(temp); }
        }
    }
}
