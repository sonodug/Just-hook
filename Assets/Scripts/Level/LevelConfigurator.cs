using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Config", menuName = "Config", order = 51)]
public class LevelConfigurator : ScriptableObject
{
    [SerializeField] private int _gemsCollectToFinish;

    public int GemsCollectToFinish => _gemsCollectToFinish;

    private void OnValidate()
    {
        
    }
}
