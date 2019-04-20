using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ColorChaser/WorldConfig")]
public class WorldConfig : ScriptableObject
{
    [Header("Platform Prefab")]
    public PlatformSpawner PlatformSpawnerPrefab;
    public List<PlatformContainer> PlatformPrefabs;

    [Space]

    [Header("Floats and Ints")]
    public float PlatformMinHorizontalDistance, PlatformMaxHorizontalDistance;
    public float PlatformMinVerticalDistance, PlatformMaxVerticalDistance;
    public float PlatformMinLength, PlatformMaxLength;
    public float GreenPlatformJumpMultiplier, RedPlatformJumpMultiplier;
    public float BlackPlatformEffectTime;
}
