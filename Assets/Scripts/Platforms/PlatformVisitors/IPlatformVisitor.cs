using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlatformVisitor
{
    void Visit(AttractingPlatform attractingPlatform);
    void Visit(PhysicsPlatform physicsPlatform);
    void Visit(BouncePlatform bouncePlatform);
    void Visit(TransporterPlatform transporterPlatform);
}
