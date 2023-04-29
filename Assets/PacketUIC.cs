using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PacketUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _packetPanel = null;
    [SerializeField] Image _timerArc = null;
    [SerializeField] Image _encryptionStatus = null;
    [SerializeField] TextMeshProUGUI _packetValueTMP = null;

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
    }
    public void ShowHideValue(bool shouldShow)
    {
        _packetValueTMP.enabled = shouldShow;
    }
    public void ShowHideEncryption(bool shouldShow)
    {
        _encryptionStatus.enabled = shouldShow;
    }

    public void SetEncryptionStatus(bool isEncrypted)
    {
        _encryptionStatus.color = ColorController.Instance.Encryption;
        //_encryptionStatus.enabled = isEncrypted;
    }

    public void SetPacketValue(int value)
    {
        _packetValueTMP.text = value.ToString();
    }

    public void SetPacketTiming(float factor)
    {
        _timerArc.fillAmount = factor;
    }
}
