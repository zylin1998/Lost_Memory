using System.Collections;
using UnityEngine;

public class EndingUI : MonoBehaviour
{
    [SerializeField] private GameObject endingUI;

    private bool hasStart = false;
    private bool isAwake = false;

    private void Update()
    {
        if (EnviromentManager.instance.DialogueMode) { hasStart = true; }

        if (!hasStart) { return; }

        if (EnviromentManager.instance.DialogueMode) { return; }

        if (isAwake) { return; }

        endingUI.SetActive(true);

        EnviromentManager.instance.ImageMode = true;

        StartCoroutine(WaitKey());

        isAwake = true;
    }

    private IEnumerator WaitKey() 
    {
        while (!Input.anyKey) { yield return null; }

        EvenManager.manager.EndGame();
    }
}
