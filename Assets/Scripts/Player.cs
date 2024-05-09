using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private MeshRenderer _meshRenderer;
    
    private Transform _transform;
    private PhotonView _photonView;

    private GameController _gameController;
    private MultiplayerController _multiplayerController;

    public void Init(GameController gameController, MultiplayerController multiplayerController)
    {
        _transform = transform;
        _gameController = gameController;

        if (multiplayerController.IsMultiplayer)
        {
            _photonView = GetComponent<PhotonView>();
            _multiplayerController = multiplayerController;
            _meshRenderer.material.color = Random.ColorHSV(0f, 1f);
        }
    }

    private void Update()
    {
        if (_gameController == null || !_gameController.IsGame) return;

        if (_photonView != null)
            Move();
        else if (_multiplayerController == null)
            Move();

        DoGameOver();
    }

    private void Move()
    {
        Vector3 vecDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _transform.position += vecDirection * _speed * Time.deltaTime;
    }

    private void DoGameOver()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            if (_multiplayerController != null)
                _multiplayerController.GameOverMultiplayer(0);
            else
                _gameController.Defeat();
        }

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (_multiplayerController != null)
                _multiplayerController.GameOverMultiplayer(1);
            else
                _gameController.Win();
        }
    }
}
