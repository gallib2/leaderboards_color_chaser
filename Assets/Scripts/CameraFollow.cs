using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{

    [HideInInspector] public Transform _player;
    [SerializeField] private float _smoothTimeX = 0.2f;
    [SerializeField] private float _smoothTimeY = 0.2f;
    private Vector3 _velocity;
    private bool _gameStarted = false;

    private void OnEnable()
    {
        GameManager.OnGameStarted += GameStarted;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= GameStarted;
    }

    private void GameStarted()
    {
        _gameStarted = true;
    }

    private void Update()
    {
        if (_gameStarted)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x, ref _velocity.x, _smoothTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y, ref _velocity.y, _smoothTimeY);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

}
