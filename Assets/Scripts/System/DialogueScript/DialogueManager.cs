using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Parameter Field

    [SerializeField] private TextSetting textSetting;
    [SerializeField] private DialogueData dialogue;

    [Header("Dialogue Text UI")]
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;

    private List<Chara> charas;
    private Queue<Sentence> sentences;
    private List<Sentence> log;

    private ImageController imageController;
    private PreviewList previewList;
    private CanvasController canvasController;
    
    private EnviromentManager envirManager;
    
    private bool autoMode = false;
    private bool skipMode = false;

    public delegate void DialogueEnd();
    public DialogueEnd dialogueEndCallBack;
    
    #region Reachable Properties

    public bool AutoMode => autoMode;

    public bool SkipMode => skipMode;

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "DialogueManager", "ScriptObject");
    }

    private void Start()
    {
        envirManager = EnviromentManager.instance;
        textSetting = EnviromentManager.instance.Data.text;

        log = new List<Sentence>();
        charas = new List<Chara>();
        sentences = new Queue<Sentence>();
        
        InitialObjects();
    }

    #endregion

    #region Dialogue Initializing

    public void StartDialogue() 
    {
        SetDialogueMode(true);

        Clear();

        SetChara();

        SetSentences();
        
        DisplayNextSentence();
    }

    #endregion

    #region Dialogue Display

    public void SetDialogue(DialogueData data)
    {
        dialogue = data;
    }

    public void DisplayNextSentence() {
    
        if(sentences.Count == 0) { EndDialogue(); return; }

        Sentence sentence = sentences.Dequeue();
        
        log.Add(sentence);

        imageController.ImageSetting(sentence);

        nameText.text = charas[sentence.chara].name;

        if (skipMode) { SkipModeTypeSentence(sentence.dialogue); }

        if (!skipMode)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence.dialogue));
        }
    }

    private void SkipModeTypeSentence(string sentence) 
    {
        dialogueText.text = sentence;

        Invoke("DisplayNextSentence", 0.15f);
    }

    #endregion

    #region Coroutine Functions

    private IEnumerator TypeSentence(string sentence) 
    {
        dialogueText.text = "";

        foreach(char letter in sentence)
        {
            while(IsPause()) { yield return null; }

            if (Input.GetKey(KeyCode.Space)) { dialogueText.text = sentence; break; }

            if (Input.GetKey(KeyCode.LeftControl)) { SkipModeTypeSentence(sentence); }

            dialogueText.text += letter;

            yield return new WaitForSeconds(textSetting.textSpeed * 0.02f);
        }

        StartCoroutine(WaitKey());
        if (autoMode && !skipMode) { StartCoroutine(WaitAutoTime()); }
    }

    private IEnumerator WaitKey() 
    {
        while(true)
        {
            if (Input.GetKeyDown(KeyCode.Space)) { break; }

            if (Input.GetKey(KeyCode.LeftControl)) { break; }

            yield return null;
        }

        Invoke("DisplayNextSentence", 0.15f);
    }

    private IEnumerator WaitAutoTime() 
    {
        float starTime = Time.realtimeSinceStartup;

        while (true) 
        {
            if(Time.realtimeSinceStartup - starTime >= 1f + textSetting.autoSpeed * 0.5f) { break; }

            yield return null;
        }

        DisplayNextSentence();
    }

    #endregion

    #region Log of Used Dialogue

    public void SetLogText() 
    {
        if(previewList == null) { previewList = ObjectPool.GetStaff("PreviewList", "ScriptObject") as PreviewList; }

        previewList.charas = charas;
        previewList.Initialized();

        foreach (Sentence sentence in log) { previewList.IncreaseText(sentence); }
    }

    #endregion

    #region End Dialogue

    public void EndDialogue() 
    {
        SetDialogueMode(false);
        
        skipMode = false;

        if (dialogueEndCallBack != null) { dialogueEndCallBack.Invoke(); }
    }

    #endregion

    #region State Function

    private void InitialObjects() 
    {
        if (imageController == null) { imageController = ObjectPool.GetStaff("ImageController", "ScriptObject") as ImageController; }
        if (canvasController == null) { canvasController = ObjectPool.GetStaff("CanvasController", "ScriptObject") as CanvasController; }
    }

    private void SetDialogueMode(bool state) 
    { 
        envirManager.DialogueMode = state;
        canvasController.DialoguePage(state);
    }

    private void Clear() 
    {
        log.Clear();
        charas.Clear();
        sentences.Clear();
    }

    private void SetChara() 
    {
        charas.Add(new Chara());

        foreach (Chara chara in dialogue.charas) { charas.Add(chara); }
    }

    private void SetSentences() 
    {
        foreach (Sentence sentence in dialogue.sentences) { sentences.Enqueue(sentence); }
    }
    
    public void Auto() 
    {
        autoMode = !autoMode; 

        if (autoMode) { DisplayNextSentence(); }
    }

    public void Skip() 
    {
        skipMode = !skipMode;

        if (skipMode) { DisplayNextSentence(); }
    }

    private bool IsPause() => EnviromentManager.instance.InventoryMode;

    #endregion
}