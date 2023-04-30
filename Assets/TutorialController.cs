using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    [SerializeField] TutorialMessage[] _tutorialMessages = null;

    //state
    int _currentTutorialStep;
    [SerializeField] TutorialMessage _currentPacketMessage;
    [SerializeField]  TutorialMessage.StepToAdvance _currentStepToAdvance;
    [SerializeField] bool _isInTutorialPair;
    public bool IsInTutorialPair => _isInTutorialPair;

    private void Awake()
    {
        Instance = this;
        _currentTutorialStep = -1;
        _currentPacketMessage = null;
        _isInTutorialPair = false;
    }

    private void Start()
    {
        ServerController.Instance.NodeActivated +=
            HandlePossibleAdvancementViaServerActivation;

        TerminalController.Instance.TargetTerminalActivated +=
            HandlePossibleAdvancementViaPacketSuccess;

        UIController.Instance.Message.MessagePanelClicked +=
            HandlePossibleAdvancementViaClick;
    }

    public void StartTutorial()
    {
        _isInTutorialPair = true;
        AdvanceTutorial();
    }

    public void AdvanceTutorial()
    {
        if (!GameController.Instance.IsTutorialMode) return;
        if (_currentTutorialStep < _tutorialMessages.Length - 1)
        {
            _currentTutorialStep++;
            _currentPacketMessage = _tutorialMessages[_currentTutorialStep];
            _currentStepToAdvance = _currentPacketMessage.stepToAdvance;
            if (_currentPacketMessage.InvokesTutorialPair)
            {
                TerminalController.Instance.SetupTutorialPair();
            }
            FadeController.Instance.IncrementFadeInPhase();
            UIController.Instance.Message.DisplayTutorialMessage(
                _currentPacketMessage.Message,
                _currentPacketMessage.SendingImage,
                _currentPacketMessage.Hint);

            ProcessSpecialThings(_currentPacketMessage.SpecialThing);
        }
        else
        {
            Debug.Log("Tut complete");
        }
    }

    private void ProcessSpecialThings(TutorialMessage.SpecialThings currentPacketMessage)
    {
        switch (currentPacketMessage)
        {
            case TutorialMessage.SpecialThings.None:
                break;

            case TutorialMessage.SpecialThings.ShowTimer:
                //fragile! must be called before other packet panel showings            
                UIController.Instance.Packet.ShowHideTimer(true); 
                break;

            case TutorialMessage.SpecialThings.ShowValue:
                UIController.Instance.Packet.ShowHideValue(true);
                break;

            case TutorialMessage.SpecialThings.ShowEncryption:
                UIController.Instance.Packet.ShowHideEncryption(true);
                UIController.Instance.Packet.SetEncryptionStatus(true);
                break;

            case TutorialMessage.SpecialThings.SetupStartTutorialTerminals:
                TerminalController.Instance.SetStartTutorialTerminalAsGreen();
                break;

            case TutorialMessage.SpecialThings.SetupTargetTutorialTerminals:
                TerminalController.Instance.SetTargetTutorialTerminalAsBlue();
                break;

            case TutorialMessage.SpecialThings.EncryptServers:
                ServerController.Instance.ApplyEncryptionToStartingServers();
                break;            
            
            case TutorialMessage.SpecialThings.ShowPacketPanel:
                UIController.Instance.Packet.ShowPacketPanel();
                UIController.Instance.Packet.ShowHideTimer(false);
                UIController.Instance.Packet.ShowHideValue(false);
                UIController.Instance.Packet.ShowHideEncryption(false);
                break;

            case TutorialMessage.SpecialThings.ShowToolPanel:
                UIController.Instance.Tool.ShowToolPanel();
                break;

            case TutorialMessage.SpecialThings.EndTutorial:
                _isInTutorialPair = false;
                GameController.Instance.EndTutorialMode();
                var sm = StorylineController.Instance.PullNewGameMessage();
                UIController.Instance.Message.DisplayStoryMessage(sm);
                UIController.Instance.Message.ClearStoryMessageButKeepPanel();
                ServerController.Instance.SetHealTime(false);
                break;

            case TutorialMessage.SpecialThings.ShowResourcePanel:
                UIController.Instance.Resource.ShowResourcePanel();
                break;
        }
    }

    public void HandlePossibleAdvancementViaClick()
    {
        if (!GameController.Instance.IsTutorialMode) return;
        if (_currentStepToAdvance == TutorialMessage.StepToAdvance.ClickMessagePanel)
        {
            AdvanceTutorial();
        }
    }

    public void HandlePossibleAdvancementViaServerActivation(object n, object m)
    {
        if (!GameController.Instance.IsTutorialMode) return;
        if (_currentStepToAdvance == TutorialMessage.StepToAdvance.ActivateServer)
        {
            AdvanceTutorial();
        }
    }

    public void HandlePossibleAdvancementViaPacketSuccess()
    {
        if (!GameController.Instance.IsTutorialMode) return;
        if (_currentStepToAdvance == TutorialMessage.StepToAdvance.TerminatePacket)
        {
            AdvanceTutorial();
            _isInTutorialPair = false;
            PathController.Instance.CreateNewPathProblem();
            //TerminalController.Instance.CreateRandomStartGoalPair();
        }
    }
}
