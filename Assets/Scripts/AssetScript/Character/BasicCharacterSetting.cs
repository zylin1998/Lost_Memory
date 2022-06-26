using UnityEngine;

[CreateAssetMenu(fileName = "Basic Character Setting", menuName = "Character/Basic Character Setting", order = 1)]
public class BasicCharacterSetting : ScriptableObject
{
    [Header("Character Controller")]
    public float radius;
    public float height;

    [Header("Player Controller")]
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpHeight;
}
