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

    private void Awake()
    {
        Instance = this;
        _message = GetComponent<MessageUIC>();
        _packet = GetComponent<PacketUIC>();
    }

    private void Start()
    {
        _message.HideMessage();
        _packet.HidePacketPanel();
    }
}
