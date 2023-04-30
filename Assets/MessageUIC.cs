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

    [SerializeField] TextMeshProUGUI _toStatic = null;
    [SerializeField] TextMeshProUGUI _fromStatic = null;
    [SerializeField] TextMeshProUGUI _toDynamic = null;
    [SerializeField] TextMeshProUGUI _fromDynamic = null;
    [SerializeField] TextMeshProUGUI _bodyDynamic = null;



    public void DisplayTutorialMessage(string messageText, Sprite sprite, string hintText)
    {
        _messagePanel.ShowHide(true);
        _messageTMP.enabled = true;
        _hintTMP.enabled = true;

        _toStatic.enabled = false;
        _fromStatic.enabled = false;
        _toDynamic.enabled = false;
        _fromDynamic.enabled = false;
        _bodyDynamic.enabled = false;


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

    public void DisplayStoryMessage(StoryMessage message)
    {
        _messagePanel.ShowHide(true);
        _messageTMP.enabled = false;
        _hintTMP.enabled = false;

        _toStatic.enabled = true;
        _fromStatic.enabled = true;
        _toDynamic.enabled = true;
        _fromDynamic.enabled = true;
        _bodyDynamic.enabled = true;

        _toDynamic.text = message.To;
        _fromDynamic.text = message.From;
        _messageIcon.sprite = message.SenderImage;
        _bodyDynamic.text = message.Message;
    }

    public void ClearStoryMessageButKeepPanel()
    {
        _toDynamic.text = null;
        _fromDynamic.text = null;
        _messageIcon.sprite = null;
        _bodyDynamic.text = null;
    }
}
