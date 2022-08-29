using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentToColorMatcherVisitor : IEnvironmentVisitor
{
    private FocusingLaser _laser;

    public void Visit(AttractingPlatform attractingPlatform)
    {
        _laser.SetLaserColor(Color.cyan);
    }

    public void Visit(PhysicsPlatform physicsPlatform)
    {
        _laser.SetLaserColor(Color.black);
    }

    public void Visit(BouncePlatform bouncePlatform)
    {
        _laser.SetLaserColor(Color.yellow);
    }

    public void Visit(TransporterPlatform transporterPlatform)
    {
        _laser.SetLaserColor(Color.green);
    }

    public void Visit(EnemyEnvironment enemy)
    {
        _laser.SetLaserColor(Color.red);
    }

    public void Init(FocusingLaser laser)
    {
        _laser = laser;
    }

}
