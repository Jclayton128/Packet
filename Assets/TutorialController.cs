using System;
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

            ProcessSpecialThings(_currentPacketMessage.SpecialThing);
        }
        else
        {
            Debug.Log("Tut complete");
        }
    }

    private void ProcessSpecialThings(PacketMessage.SpecialThings currentPacketMessage)
    {
        switch (currentPacketMessage)
        {
            case PacketMessage.SpecialThings.None:
                break;

            case PacketMessage.SpecialThings.ShowTimer:
                //fragile! must be called before other packet panel showings
                UIController.Instance.Packet.ShowPacketPanel();                
                UIController.Instance.Packet.ShowHideTimer(true);
                UIController.Instance.Packet.ShowHideValue(false);
                UIController.Instance.Packet.ShowHideEncryption(false);
                break;

            case PacketMessage.SpecialThings.ShowValue:
                UIController.Instance.Packet.ShowHideValue(true);
                break;

            case PacketMessage.SpecialThings.ShowEncryption:
                UIController.Instance.Packet.ShowHideEncryption(true);
                break;

            case PacketMessage.SpecialThings.SetupStartTutorialTerminals:
                TerminalController.Instance.SetStartTutorialTerminalAsGreen();
                break;

            case PacketMessage.SpecialThings.SetupTargetTutorialTerminals:
                TerminalController.Instance.SetTargetTutorialTerminalAsBlue();
                break;
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
