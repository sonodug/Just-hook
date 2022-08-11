using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToColorMatcherVisitor : IPlatformVisitor
{
    private FocusingLaser _laser;

    public void Visit(AttractingPlatform attractingPlatform)
    {
        _laser.SetLaserColor(Color.cyan);
    }

    public void Visit(PhysicsPlatform physicsPlatform)
    {
        _laser.SetLaserColor(Color.red);
    }

    public void Visit(BouncePlatform bouncePlatform)
    {
        _laser.SetLaserColor(Color.yellow);
    }

    public void Visit(TransporterPlatform transporterPlatform)
    {
        throw new System.NotImplementedException();
    }

    public void Init(FocusingLaser laser)
    {
        _laser = laser;
    }
}
