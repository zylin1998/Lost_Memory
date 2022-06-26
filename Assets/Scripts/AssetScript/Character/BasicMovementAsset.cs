using UnityEngine;
using Animancer;

[CreateAssetMenu(fileName = "Basic Movement Asset", menuName = "Character/Basic Movement Asset", order = 1)]
public class BasicMovementAsset : ScriptableObject
{
    [Header("Linear Mixer Transition Asset")]
    public LinearMixerTransitionAsset moveAsset;
    public LinearMixerTransitionAsset landAsset;

    [Header("Animation Clips")]
    public AnimationClip jumpStart;
    public AnimationClip inAir;
}
