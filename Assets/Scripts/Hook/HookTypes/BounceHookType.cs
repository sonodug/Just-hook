using UnityEngine;

public class BounceHookType : HookEngine
{
    [SerializeField] private float _launchSpeed;
    [SerializeField] private float _jumpForce;

    private bool _isReadyToJerk;

    public override void Grapple()
    {
        Rigidbody.gravityScale = 0;
        Rigidbody.velocity = Vector2.zero;
    }
    protected override void MoveHookHolderAtLaunch()
    {
        _isReadyToJerk = true;

        Vector2 firePointDistnace = ShotPoint.position - HookHolder.localPosition;
        Vector2 targetPos = GrapplePoint - firePointDistnace;
        HookHolder.position = Vector2.MoveTowards(HookHolder.position, targetPos, Time.deltaTime * _launchSpeed);
    }

    protected override void MoveHookHolderAfterLaunch()
    {
        if (_isReadyToJerk)
        {
            Rigidbody.velocity = GrappleDistanceVector;
        }

        _isReadyToJerk = false;
    }

    protected override void MoveHookHolderAfterLaunchWithEffect()
    {
        Rigidbody.gravityScale = 1;
        Rigidbody.velocity = Vector2.Reflect(GrappleDistanceVector, Vector2.up) * _jumpForce;
    }
}
