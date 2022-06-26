using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    #region Parameter Field

    [SerializeField] private MIFrames mIFrames;

    [Header("GameObjects of Image UI")]
    [SerializeField] private GameObject backGroundImage;
    [SerializeField] private GameObject charaSpriteParent;

    private bool imageState = false;
    private bool spriteState = false;
    private int nowImageID = -1;

    private CharaSprite[] charaSprites;

    private EnviromentManager envirManager;

    #region Reachable Properties

    public bool SpriteState => spriteState;

    public int NowImageID => nowImageID;

    public bool ImageState
    {
        get => imageState;

        private set 
        {
            imageState = value;
            SetImageState(value);
        }
    }

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        ObjectPool.Add(this, "ImageController", "ScriptObject");
    }

    private void Start()
    {
        charaSprites = charaSpriteParent.GetComponentsInChildren<CharaSprite>();

        envirManager = EnviromentManager.instance;

        if (envirManager == null) { return; }

        ImageState = false;
        
        mIFrames = EnviromentManager.instance.MIFrames;
    }

    #endregion

    #region Public Function

    public void ImageSetting(Sentence sentence) 
    {
        if (!imageState) { ImageState = true; }

        SetBackGround(sentence);

        SetCharaSprite(sentence);

        charaSprites[1].Move(-300);
        charaSprites[2].Move(300);
    }

    #endregion

    #region Private Function

    private void SetImageState(bool state) 
    {
        charaSpriteParent.SetActive(state);
        backGroundImage.SetActive(state);
    }

    private void SetBackGround(Sentence sentence)
    {
        Image image = backGroundImage.GetComponent<Image>();

        if (!sentence.backGroundImage)
        {
            image.color = new Vector4(0, 0, 0, 145 / 255f);
            return;
        }

        if (sentence.ImageID == NowImageID) { return; }

        nowImageID = sentence.ImageID;

        image.sprite = mIFrames.frames[NowImageID].sprite;
        image.color = new Vector4(1, 1, 1, 1);

        if (!mIFrames.frames[NowImageID].used) 
        { 
            envirManager.Data.imageData.used[nowImageID] = true;
            envirManager.DataSaved();
        }
    }

    private void SetCharaSprite(Sentence sentence) 
    {
        spriteState = sentence.sprite;

        if (!SpriteState) { charaSpriteParent.SetActive(false); return; }

        if (!charaSpriteParent.activeSelf) { charaSpriteParent.SetActive(true); }

        for (int i = 0; i < charaSprites.Length; i++) 
        {
            if ((i + 1) == sentence.chara) { charaSprites[i].IsSpeak = true; }

            if ((i + 1) != sentence.chara) { charaSprites[i].IsSpeak = false; }

            charaSprites[i].SpeakState();
        }
    }

    #endregion
}
