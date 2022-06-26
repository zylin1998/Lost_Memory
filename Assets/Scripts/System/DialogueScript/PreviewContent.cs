using UnityEngine;
using UnityEngine.UI;

public class PreviewContent : MonoBehaviour
{
    #region Parameter Field

    [Header("Text UI")]
    public Text text;
    [Header("Text Content")]
    public Sentence content;

    #endregion

    #region Content Input

    public void SetContent(Sentence sentence)
    {
        content = new Sentence(sentence.type, sentence.id, sentence.chara, sentence.dialogue);

        PreviewText();
    }

    #endregion

    #region Content Format

    public void PreviewText() 
    {
        string name;
     
        if (content.chara == 0) { name = "®Ç¥Õ"; }
        else { name = FindObjectOfType<PreviewList>().charas[content.chara].name; }

        string preview = $"{name}\n{content.dialogue}";

        text.text = preview;
    }

    #endregion
}