using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToHookMatcherVisitor : IEnemyVisitor
{
    private Hook _hook;

    public void Init(Hook hook)
    {
        _hook = hook;
    }

    public void Visit(EnemyEnvironment enemy)
    {
        throw new System.NotImplementedException();
    }
}
