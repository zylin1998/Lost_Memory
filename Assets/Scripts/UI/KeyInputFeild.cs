using UnityEngine;
using UnityEngine.UI;

public class KeyInputFeild : MonoBehaviour
{
    public string fieldName = string.Empty;
    public Button keyInput;
    public Text title;
    public Text keyText;
    public KeyState keyState;
    public KeyCode keyCode;

    private void Start()
    {
        Initialized();
    }

    public void Initialized()
    {
        if(fieldName == string.Empty) { title.text = keyState.ToString(); }
        
        if(fieldName != string.Empty) { title.text = fieldName; }
    }

    public void Reset(KeyCode keyCode)
    {
        this.keyCode = keyCode;

        keyText.text = this.keyCode.ToString();
    }

    public void Reset(int keyCode)
    {
        this.keyCode = (KeyCode)keyCode;

        keyText.text = this.keyCode.ToString();
    }
}
