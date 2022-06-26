using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Animancer;

[RequireComponent(typeof(AnimancerComponent))]
public class BasicMovement : MonoBehaviour
{
    [Header("Animations")]
    [SerializeField] private AnimationClip jumpStart;
    [SerializeField] private AnimationClip inAir;
    [SerializeField] private LinearMixerTransition move;
    [SerializeField] private LinearMixerTransition landed;

    private AnimancerComponent animancer;

    private BasicMovementAsset movementAsset;

    private void Awake()
    {
        animancer = this.GetComponent<AnimancerComponent>();
        animancer.Animator = this.GetComponent<Animator>();

        string animationPath = Path.Combine("Character", "Animation");

        movementAsset = Resources.Load<BasicMovementAsset>(Path.Combine(animationPath, $"{gameObject.name}_Movement"));

        move = movementAsset.moveAsset.Transition;
        landed = movementAsset.landAsset.Transition;

        jumpStart = movementAsset.jumpStart;
        inAir = movementAsset.inAir;

        animancer.Evaluate();

        animancer.States.GetOrCreate(move);
        animancer.States.GetOrCreate(landed);

        Move();
    }

    public void Move()
    {
        animancer.Play(move, 0.25f, FadeMode.FixedDuration);
    }

    public void Jump() 
    {
        animancer.Play(jumpStart, 0.25f, FadeMode.FixedDuration).Events.OnEnd 
                    = () => animancer.Play(inAir, 0.25f, FadeMode.FixedDuration);
    }

    public void Fall() 
    {
        animancer.Play(inAir, 0.25f, FadeMode.FixedDuration);
    }

    public void Landed() 
    {
        animancer.Play(landed, 0.25f, FadeMode.FixedDuration).Events.OnEnd
                    = () => animancer.Play(move, 0.25f, FadeMode.FixedDuration);
    }

    public bool isFalling => animancer.IsPlayingClip(inAir);

    public float MoveState
    {
        get { return move.State.Parameter; }

        set { move.State.Parameter = value; }
    }

    public float LandState
    {
        get { return landed.State.Parameter; }

        set { landed.State.Parameter = value; }
    }
}