using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    private float progress;

    private AsyncOperation asyncload;

    #region Reachable Properties

    public float Progress => progress;

    public AsyncOperation Asyncload => asyncload;

    #endregion

    private void Awake() => ObjectPool.Add(this, "LoadScenes", "ScriptObject");

    public void LoadNewScene() => StartCoroutine(LoadCrossScene());

    public void LoadNewScene(string sceneName) => StartCoroutine(LoadAsyncScene(sceneName));

    public void LoadNextScene() => StartCoroutine(LoadAsyncScene(StaticValue.targetScene));

    private IEnumerator LoadAsyncScene(string scene)
    {
        asyncload = SceneManager.LoadSceneAsync(scene);

        asyncload.allowSceneActivation = false;

        ObjectPool.Reset();

        while (!asyncload.isDone)
        {
            progress = Mathf.FloorToInt(Mathf.Clamp01(asyncload.progress / 0.9f) * 100);
            yield return null;
        }
    }

    public IEnumerator LoadCrossScene()
    {
        LoadNewScene("Cross Scene");

        while (progress < 100) { yield return null; }

        asyncload.allowSceneActivation = true;
    }

    public IEnumerator LoadAtDialogueEnd()
    {
        while (EnviromentManager.instance.DialogueMode) { yield return null; }

        StartCoroutine(LoadCrossScene());
    }
}
