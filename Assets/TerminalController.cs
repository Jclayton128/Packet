using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalController : MonoBehaviour
{
    public static TerminalController Instance;
    public Action TargetTerminalActivated;
    [SerializeField] SelectionHandler _tutorialStartTerminal = null;
    [SerializeField] SelectionHandler _tutorialTargetTerminal = null;

    //state
    List<TerminalLoadHandler> _terminals = new List<TerminalLoadHandler>();
    public List<TerminalLoadHandler> Terminals => _terminals;

    public SelectionHandler _startTerminal;
    public SelectionHandler _targetTerminal;

    private void Awake()
    {
        Instance = this;
        _targetTerminal = null;
        _startTerminal = null;
        FindAllTerminals();
    }

    private void FindAllTerminals()
    {
        GameObject[] terminals = GameObject.FindGameObjectsWithTag("Terminal");
        foreach (var terminal in terminals)
        {
            _terminals.Add(terminal.GetComponent<TerminalLoadHandler>());
            terminal.GetComponent<SelectionHandler>().NodeActivated += HandleTerminalActivated;
        }
    }

    private void HandleTerminalActivated(SelectionHandler obj)
    {
        if (obj == _targetTerminal)
        {
            TargetTerminalActivated?.Invoke();
            Debug.Log("success!");
            PathController.Instance.CreateNewPathProblem();
        }
    }

    public void CreateRandomStartGoalPair()
    {
        foreach (var term in _terminals)
        {
            term.GetComponent<SelectionHandler>().StartResetivate();
        }

        int rand = UnityEngine.Random.Range(0, _terminals.Count);
        var start = _terminals[rand];
        SelectionHandler sh = start.GetComponent<SelectionHandler>();
        sh.StartActivationRemotely();
        _startTerminal = sh;

        int breaker = 10;
        TerminalLoadHandler goal;
        do
        {
            int rand1 = UnityEngine.Random.Range(0, _terminals.Count);
            goal = _terminals[rand1];
            breaker--;
            if (breaker <= 0)
            {
                Debug.LogWarning("Breaker!");
                break;
            }
        }
        while (goal == start);

        if (goal != null)
        {
            SelectionHandler sh1 = goal.GetComponent<SelectionHandler>();
            sh1.SetAsTargetNode();
            _targetTerminal = sh1;
        }
    }


    public void ResetivateAllTerminals()
    {
        _startTerminal = null;
        _targetTerminal = null;
        foreach (var terminal in _terminals)
        {
            terminal.GetComponent<SelectionHandler>().StartResetivate();
        }
    }

    #region Tutorial Support
    public void SetupTutorialPair()
    {
        //foreach (var term in _terminals)
        //{
        //    term.GetComponent<SelectionHandler>().StartResetivate();
        //}

        var start = _tutorialStartTerminal;
        SelectionHandler sh = start.GetComponent<SelectionHandler>();
        sh.StartActivationRemotely();
        _startTerminal = sh;

        _targetTerminal = _tutorialTargetTerminal.GetComponent<SelectionHandler>();
        _targetTerminal.SetAsTargetNode();
    }

    #endregion
}
