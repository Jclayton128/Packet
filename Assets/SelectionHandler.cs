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
            StartActivation();
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


    public void StartActivationRemotely()
    {
        if (TutorialController.Instance.IsInTutorialPair && !_isTutorialServer) return;
        StartActivation();
    }

    private void StartActivation()
    {
        IsActivated = true;
        CanBeSelected = false;
        HasBeenPreviouslyActivated = true;

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
