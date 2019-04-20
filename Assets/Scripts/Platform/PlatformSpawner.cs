using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    #region PrivateVaraibles
    private float _cameraHorizontalHalfSize;
    private float _minHorizontalDistance;
    private float _maxHorizontalDistance;
    private float _minVerticalDistance;
    private float _maxVerticalDistance;
    private BasePlatform _currentPlatform;
    private Vector3 _lastPosition;
    private float _nextHorizontalDistance;
    private PlatformPool _platformPool;
    #endregion

    private void Awake()
    {
        _cameraHorizontalHalfSize = Camera.main.orthographicSize * Screen.width / Screen.height;
    }

    public void Setup(float minHorizontalDistance,
                      float maxHorizontalDistance,
                      float minVerticalDistance,
                      float maxVerticalDistance,
                      PlatformPool platformPool)
    {
        _minHorizontalDistance = minHorizontalDistance;
        _maxHorizontalDistance = maxHorizontalDistance;
        _minVerticalDistance = minVerticalDistance;
        _maxVerticalDistance = maxVerticalDistance;
        _platformPool = platformPool;
        Spawn();
    }

    private void Update()
    {
        if ((_currentPlatform != null) && ((Camera.main.transform.position.x + _cameraHorizontalHalfSize) - (_lastPosition.x + _currentPlatform.transform.localScale.x / 2) >= _nextHorizontalDistance))
        {
            Spawn();
            _nextHorizontalDistance = Random.Range(_minHorizontalDistance, _maxHorizontalDistance);
        }
    }

    void Spawn()
    {
        if (_platformPool == null)
        {
            throw new Exception("Tried to spawn a platform before initializing the pool.");
        }

        _currentPlatform = _platformPool.Get();
        _lastPosition = new Vector3(
            Camera.main.transform.position.x + _cameraHorizontalHalfSize + _currentPlatform.transform.localScale.x / 2,
            Random.Range(_minVerticalDistance, _maxHorizontalDistance),
            _currentPlatform.transform.position.z);
        _currentPlatform.transform.position = _lastPosition;
    }
}
