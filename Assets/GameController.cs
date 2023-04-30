using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //state
    [SerializeField] bool _isTutorialMode;
    public bool IsTutorialMode => _isTutorialMode;

    private void Awake()
    {
        _isTutorialMode = true;
        Instance = this;
    }


    public void ToggleTutorialMode()
    {
        _isTutorialMode = !_isTutorialMode;
    }

    public void InitiateDelayedStartGame()
    {
        Invoke(nameof(StartGame), 2.5f);
    }

    public void StartGame()
    {
        if (_isTutorialMode)
        {
            TutorialController.Instance.StartTutorial();
        }
        else
        {
            UIController.Instance.Message.DisplayTutorialMessage("New Game!", null, "play!");
            UIController.Instance.Packet.ShowPacketPanel();
            UIController.Instance.Tool.ShowToolPanel();
            PathController.Instance.CreateNewPathProblem();
            ServerController.Instance.ApplyEncryptionToStartingServers();
        }
    }


}
