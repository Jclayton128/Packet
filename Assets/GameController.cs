using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [ContextMenu("Start Game")]
    public void StartGame()
    {
        UIController.Instance.Message.DisplayMessage("Hi!");
        UIController.Instance.Packet.ShowPacketPanel();
        PathController.Instance.CreateNewPathProblem();
    }


}
