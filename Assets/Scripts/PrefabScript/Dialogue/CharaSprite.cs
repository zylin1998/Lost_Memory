using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharaSprite : MonoBehaviour
{
    #region Parameter Field

    [Header("Components of Sprite UI")]
    [SerializeField] private RectTransform character;
    [SerializeField] private Image spriteRenderer;
    [Header("Sprite Detail")]
    
    private bool isSpeak;
    
    #region Reachable Properties

    public bool IsSpeak { get => isSpeak; set => isSpeak = value; }

    #endregion
    
    #endregion

    #region Public Function

    public void SpeakState() 
    {
        if (isSpeak) { spriteRenderer.color = Color.white; }

        if (!isSpeak) { spriteRenderer.color = Color.gray; }
    }

    public void Move(int posiX) 
    {
        character.localPosition = new Vector3(posiX , character.localPosition.y, 0f);
    }

    #endregion

    #region Private Function

    public IEnumerator MoveSprite(int posiX) 
    {
        float newPosiX = character.localPosition.x;
        float distance = (posiX - newPosiX);

        while (true) 
        {
            transform.Translate(distance * Time.deltaTime, 0, 0f);

            if(character.localPosition.x >= posiX) { break; }

            yield return null;
        }
    }

    #endregion
}
