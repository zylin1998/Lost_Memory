using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    [System.Serializable]
    public enum AnimationState
    {
        Animator = 0,
        Animancer = 1
    }

    [SerializeField] private string[] characterNames;
    [SerializeField] private int characterState;
    [SerializeField] private AnimationState animationState;

    private PlayerData playerData;
    
    private CameraController cameraController;
    
    private GameObject parent;
    private List<GameObject> characters;
    private Transform currentCharacter;

    public Transform CurrentCharacter => currentCharacter;

    private void Awake()
    {
        ObjectPool.Add(this, "CharacterState", "ScriptObject");

        characters = new List<GameObject>();
    }

    private void Start()
    {
        Initialized();
    }

    private void Initialized()
    {
        cameraController = ObjectPool.GetStaff("CameraController", "ScriptObject") as CameraController;

        playerData = StaticValue.userData.hasSaved ? StaticValue.userData.player : new PlayerData();

        parent = GameObject.Find("Player");
        
        if (characterNames.Length <= 0) { return; }

        foreach (string name in characterNames)
        {
            GameObject added = Resources.Load<GameObject>(Path.Combine("Character", "3D", name, name));

            if (added != null) { characters.Add(added); }
        }

        SetCharacter();
    }

    private void SetCharacter() 
    {
        Transform temp = currentCharacter == null ? null : currentCharacter;

        GameObject targetCharacter = null;

        var character = characters[characterState];
        var position = playerData.Position;
        var rotation = playerData.Rotation;

        if (parent != null) 
        { 
            targetCharacter = Instantiate(character, position, rotation, parent.transform);

            targetCharacter.name = character.name;

            if (animationState == AnimationState.Animancer) { targetCharacter.AddComponent<AnimancerMovement>(); }

            if (animationState == AnimationState.Animator) { targetCharacter.AddComponent<ThirdPersonMovement>(); }
        }

        if (targetCharacter != null) { currentCharacter = targetCharacter.transform; }

        if (currentCharacter != null) { cameraController.ChangeFocus(); }

        if (temp != null) { Destroy(temp); }
    }

    public void ChangeCharacter(int state) 
    {
        if(state == characterState) { return; }

        characterState = state;

        SetCharacter();
    }
}
