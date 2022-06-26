using UnityEngine;

public class DialogueMenu : MonoBehaviour
{
    #region Parameter Field

    [Header("Necessary Component")]
    public Animator animator;
    public GameObject backGroundImage;
    [Header("Dialogue Menu State")]
    [SerializeField] private bool menuState = false;

    private DialogueManager dialogueManager;

    #endregion

    #region Script Behaviour

    private void Start()
    {
        animator.GetComponent<Animator>();

        dialogueManager = ObjectPool.GetStaff("DialogueManager", "ScriptObject") as DialogueManager;
    }

    #endregion

    #region Public Function

    public void MenuButton() 
    { 
        menuState = !menuState;
        animator.SetBool("isOpen", menuState);
        backGroundImage.SetActive(menuState);
    }

    public void AutoButton() 
    {
        dialogueManager.Auto();
        MenuButton();
    }

    public void SkipMenu() 
    {
        dialogueManager.Skip();
        MenuButton();
    }

    public void LogButton() 
    {
        dialogueManager.SetLogText();
        MenuButton();
    }

    #endregion

    #region Parameter Setting

    public bool MenuState => menuState;

    #endregion
}
