using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private LoadScenes loadScenes;

    private void Start()
    {
        loadScenes = ObjectPool.GetStaff("LoadScenes", "ScriptObject") as LoadScenes;

        EnviromentManager.instance.SetResolution();
    }

    public void StartGame() 
    {
        StaticValue.targetScene = "Beginning";
        StaticValue.userData = new UserData();

        Debug.Log("Start Game");

        StartCoroutine(loadScenes.LoadCrossScene());
    }

    public void QuitGame() 
    {
        Debug.Log("Game Quit");

        Application.Quit();
    }
}
