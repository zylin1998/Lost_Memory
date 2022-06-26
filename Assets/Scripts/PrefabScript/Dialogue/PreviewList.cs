using System.Collections.Generic;
using UnityEngine;

public class PreviewList : MonoBehaviour
{
    #region Parameter Field

    [Header("GameObjects of UI")]
    public GameObject previewText;
    
    [Header("Preview Contexts")]
    public List<GameObject> previewTexts;
    
    public List<Chara> charas;

    #endregion

    #region Script Behaviour

    private void Awake() => ObjectPool.Add(this, "PreviewList", "ScriptObject");

    private void Start() 
    {
        previewTexts = new List<GameObject>();
    }

    #endregion

    #region Preview Text Object Setting

    public void Initialized()
    {
        if (gameObject.transform.childCount != 0)
        {
            Transform[] elderList = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform transform in elderList)
            {
                if (transform.name.Equals(gameObject.name)) { continue; }

                Destroy(transform.gameObject);
            }
        }

        previewTexts.Clear();
    }

    public void IncreaseText(Sentence sentence)
    {
        GameObject newObject;

        newObject = Instantiate(previewText, transform);

        newObject.GetComponent<PreviewContent>().SetContent(sentence);

        previewTexts.Add(newObject);

        newObject.name = $"Sentence {sentence.id}";
    }

    #endregion
}
