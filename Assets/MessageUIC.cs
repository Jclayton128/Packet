using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MessageUIC : MonoBehaviour
{
    public Action MessagePanelClicked;

    [SerializeField] UIElementDriver _messagePanel = null;
    [SerializeField] TextMeshProUGUI _messageTMP = null;
    [SerializeField] TextMeshProUGUI _hintTMP = null;
    [SerializeField] Image _messageIcon = null;

    public void DisplayMessage(string messageText, Sprite sprite, string hintText)
    {
        _messagePanel.ShowHide(true);
        _messageTMP.text = messageText;
        if (sprite != null)
        {
            _messageIcon.sprite = sprite;
        }

        _hintTMP.text = hintText;
    }

    public void HideMessage()
    {
        _messagePanel.ShowHide(false);
    }

    public void HandleMessagePanelClicked()
    {
        MessagePanelClicked?.Invoke();
    }
}
