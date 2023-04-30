using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PacketUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _packetPanel = null;
    [SerializeField] Image _timerArc = null;
    [SerializeField] Image _priorityArc = null;
    [SerializeField] Image _encryptionStatus = null;
    [SerializeField] Image _pulseDot = null;

    [SerializeField] Color _1Pri = Color.white;
    [SerializeField] Color _2Pri = Color.white;
    [SerializeField] Color _3Pri = Color.white;
    [SerializeField] Color _4Pri = Color.white;
    [SerializeField] Color _5Pri = Color.white;



    [SerializeField] float _pulseRate = 1;

    //state
    float _timeSinceNewPacket;
    bool _isPulseRising = true;
    Color _pulseColor = Color.clear;
    Color _pulseDim = Color.clear;
    float _pulseFactor;


    public void ShowPacketPanel()
    {
        _packetPanel.ShowHide(true);
    }

    public void HidePacketPanel()
    {
        _packetPanel.ShowHide(false);
    }

    public void ShowHideTimer(bool shouldShow)
    {
        _timerArc.enabled = shouldShow;
        _pulseDot.enabled = shouldShow;
    }
    public void ShowHideValue(bool shouldShow)
    {
        _priorityArc.enabled = shouldShow;
    }
    public void ShowHideEncryption(bool shouldShow)
    {
        _encryptionStatus.enabled = shouldShow;
    }

    public void SetEncryptionStatus(bool isEncrypted)
    {
        if (isEncrypted)
        {
            _encryptionStatus.color = ColorController.Instance.Encryption;
        }
        else
        {
            _encryptionStatus.color = Color.clear;
        }
        //_encryptionStatus.enabled = isEncrypted;
    }

    public void SetPacketValue(int value)
    {
        if (value == 1)
        {
            _priorityArc.fillAmount = .2f;
            _priorityArc.color = _1Pri;
        }
        if (value == 2)
        {
            _priorityArc.fillAmount = .4f;
            _priorityArc.color = _2Pri;
        }
        if (value == 3)
        {
            _priorityArc.fillAmount = .6f;
            _priorityArc.color = _3Pri;
        }
        if (value == 4)
        {
            _priorityArc.fillAmount = .8f;
            _priorityArc.color = _4Pri;
        }
        if (value == 5)
        {
            _priorityArc.fillAmount = 1f;
            _priorityArc.color = _5Pri;
        }
        _pulseFactor = 0;
        _timeSinceNewPacket = 0;
        _isPulseRising = true;
        _pulseColor = _priorityArc.color;
        _pulseDim = _pulseColor;
        _pulseDim.a = 0.2f;
    }

    public void SetPacketTiming(float factor)
    {
        _timerArc.fillAmount = factor;
    }

    public void ClearPacketPanel()
    {
        SetPacketTiming(0);
        SetEncryptionStatus(false);
        SetPacketValue(0);
        _pulseColor = Color.clear;
        _pulseDim = Color.clear;
        _pulseFactor = 0;
    }

    private void Update()
    {
        UpdatePulse();
    }

    private void UpdatePulse()
    {
        if (_isPulseRising)
        {
            _timeSinceNewPacket += Time.deltaTime;
            _pulseFactor = _timeSinceNewPacket / _pulseRate;
            _pulseDot.color = Color.Lerp(_pulseDot.color, _pulseColor, _pulseFactor);
            if (_timeSinceNewPacket >= _pulseRate)
            {
                _isPulseRising = false;
            }
        }
        else
        {
            _timeSinceNewPacket -= Time.deltaTime;
            _pulseFactor = _timeSinceNewPacket / _pulseRate;
            _pulseDot.color = Color.Lerp(_pulseDim, _pulseDot.color, _pulseFactor);
            if (_timeSinceNewPacket <= 0)
            {
                _isPulseRising = true;
            }
        }
    }
}
