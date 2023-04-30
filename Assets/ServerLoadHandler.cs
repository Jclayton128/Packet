using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLoadHandler : MonoBehaviour
{
    public Action<ServerLoadHandler> ServerActivated;
    public enum LoadStatus { NoLoad, Low, Mid, High, Broken}

    ServerVisualHandler _svh;

    //settings
    [SerializeField] int _startingMaxLoad = 2;
    [SerializeField] float _timeRequiredToHealLoadDamageUnit = 5f;
    [SerializeField] bool _startEncryptionStatus = false;

    //state
    int _currentMaxLoad;
    float _timeToHealLoadDamageUnit;
    int _currentLoad;
    bool _currentEncryptionStatus;
    public bool HasEncryption => _currentEncryptionStatus;
    LoadStatus _currentLoadStatus;
    public bool IsBroken;//{ get; private set; }

    private void Awake()
    {
        _currentMaxLoad = _startingMaxLoad;
        _timeToHealLoadDamageUnit = 0;
        _currentLoad = 0;
        _currentEncryptionStatus = false;
        IsBroken = false;
        _svh = GetComponent<ServerVisualHandler>();
    }

    private void Start()
    {
        Resetivate();
        _svh.DepictMaxLoad(_startingMaxLoad);
        _svh.SetEncryptionStatus(_currentEncryptionStatus);
        PushVisuals();
    }

    private void Update()
    {
        UpdateHealLoad();
    }

    private void UpdateHealLoad()
    {
        if (_currentLoad > 0 && !IsBroken && Time.time >= _timeToHealLoadDamageUnit)
        {
            _currentLoad--;
            _currentLoad = Mathf.Clamp(_currentLoad, 0, 99);
            _timeToHealLoadDamageUnit = Time.time + _timeRequiredToHealLoadDamageUnit;
            _currentLoadStatus = DetermineLoadStatus();
            if (_currentLoadStatus == LoadStatus.Broken) IsBroken = true;
            PushVisuals();
        }
    }

    private void PushVisuals()
    {
        if (IsBroken) _svh.DepictBrokenStatus();
        //else  _svh.Deactivate();

        _svh.DepictLoadStatus(_currentLoadStatus, _currentLoad, _currentMaxLoad);
    }

    private LoadStatus DetermineLoadStatus()
    {
        int delta = _currentMaxLoad - _currentLoad;
        if (delta > 2)
        {
            return LoadStatus.Low;
        }
        else if (delta >= 1)
        {
            return LoadStatus.Mid;
        }
        else if (delta == 0)
        {
            return LoadStatus.High;
        }
        else
        {
            //Debug.Log("Server Break!");
            return LoadStatus.Broken;
        }
    }


    public void Activate()
    {
        ServerActivated?.Invoke(this);
        _currentLoad++;
        _timeToHealLoadDamageUnit = Time.time + _timeRequiredToHealLoadDamageUnit;
        _currentLoadStatus = DetermineLoadStatus();
        if (_currentLoadStatus == LoadStatus.Broken) IsBroken = true;
        PushVisuals();
    }

    public bool CheckIfBroken()
    {
        return IsBroken;
    }

    public void Resetivate()
    {
        _currentMaxLoad = _startingMaxLoad;
        _currentLoad = 0;
        _timeToHealLoadDamageUnit = 0;
        _currentLoadStatus = LoadStatus.Low;
        _currentEncryptionStatus = _startEncryptionStatus;
        //_isBroken = false;
        PushVisuals();
    }

    [ContextMenu("Repair")]
    public void RepairServer()
    {
        IsBroken = false;
        _currentLoad = 0;
        _timeToHealLoadDamageUnit = 0;
        _currentLoadStatus = DetermineLoadStatus();
        //TODO force a reselection if 
        BroadcastMessage("Deactivate");
        _svh.DepictLoadStatus(_currentLoadStatus, _currentLoad, _currentMaxLoad);
    }

    [ContextMenu("Encrypt")]
    public void EncryptServer()
    {
        _currentEncryptionStatus = true;
        _svh.SetEncryptionStatus(_currentEncryptionStatus);
    }

    [ContextMenu("Capacity")]
    public void IncreaseServerMaxLoad()
    {
        _currentMaxLoad++;
        _currentMaxLoad = Mathf.Clamp(_currentMaxLoad, 0, 8);
        _timeToHealLoadDamageUnit = 0;
        _currentLoadStatus = DetermineLoadStatus();
        _svh.DepictMaxLoad(_currentMaxLoad);
        PushVisuals();
    }

    public bool CheckIfCanRepair()
    {
        return IsBroken;
    }

    public bool CheckIfCanEncrypt()
    {
        return !_currentEncryptionStatus;
    }

    public bool CheckIfCanIncreaseMaxLoad()
    {
        if (_currentMaxLoad < 8) return true;
        else return false;
    }
}
