using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformContainer
{
    [SerializeField]
    private BasePlatform platformPrefab;
    [SerializeField]
    [Range(0, 100)]
    private int spawnChance;

    public BasePlatform PlatformPrefab => platformPrefab;

    public int SpawnChance => spawnChance;
}
