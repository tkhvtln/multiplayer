using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

public class PanelMultiplayer : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameRoomJoin;
    [SerializeField] private TMP_InputField _nameRoomCreate;

    [Space]
    [SerializeField] private GameObject _objInput;
    [SerializeField] private TextMeshProUGUI _textStatus;

    [Inject] private MultiplayerController _multiplayerController;

    public void Init()
    {

    }

    public void Show()
    {
        gameObject.SetActive(true);

        _objInput.SetActive(true);
        _textStatus.gameObject.SetActive(false);

        _nameRoomJoin.text = "";
        _nameRoomCreate.text = "";
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void JoinRoom()
    {
        if (_nameRoomJoin.text.Length > 0)
            _multiplayerController.JoinRoom(_nameRoomJoin.text);
    }

    public void CreateRoom()
    {
        if (_nameRoomCreate.text.Length > 0)
            _multiplayerController.CreateRoom(_nameRoomCreate.text);
    }

    public void ShowInput()
    {
        _objInput.SetActive(true);
        _textStatus.gameObject.SetActive(false);
    }

    public void ShowStatus(string status)
    {
        _textStatus.text = status;

        _textStatus.gameObject.SetActive(true);
        _objInput.SetActive(false);
    }

    public async UniTaskVoid ShowStatusThenHide(string status, int milliseconds = 1000)
    {
        ShowStatus(status);
        await UniTask.Delay(milliseconds);
        ShowInput();
    }
}
