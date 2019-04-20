using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager Instance;

    #region Properties
    public WorldConfig WorldConfig { get; private set; }
    public PlayerConfig PlayerConfig { get; private set; }
    #endregion
    

    #region ExposedVaraibles
    [SerializeField] private string _worldConfigName;
    [SerializeField] private string _playerConfigName;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _initialPoolSize = 10;
    #endregion

    #region PrivateVariables
    #endregion

    public delegate void GameStarted();
    public static event GameStarted OnGameStarted;


    private void Awake()
    {
        SetupSingleton();
        LoadConfigs();
    }

    private void SetupSingleton()
    {
        if (Instance != null)
            throw new Exception("There is more than one singleton in the scene!");

        Instance = this;
    }

    private void LoadConfigs()
    {
        WorldConfig = Resources.Load<WorldConfig>(_worldConfigName);
        PlayerConfig = Resources.Load<PlayerConfig>(_playerConfigName);
    }

    public void SetupPlatformSpawner()
    {
        var plarformSpawner = Instantiate(WorldConfig.PlatformSpawnerPrefab);
        plarformSpawner.GetComponent<PlatformSpawner>().Setup(WorldConfig.PlatformMinHorizontalDistance,
            WorldConfig.PlatformMaxHorizontalDistance, WorldConfig.PlatformMinVerticalDistance,
            WorldConfig.PlatformMaxVerticalDistance, new PlatformPool(WorldConfig.PlatformPrefabs, _initialPoolSize, WorldConfig.GreenPlatformJumpMultiplier, WorldConfig.RedPlatformJumpMultiplier, WorldConfig.BlackPlatformEffectTime));
    }

    public void SetupPlayer()
    {
        var player = Instantiate(PlayerConfig.PlayerPrefab, _spawnPoint);
        player.transform.SetParent(null);
        player.GetComponent<PlayerController>().Setup(PlayerConfig.InitialSpeed, PlayerConfig.SpeedPerSecond, PlayerConfig.JumpForce,PlayerConfig.GravityMultiplier, PlayerConfig.JumpTime, PlayerConfig.JumpKey);
        Camera.main.gameObject.GetComponent<CameraFollow>()._player = player.gameObject.transform;

        if(OnGameStarted != null)
            OnGameStarted();
    }

    //Called from endpoint cs
    public void EndGame()
    {
        Debug.Log("Game Over");
        _uiManager.GameOver();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Configs have been updated");
            LoadConfigs();
        }
    }
}
