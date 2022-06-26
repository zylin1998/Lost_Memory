using UnityEngine;
using UnityEngine.UI;

public class CrossScenes : MonoBehaviour
{
    [SerializeField] private Text text;

    private LoadScenes loadScenes;
    
    private void Start()
    {
        loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        loadScenes.LoadNextScene();
    }

    private void Update()
    {
        text.text = $"Loading...{loadScenes.Progress}%";
        
        if (loadScenes.Progress >= 100) { loadScenes.Asyncload.allowSceneActivation = true; }
    }
}