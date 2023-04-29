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
}
