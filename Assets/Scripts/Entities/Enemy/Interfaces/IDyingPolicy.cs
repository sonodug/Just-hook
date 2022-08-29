using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDyingPolicy
{
    public bool Died(float health);
}
