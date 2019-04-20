using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private Transform _player;
    private bool _gameStarted = false;

    private void OnEnable()
    {
        GameManager.OnGameStarted += FindPlayer;
    }

    private void OnDisable()
    {
        GameManager.OnGameStarted -= FindPlayer;
    }

    private void FindPlayer()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _gameStarted = true;
    }

    private void Update()
    {
        if (_gameStarted)
            transform.position = new Vector3(_player.transform.position.x, this.transform.position.y, _player.transform.position.z * 1000.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.EndGame();
            collision.gameObject.SetActive(false);
        }
    }

}
