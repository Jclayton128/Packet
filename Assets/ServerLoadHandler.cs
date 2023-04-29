using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLoadHandler : MonoBehaviour
{
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
    LoadStatus _currentLoadStatus;

    private void Awake()
    {
        _currentMaxLoad = _startingMaxLoad;
        _currentLoad = 0;
        _timeToHealLoadDamageUnit = 0;
        _currentLoadStatus = LoadStatus.Low;
        _currentEncryptionStatus = _startEncryptionStatus;
        _svh = GetComponent<ServerVisualHandler>();
    }

    private void Start()
    {
        _svh.DepictMaxLoad(_startingMaxLoad);
        _svh.SetEncryptionStatus(_currentEncryptionStatus);
    }

    private void Update()
    {
        UpdateHealLoad();
    }

    private void UpdateHealLoad()
    {
        if (Time.time >= _timeToHealLoadDamageUnit)
        {
            _currentLoad--;
            _currentLoad = Mathf.Clamp(_currentLoad, 0, 99);
            _timeToHealLoadDamageUnit = Time.time + _timeRequiredToHealLoadDamageUnit;
            PushVisuals();
        }
    }

    private void PushVisuals()
    {
        _currentLoadStatus = DetermineLoadStatus();
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
            Debug.Log("Server Break!");
            return LoadStatus.Broken;
        }
    }

    [ContextMenu("Impose Load")]
    public void ImposeLoad()
    {
        _currentLoad++;
        _timeToHealLoadDamageUnit = Time.time + _timeRequiredToHealLoadDamageUnit;
        PushVisuals();
    }
}
