using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    //state
    public bool CanBeSelected;// { get; private set; }
    bool _isSelected;
    public bool IsActivated;// { get; private set; }
    public bool HasBeenPreviouslyActivated;// { get; private set; }

    private void Awake()
    {
        CanBeSelected = false;
        _isSelected = false;
         IsActivated = false;
        HasBeenPreviouslyActivated = false;
    }

    private void Start()
    {
        StartResetivate();
    }

    public void ToggleSelectability(bool canBeSelected)
    {
        CanBeSelected = canBeSelected;
        if (HasBeenPreviouslyActivated) CanBeSelected = false;
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
        if (CanBeSelected)
        {
            _isSelected = true;
            BroadcastMessage("Select");
        }
    }

    private void OnMouseDown()
    {
        if (_isSelected)
        {
            StartActivation();
        }
    }

    public void StartResetivate()
    {
        HasBeenPreviouslyActivated = false;
        StopActivation();
    }

    public void StopActivation()
    {
        IsActivated = false;
        CanBeSelected = false;
        BroadcastMessage("Deactivate");
    }

    public void StartActivationRemotely()
    {
        StartActivation();
    }

    private void StartActivation()
    {
        IsActivated = true;
        CanBeSelected = false;
        HasBeenPreviouslyActivated = true;
        BroadcastMessage("Activate");
        //Visually depict that now activated
        //Tell neighbors to all become selectable and not activated

        //GetComponent<ServerLoadHandler>()?.ImposeLoad();
    }

    private void OnMouseExit()
    {
        _isSelected = false;
        BroadcastMessage("Deselect");
    }
}
