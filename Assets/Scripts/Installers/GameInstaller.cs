using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;

    [Space]
    [SerializeField] private SaveController _saveController; 
    [SerializeField] private SoundController _soundController;
    [SerializeField] private MultiplayerController _multiplayerController;

    [Space]
    [SerializeField] private Spawner _spawner;
    [SerializeField] private CameraFollow _cameraFollow;

    public override void InstallBindings()
    {
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();

        Container.Bind<SaveController>().FromInstance(_saveController).AsSingle().NonLazy();
        Container.Bind<SoundController>().FromInstance(_soundController).AsSingle().NonLazy();      
        Container.Bind<MultiplayerController>().FromInstance(_multiplayerController).AsSingle().NonLazy();

        Container.Bind<Spawner>().FromInstance(_spawner).AsSingle().NonLazy();
        Container.Bind<CameraFollow>().FromInstance(_cameraFollow).AsSingle().NonLazy();
    }
}