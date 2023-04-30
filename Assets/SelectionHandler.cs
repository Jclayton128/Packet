using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    public Action<SelectionHandler> NodeActivated;

    ServerLoadHandler _slh;

    //settings
    [SerializeField] bool _isTutorialServer = false;

    //state
    public bool CanBeSelected;// { get; private set; }
    bool _isSelected;
    public bool IsActivated;// { get; private set; }
    public bool HasBeenPreviouslyActivated;// { get; private set; }
    public bool IsTargetNode;
    

    private void Awake()
    {
        _slh = GetComponent<ServerLoadHandler>();
        CanBeSelected = false;
        _isSelected = false;
         IsActivated = false;
        HasBeenPreviouslyActivated = false;
        IsTargetNode = false;
    }

    private void Start()
    {
        StartResetivate();
    }

    public void ToggleSelectability(bool canBeSelected)
    {
        CanBeSelected = canBeSelected;

        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        if (HasBeenPreviouslyActivated) CanBeSelected = false;
        if (_slh && _slh.CheckIfBroken()) CanBeSelected = false;
        if (CanBeSelected)
        {
            GetComponent<VisualHandler>().Selectable();
        }
        else
        {
            GetComponent<VisualHandler>().Deselect();
        }
    }

    private void OnMouseOver()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        if (CanBeSelected)
        {
            _isSelected = true;
            BroadcastMessage("Select");
        }
    }

    private void OnMouseDown()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        if (_isSelected)
        {
            StartActivation(true);
        }
    }

    public void StartResetivate()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        HasBeenPreviouslyActivated = false;
        IsTargetNode = false;

        //TODO go through all links and Resetivate them.

        StopActivation();
    }

    public void StopActivation()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        IsActivated = false;
        CanBeSelected = false;
        BroadcastMessage("Deactivate");
    }

    internal void SetAsTargetNode()
    {
        IsTargetNode = true;
        GetComponent<TerminalVisualHandler>().SetAsEndTerminal();
    }


    public void StartActivationRemotely(bool checkEncryption)
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        StartActivation(checkEncryption);
    }

    private void StartActivation(bool checkEncryption)
    {
        IsActivated = true;
        CanBeSelected = false;
        HasBeenPreviouslyActivated = true;

        //Check if packet is encrypted and if this server is encrypted.
        //if mismatch, then roll the dice to lose or not.

        if (checkEncryption && _slh != null &&
            PacketController.Instance.GetPacketEncryption())
        {
            if ( _slh.HasEncryption)
            {
                //no problems
            }
            else
            {
                float roll = UnityEngine.Random.Range(0, 1f);
                if (roll <= PacketController.Instance.EncryptionPenaltyChance)
                {
                    //TODO show some kind of decryption-themed penalty effect here.
                    Debug.Log("Decrypted!!!");
                    PacketController.Instance.LoseCurrentPackage();
                }
            }
        }

        BroadcastMessage("Activate");
        NodeActivated?.Invoke(this);

    }

    private void OnMouseExit()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        _isSelected = false;
        BroadcastMessage("Deselect");
    }
}
