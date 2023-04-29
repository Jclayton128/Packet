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

    public void SetEncryptionStatus(bool isEncrypted)
    {
        _encryptionStatus.color = ColorController.Instance.Encryption;
        _encryptionStatus.enabled = isEncrypted;
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
