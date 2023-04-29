using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _messagePanel = null;
    [SerializeField] TextMeshProUGUI _messageTMP = null;

    public void DisplayMessage(string messageText)
    {
        _messagePanel.ShowHide(true);
        _messageTMP.text = messageText;
    }

    public void HideMessage()
    {
        _messagePanel.ShowHide(false);
    }
}
