using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player _playerPfefab;

    private Player _player;
    private CameraFollow _cameraFollow;
    private GameController _gameController;
    private MultiplayerController _multiplayerController;

    [Inject]
    private void Construct(GameController gameController, MultiplayerController multiplayerController, CameraFollow cameraFollow)
    {
        _gameController = gameController;
        _multiplayerController = multiplayerController;
        _cameraFollow = cameraFollow;
    }

    public void SpawnPlayer()
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        if (_multiplayerController.IsMultiplayer)
            _player = _multiplayerController.SpawnPlayer("Player", position, rotation).GetComponent<Player>();
        else
            _player = Instantiate(_playerPfefab, position, rotation);

        _player.transform.parent = transform;
        _player.Init(_gameController, _multiplayerController);

        _cameraFollow.SetTarget(_player.transform);
    }

    public void DestroyPlayer()
    {
        if (_player != null)
            Destroy(_player.gameObject);
    }
}
