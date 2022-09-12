namespace Entities.Enemy.DyingPolicies
{
    public class NormalDyingPolicy : IDyingPolicy
    {
        public bool Died(float health)
        {
            return health <= 0;
        }
    }
}
