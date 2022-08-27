using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealingHookType : HookEngine
{
    [SerializeField] private float _damage;

    public override void Grapple() {}

    protected override void MoveHookHolderAfterLaunch() {}

    protected override void MoveHookHolderAfterLaunchWithEffect() {}

    protected override void MoveHookHolderAtLaunch() {}
}
