using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDyingPolicy : IDyingPolicy
{
    public bool Died(float health)
    {
        return health <= 0;
    }
}
