public interface IEnvironmentVisitor
{
    void Visit(AttractingPlatform attractingPlatform);
    void Visit(PhysicsPlatform physicsPlatform);
    void Visit(BouncePlatform bouncePlatform);
    void Visit(TransporterPlatform transporterPlatform);
    void Visit(EnemyEnvironment enemy);
}
