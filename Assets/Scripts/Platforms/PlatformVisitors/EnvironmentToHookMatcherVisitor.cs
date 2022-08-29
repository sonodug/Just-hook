using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentToHookMatcherVisitor : IEnvironmentVisitor
{
    private Hook _hook;

    public void Visit(AttractingPlatform attractingPlatform)
    {
        _hook.SetCurrentHook(Hook.Hook_Type.AttractingHookType);
    }

    public void Visit(PhysicsPlatform physicsPlatform)
    {
        _hook.SetCurrentHook(Hook.Hook_Type.PhysicsHookType);
    }

    public void Visit(BouncePlatform bouncePlatform)
    {
        _hook.SetCurrentHook(Hook.Hook_Type.BounceHookType);
    }

    public void Visit(TransporterPlatform transporterPlatform)
    {
        _hook.SetCurrentHook(Hook.Hook_Type.TransporterHookType);
    }

    public void Visit(EnemyEnvironment enemy)
    {
        _hook.SetCurrentHook(Hook.Hook_Type.DamageDealingHookType);
    }

    public void Init(Hook hook)
    {
        _hook = hook;
    }

}
