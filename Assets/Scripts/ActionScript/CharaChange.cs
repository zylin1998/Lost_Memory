using UnityEngine;

public class CharaChange : MonoBehaviour
{
    #region Parameter Field

    [Header("Necessary Game Components.")]
    public GameObject[] characters;

    [Header("Currently Character")]
    [SerializeField] private int charaState = 0;

    [Header("Necessary Script Components")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private KeyManager keyManager;

    private Vector3 originPosi = Vector3.one;
    private Quaternion originRotate = Quaternion.Euler(0, 0, 0);

    public Transform CurrentChara => characters[charaState].transform;

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "CharaChange", "ScriptObject");
    }

    void Start()
    {
        keyManager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;
        //ChangeCharacter();
    }
    void Update()
    {
        if (!keyManager.TabState) { return; }
        if (EnviromentManager.instance.Pause) { return; }

        originPosi = characters[charaState].transform.position;
        originRotate = characters[charaState].transform.rotation;

        charaState++;
        ChangeCharacter();
    }

    #endregion

    #region Change Character

    public void ChangeCharacter() 
    {
        if(charaState == characters.Length) { charaState = 0; }

        characters[charaState].transform.position = originPosi;
        characters[charaState].transform.rotation = originRotate;

        for (int i = 0; i < characters.Length; i++)
        {
            if(i == charaState) { continue; }

            if(i != charaState) { characters[i].SetActive(false); }
        }

        characters[charaState].SetActive(true); 
        //cameraController.ChangeFocus(characters[charaState].transform);
    }

    #endregion

    #region Parameter Setting

    public int CharaState => charaState;

    #endregion
}
