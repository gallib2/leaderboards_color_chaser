using System.Collections.Generic;
using UnityEngine;

public class PlatformPool
{
    private List<PlatformContainer> platformContainers;
    private List<BasePlatform> availiablePlatforms;
    private List<BasePlatform> inUsePlatforms;

    private float _greenPlatformMultiplier, _redPlatformMultiplier;
    private float _blackPlatformChangeTime;

    public PlatformPool(List<PlatformContainer> platformContainers, int initialAmount, float greenPlatformMultiplier, float redPlatformMultiplier, float blackPlatformChangeTime)
    {
        availiablePlatforms = new List<BasePlatform>();
        inUsePlatforms = new List<BasePlatform>();
        this.platformContainers = platformContainers;
        this._greenPlatformMultiplier = greenPlatformMultiplier;
        this._redPlatformMultiplier = redPlatformMultiplier;
        this._blackPlatformChangeTime = blackPlatformChangeTime;

        foreach (PlatformContainer platformContainer in platformContainers)
        {
            for (int i = 0; i < initialAmount; i++)
            {
                availiablePlatforms.Add(CreatePlatform(platformContainer.PlatformPrefab));
            }
        }
    }

    private BasePlatform CreatePlatform(BasePlatform platformPrefab)
    {
        BasePlatform platform = Object.Instantiate(platformPrefab);
        platform.Setup(Return, _greenPlatformMultiplier, _redPlatformMultiplier, _blackPlatformChangeTime);
        platform.gameObject.SetActive(false);
        return platform;
    }

    public BasePlatform Get()
    {
        int random = Random.Range(0, 101);
        BasePlatform platformPrefab = platformContainers[0].PlatformPrefab;
        foreach (PlatformContainer platformContainer in platformContainers)
        {
            random -= platformContainer.SpawnChance;
            if (random <= 0)
            {
                platformPrefab = platformContainer.PlatformPrefab;
                break;
            }
        }
        BasePlatform platform = availiablePlatforms.Find(availiablePlatform => availiablePlatform.GetType() == platformPrefab.GetType());
        if (platform == null)
        {
            platform = CreatePlatform(platformPrefab);
        }
        else
        {
            availiablePlatforms.Remove(platform);
        }
        inUsePlatforms.Add(platform);
        platform.gameObject.SetActive(true);
        return platform;
    }

    private void Return(BasePlatform platform)
    {
        platform.gameObject.SetActive(false);
        inUsePlatforms.Remove(platform);
        availiablePlatforms.Add(platform);
    }
}
