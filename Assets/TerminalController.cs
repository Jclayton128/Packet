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

    public void ActivateRandomTerminal()
    {
        Debug.Log("Activate random terminal");
        foreach (var term in _terminals)
        {
            term.GetComponent<SelectionHandler>().StartResetivate();
        }

        int rand = UnityEngine.Random.Range(0, _terminals.Count);
        var terminal = _terminals[rand];
        terminal.GetComponent<SelectionHandler>().StartActivationRemotely();
    }
}
