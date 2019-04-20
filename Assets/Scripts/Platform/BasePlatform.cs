using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    private event Action<BasePlatform> ReturnToPoolEvent;

    protected float _greenPlatformMultiplier, _redPlatformMultiplier;
    protected float _blackPlatformChangeTime;

    public void Setup(Action<BasePlatform> ReturnToPoolEvent, float greenPlatformMultiplier, float redPlatformMultiplier, float blackPlatfromChangeTime)
    {
        this.ReturnToPoolEvent = ReturnToPoolEvent;
        this._greenPlatformMultiplier = greenPlatformMultiplier;
        this._redPlatformMultiplier = redPlatformMultiplier;
        this._blackPlatformChangeTime = blackPlatfromChangeTime;
    }

    private void Update()
    {
        if (Camera.main.transform.position.x > transform.position.x + 20)
            ReturnToPoolEvent(this);
    }
}
