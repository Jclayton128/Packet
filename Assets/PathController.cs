using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    public static PathController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateFirstPathProblem()
    {
        LinkController.Instance.ResetivateAllLinks();
        ServerController.Instance.ResetivateAllServers();
        TerminalController.Instance.ResetivateAllTerminals();
        TerminalController.Instance.CreateRandomStartGoalPair();
        PacketController.Instance.GenerateRandomPacket();
    }

    public void CreateNewPathProblem( )
    {
        LinkController.Instance.ResetivateAllLinks();
        ServerController.Instance.ResetivateAllServers();
        TerminalController.Instance.ResetivateAllTerminals();

        if (GameController.Instance.IsTutorialMode)
        {
            NewPathDelayedItems();
        }
        else
        {
            UIController.Instance.Message.ClearStoryMessageButKeepPanel();
            float delay = UnityEngine.Random.Range(1, 3f);
            Invoke(nameof(NewPathDelayedItems), delay);
        }

    }

    private void NewPathDelayedItems()
    {
        if (!GameController.Instance.IsTutorialMode)
        {
            var sm = StorylineController.Instance.AdvanceToNextStoryMessage();
            UIController.Instance.Message.DisplayStoryMessage(sm);
        }

        TerminalController.Instance.CreateRandomStartGoalPair();
        PacketController.Instance.GenerateRandomPacket();
    }
}
