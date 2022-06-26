using UnityEngine;
using UnityEngine.UI;

public class GalleryImage : MonoBehaviour
{

    [SerializeField] private int imageID;
    [SerializeField] private Sprite initialSprite;

    [SerializeField] private string prvImageName = "PreviewImage";
    [SerializeField] private string pagCtrName = "GalleryPagCtr";


    private MIFrames frames;

    private int page;

    private Image image;
    private Image previewImage;

    private PageController pageController;

    #region Reachable Properties

    public int Page { get => page; set => page = value; }

    #endregion

    private void Start()
    {
        frames = EnviromentManager.instance.MIFrames;

        image = GetComponent<Image>();

        previewImage = ObjectPool.GetStaff(prvImageName, "UI") as Image;

        pageController = ObjectPool.GetStaff(pagCtrName, "ScriptObject") as PageController;

        image.sprite = frames[imageID].used ? frames[imageID].sprite : initialSprite;
    }

    public void PreviewImage() 
    {
        if(!frames[imageID].used) { return; }

        pageController.OpenChildPage(0);

        previewImage.sprite = frames[imageID].sprite;
    }
}
