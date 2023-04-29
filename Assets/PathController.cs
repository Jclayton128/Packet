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

    public void CreateNewPathProblem()
    {
        LinkController.Instance.ResetivateAllLinks();
        ServerController.Instance.ResetivateAllServers();
        TerminalController.Instance.ResetivateAllTerminals();
        TerminalController.Instance.CreateRandomStartGoalPair();
    }
}
