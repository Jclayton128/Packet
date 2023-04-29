using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    MessageUIC _message;
    public MessageUIC Message => _message;

    PacketUIC _packet;
    public PacketUIC Packet => _packet;

    TitleUIC _title;
    public TitleUIC Title => _title;

    ToolUIC _tool;
    public ToolUIC Tool => _tool;

    private void Awake()
    {
        Instance = this;
        _message = GetComponent<MessageUIC>();
        _packet = GetComponent<PacketUIC>();
        _tool = GetComponent<ToolUIC>();
    }

    private void Start()
    {
        _message.HideMessage();
        _packet.HidePacketPanel();
        _tool.HideToolPanel();
    }
}
