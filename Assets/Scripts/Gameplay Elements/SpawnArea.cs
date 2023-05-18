using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SpawnArea 
{
    public Transform minTransform;
    public Transform maxTransform;
    public int minNumberOfCollectibles;
    public int maxNumberOfCollectibles;
}
