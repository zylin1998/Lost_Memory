using UnityEngine;

#region Data Asset Class

[CreateAssetMenu(fileName = "CharaSprites", menuName = "Dialogue/CharaSprites", order = 1)]
public class CharaSprites : ScriptableObject
{
    #region Parameter Field

    [Header("Character Sprite Prefabs")]
    public GameObject[] charaSprites;

    #endregion
}

#endregion