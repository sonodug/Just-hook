using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnvironmentVisitor
{
    void Visit(AttractingPlatform attractingPlatform);
    void Visit(PhysicsPlatform physicsPlatform);
    void Visit(BouncePlatform bouncePlatform);
    void Visit(TransporterPlatform transporterPlatform);
    void Visit(Enemy enemy);
}
