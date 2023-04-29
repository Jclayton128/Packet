using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public static TerminalController Instance;

    //state
    List<TerminalLoadHandler> _terminals = new List<TerminalLoadHandler>();
    public List<TerminalLoadHandler> Terminals => _terminals;

    private void Awake()
    {
        Instance = this;
        FindAllTerminals();
    }

    private void FindAllTerminals()
    {
        GameObject[] terminals = GameObject.FindGameObjectsWithTag("Terminal");
        foreach (var terminal in terminals)
        {
            _terminals.Add(terminal.GetComponent<TerminalLoadHandler>());
        }
    }

    public void CreateRandomStartGoalPair()
    {
        Debug.Log("Creating Start Goal Pair");
        foreach (var term in _terminals)
        {
            term.GetComponent<SelectionHandler>().StartResetivate();
        }

        int rand = UnityEngine.Random.Range(0, _terminals.Count);
        var start = _terminals[rand];
        start.GetComponent<SelectionHandler>().StartActivationRemotely();

        int breaker = 10;
        TerminalLoadHandler goal;
        do
        {
            int rand1 = UnityEngine.Random.Range(0, _terminals.Count);
            goal = _terminals[rand1];
            breaker--;
            if (breaker <= 0) break;
        }
        while (goal == start);

        if (goal != null)
        {
            goal.GetComponent<SelectionHandler>().SetAsTargetNode();
        }
    }

    public void ResetivateAllTerminals()
    {
        foreach (var terminal in _terminals)
        {
            terminal.GetComponent<SelectionHandler>().StartResetivate();
        }
    }
}
