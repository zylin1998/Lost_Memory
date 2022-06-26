using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    #region Parameter Field

    private CinemachineFreeLook freeLookCamera;
    
    [Header("Dynamic Direction")]
    public Vector2 deltaDirect = new Vector2();
    public Vector2 sightDirect = new Vector2();

    private KeyManager keyManager;
    private CharacterState characterState;
    private EnviromentManager envirManager;

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "CameraController", "ScriptObject");
        
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    private void Start()
    {
        keyManager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;

        envirManager = EnviromentManager.instance;
    }

    private void Update()
    {
        CameraIsEnable(!EnviromentManager.instance.Pause);

        SightUpdate();
    }

    #endregion

    #region Camera Sight Update

    private void SightUpdate() 
    {
        float deltaX = envirManager.Data.cameraSight.xDelta;
        float deltaY = envirManager.Data.cameraSight.yDelta;

        freeLookCamera.m_XAxis.m_MaxSpeed = 400 * deltaX / 10 + 100f;
        freeLookCamera.m_YAxis.m_MaxSpeed = 4 * deltaY / 10 + 1f;

        sightDirect.x = freeLookCamera.m_XAxis.Value;
        sightDirect.y = freeLookCamera.m_YAxis.Value;

        deltaDirect = keyManager.SightDirect;

        sightDirect.y += deltaDirect.y * 0.5f * freeLookCamera.m_YAxis.m_MaxSpeed * Time.deltaTime;
        sightDirect.x += deltaDirect.x * 0.5f * freeLookCamera.m_XAxis.m_MaxSpeed * Time.deltaTime;

        freeLookCamera.m_XAxis.Value = sightDirect.x;
        freeLookCamera.m_YAxis.Value = sightDirect.y;
    }

    public void ChangeFocus() 
    {
        if(characterState == null) { characterState = ObjectPool.GetStaff("CharacterState", "ScriptObject") as CharacterState; }

        Transform character = characterState.CurrentCharacter;

        var position = character.position + new Vector3(0f, character.GetComponent<CharacterController>().height * 0.75f, 0f);

        Transform focusPoint = new GameObject("FocusPoint").transform;

        focusPoint.parent = character;
        focusPoint.position = position;

        freeLookCamera.Follow = character.transform;
        freeLookCamera.LookAt = focusPoint.transform;
    }

    public void CameraIsEnable(bool state) => freeLookCamera.enabled = state;
    
    #endregion
}
