using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    //state
    [SerializeField] bool _isTutorialMode;
    public bool IsTutorialMode => _isTutorialMode;

    [SerializeField] bool _isGameOver = false;
    public bool IsGameOver => _isGameOver;

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
        _isGameOver = false;
        ToolResourceController.Instance.GainResources(20);

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
            UIController.Instance.Resource.ShowResourcePanel();
            UIController.Instance.Endgame.HideEndgamePanel();
            PathController.Instance.CreateFirstPathProblem();
            ServerController.Instance.ApplyEncryptionToStartingServers();
        }
    }

    public void EnterGameOver()
    {
        _isGameOver = true;
        UIController.Instance.Message.HideMessage();
        UIController.Instance.Packet.HidePacketPanel();
        UIController.Instance.Tool.HideToolPanel();
        UIController.Instance.Resource.HideResourcePanel();
        FadeController.Instance.FadeOutEverything();

        Debug.Log("Game over!");
        //AUDIO play bong of doom;
        FadeController.Instance.FadeOutEverything();

        Invoke(nameof(ShowEndgamePanel_Delay),3f);
        //UIController.Instance.Endgame.ShowEndgamePanel();
    }

    private void ShowEndgamePanel_Delay()
    {
        UIController.Instance.Endgame.ShowEndgamePanel();
    }

}
