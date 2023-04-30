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

    public void EndTutorialMode()
    {
        _isTutorialMode = false;
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
            ServerController.Instance.SetHealTime(true);
        }
        else
        {
            ServerController.Instance.SetHealTime(false);
            var sm = StorylineController.Instance.PullNewGameMessage();
            UIController.Instance.Message.DisplayStoryMessage(sm);
            UIController.Instance.Packet.ShowPacketPanel();
            UIController.Instance.Tool.ShowToolPanel();
            PathController.Instance.CreateFirstPathProblem();
            ServerController.Instance.ApplyEncryptionToStartingServers();
        }
    }


}
