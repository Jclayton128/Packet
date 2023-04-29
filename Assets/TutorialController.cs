using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance;

    [SerializeField] PacketMessage[] _tutorialMessages = null;

    //state
    int _currentTutorialStep;
    [SerializeField] PacketMessage _currentPacketMessage;
    [SerializeField]  PacketMessage.StepToAdvance _currentStepToAdvance;
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
        ServerController.Instance.NodeBridgeActivated +=
            HandlePossibleAdvancementViaServerActivation;

        TerminalController.Instance.TargetTerminalActivated +=
            HandlePossibleAdvancementViaPacketSuccess;

        UIController.Instance.Message.MessagePanelClicked +=
            HandlePossibleAdvancementViaClick;
    }

    public void StartTutorial()
    {
        _isInTutorialPair = true;
        //_currentTutorialStep++;
        //_currentPacketMessage = _tutorialMessages[_currentTutorialStep];
        //_currentStepToAdvance = _currentPacketMessage.stepToAdvance;
        //FadeController.Instance.IncrementFadeInPhase();
        //UIController.Instance.Message.DisplayMessage(
        //    _currentPacketMessage.Message,
        //    _currentPacketMessage.SendingImage,
        //    _currentPacketMessage.Hint);
        AdvanceTutorial();
    }

    public void AdvanceTutorial()
    {
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
            UIController.Instance.Message.DisplayMessage(
                _currentPacketMessage.Message,
                _currentPacketMessage.SendingImage,
                _currentPacketMessage.Hint);
        }
        else
        {
            Debug.Log("Tut complete");
        }
    }

    public void HandlePossibleAdvancementViaClick()
    {
        if (_currentStepToAdvance == PacketMessage.StepToAdvance.ClickMessagePanel)
        {
            AdvanceTutorial();
        }
    }

    public void HandlePossibleAdvancementViaServerActivation(object n, object m)
    {
        if (_currentStepToAdvance == PacketMessage.StepToAdvance.ActivateServer)
        {
            AdvanceTutorial();
        }
    }

    public void HandlePossibleAdvancementViaPacketSuccess()
    {
        if (_currentStepToAdvance == PacketMessage.StepToAdvance.TerminatePacket)
        {
            AdvanceTutorial();
            _isInTutorialPair = false;
            TerminalController.Instance.CreateRandomStartGoalPair();
        }
    }
}
