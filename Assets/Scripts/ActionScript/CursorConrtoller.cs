using UnityEngine;

public class CursorConrtoller : MonoBehaviour
{
    private void Update() => Cursor.lockState = EnviromentManager.instance.Pause ? CursorLockMode.None : CursorLockMode.Locked;
}
