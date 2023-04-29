using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        UIController.Instance.Message.DisplayMessage("Hi!");
    }

    [ContextMenu("Generate Packet")]
    public void GeneratePacket()
    {
        UIController.Instance.Packet.ShowPacketPanel();
        UIController.Instance.Packet.SetEncryptionStatus(true);
        UIController.Instance.Packet.SetPacketTiming(0.5f);
        UIController.Instance.Packet.SetPacketValue(5);
    }
}
