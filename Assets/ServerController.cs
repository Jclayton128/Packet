using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerController : MonoBehaviour
{
    public static ServerController Instance;

    [SerializeField] ServerLoadHandler[] _startingEncryptedServers = null;

    /// <summary>
    /// First Arg: previous SLH, second Arg: new SLH
    /// </summary>
    public Action<SelectionHandler, SelectionHandler> NodeActivated;

    //state
    List<ServerLoadHandler> _servers = new List<ServerLoadHandler>();
    public List<ServerLoadHandler> Servers => _servers;

    public SelectionHandler _previousActivatedNode;
    public SelectionHandler _currentActivatedNode;

    private void Awake()
    {
        _previousActivatedNode = null;
        _currentActivatedNode = null;
        Instance = this;
        FindAndConnectAllServers();
    }

    private void Start()
    {
        ConnectToAllNodes();
    }

    private void FindAndConnectAllServers()
    {
        GameObject[] servers = GameObject.FindGameObjectsWithTag("Server");
        foreach (var server in servers)
        {
            var slh = server.GetComponent<ServerLoadHandler>();
            _servers.Add(slh);
        }
    }

    private void ConnectToAllNodes()
    {
        foreach (var terminal in TerminalController.Instance.Terminals)
        {
            SelectionHandler sh = terminal.GetComponent<SelectionHandler>();
            sh.NodeActivated += HandleActivatedNode;
        }
        foreach (var server in ServerController.Instance.Servers)
        {
            SelectionHandler sh = server.GetComponent<SelectionHandler>();
            sh.NodeActivated += HandleActivatedNode;
        }
    }

    private void HandleActivatedNode(SelectionHandler newlyActivatedNode)
    {
        if (_currentActivatedNode == null)
        {
            _currentActivatedNode = newlyActivatedNode;
        }
        else
        {
            _previousActivatedNode = _currentActivatedNode;
            _currentActivatedNode = newlyActivatedNode;
            NodeActivated?.Invoke(_previousActivatedNode, _currentActivatedNode);
        }
    }

    public void ResetivateAllServers()
    {
        foreach (var server in _servers)
        {
            server.GetComponent<SelectionHandler>().StartResetivate();
        }
    }

    public void ApplyEncryptionToStartingServers()
    {
        foreach (var server in _startingEncryptedServers)
        {
            server.EncryptServer();
        }
    }

    public void RecheckCurrentActivatedNodesNeighbors()
    {
        if (_currentActivatedNode)
        {
            _currentActivatedNode.StartActivationRemotely(false);
        }
    }
}
