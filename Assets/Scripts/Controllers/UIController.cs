using UnityEngine;
using Zenject;
using Zenject.Asteroids;

public class UIController : MonoBehaviour
{
    private GameController _gameController;
    private SoundController _soundController;
    private MultiplayerController _multiplayerController;

    private PanelMenu _panelMenu;
    private PanelGame _panelGame;
    private PanelWin _panelWin;
    private PanelDefeat _panelDefeat;
    private PanelMultiplayer _panelMultiplayer;

    [Inject]
    private void Construct(PanelMenu panelMenu, PanelGame panelGame, PanelWin panelWin, PanelDefeat panelDefeat, PanelMultiplayer panelMultiplayer, 
                           GameController gameController, MultiplayerController multiplayerController, SoundController soundController) 
    {
        _panelMenu = panelMenu;
        _panelGame = panelGame;
        _panelWin = panelWin;
        _panelDefeat = panelDefeat;
        _panelMultiplayer = panelMultiplayer;

        _gameController = gameController;
        _soundController = soundController;
        _multiplayerController = multiplayerController;

        _panelMenu.Init();
        _panelGame.Init();
        _panelWin.Init();
        _panelDefeat.Init();
        _panelMultiplayer.Init();
    }

    public void ShowPanelMenu() 
    {
        Clear();
        _panelMenu.Show();
    }

    public void ShowPanelGame() 
    {
        Clear();
        _panelGame.Show();
    }

    public void ShowPanelWin() 
    {
        Clear();
        _panelWin.Show();
    }

    public void ShowPanelDefeat() 
    {
        Clear();
        _panelDefeat.Show();
    }

    public void ShowPanelMultiplayer() 
    {
        Clear();
        _panelMultiplayer.Show();
    }

    public void OnButtonPlay() 
    {
        _gameController.Game();
    }

    public void OnButtonNextLevel()
    {
        _gameController.LoadNextLevel();
    }

    public void OnButtonRestartLevel()
    {
        _gameController.LoadCurrentLevel();
    }

    public void OnButtonOpenMultiplayer()
    {
        ShowPanelMultiplayer();
        _multiplayerController.Connect();
    }

    public void OnButtonJoinRoom()
    {
        _panelMultiplayer.JoinRoom();
    }

    public void OnButtonCreateRoom()
    {
        _panelMultiplayer.CreateRoom();
    }

    public void OnButtonLeaveLevel()
    {
        if (_multiplayerController.IsMultiplayer)
        {
            ShowPanelMultiplayer();
            _gameController.UnloadScene();
            _multiplayerController.LeaveRoom();
        }
        else
        {
            ShowPanelMenu();
            _gameController.UnloadScene();
        }
    }

    public void OnButtonLeaveMultiplayer()
    {
        _multiplayerController.LeaveServer();
        ShowPanelMenu();
    }

    public void OnButtonSound()
    {
        _soundController.SwitchSound();
    }

    public void OnButtonExitGame()
    {
        Application.Quit();
    }

    public void Clear() 
    {
        _panelMenu.Hide();
        _panelGame.Hide();
        _panelWin.Hide();
        _panelDefeat.Hide();
        _panelMultiplayer.Hide();
    }
}
