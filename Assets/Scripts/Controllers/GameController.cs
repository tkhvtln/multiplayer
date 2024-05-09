using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour
{
    public bool IsGame { get; private set; }

    private UIController _uiController;
    private SaveController _saveController;
    private SoundController _soundController;
    private MultiplayerController _multiplayerController;

    private Spawner _spawner;

    private bool _isSceneLoaded;

    [Inject]
    private void Construct(UIController uiController, SaveController saveController, SoundController soundController, MultiplayerController multiplayerController, Spawner spawner)
    {
        _uiController = uiController;
        _saveController = saveController;
        _soundController = soundController;
        _multiplayerController = multiplayerController;
        _spawner = spawner;

        _saveController.Load();
        //_soundController.Init();
        //_spawnerController.Init();
        //_uiController.Init();

        _uiController.ShowPanelMenu();
    }

    public void Game() 
    {
        IsGame = true;
        LoadCurrentLevel();
    }

    public void Win()
    {
        IsGame = false;
        _uiController.ShowPanelWin();
    }

    public void Defeat() 
    {
        IsGame = false;
        _uiController.ShowPanelDefeat();
    }

    public void LoadCurrentLevel() 
    {
        UnloadScene();
        LoadScene();
    }

    public void LoadNextLevel() 
    {
        UnloadScene();

        _saveController.data.level = ++_saveController.data.level >= SceneManager.sceneCountInBuildSettings ? 1 : _saveController.data.level;
        _saveController.Save();

        LoadScene();
    }

    public void UnloadScene()
    {
        if (_isSceneLoaded)
        {
            _isSceneLoaded = false;
            SceneManager.UnloadSceneAsync(_saveController.data.level);
        }

        _spawner.DestroyPlayer();
    }

    public void LoadScene()
    {
        if (!_isSceneLoaded)
        {
            _isSceneLoaded = true;
            SceneManager.LoadSceneAsync(_saveController.data.level, LoadSceneMode.Additive);
        }

        _spawner.SpawnPlayer();
        _uiController.ShowPanelGame();
    }
}
