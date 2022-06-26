using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InformImage : MonoBehaviour
{
    #region Reachable Properties

    public Image targetImage => GetComponent<Image>();

    #endregion

    void Awake() => ObjectPool.Add(this, "InformImage", "ScriptObject");

    public IEnumerator PictureDisplay() 
    {
        bool isEscape;

        while(true)
        {
            isEscape = Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1);

            if (isEscape) { break; }

            yield return null;
        }

        targetImage.sprite = null;

        gameObject.SetActive(false);
    }
}
