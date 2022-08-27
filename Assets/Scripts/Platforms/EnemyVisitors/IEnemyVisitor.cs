using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyVisitor
{
    void Visit(Enemy enemy);
}
