using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public abstract void Accept(IPlatformVisitor visitor);
}
