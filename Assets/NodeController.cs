using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public Action<NewNodeHandler> NodeBreak;

    public static NodeController Instance { get; private set; }
    
    //state
    List<NewNodeHandler> _allNodes = new List<NewNodeHandler>();
    public List<NewNodeHandler> Nodes => _allNodes;
    List<NewNodeHandler> _workingNodes = new List<NewNodeHandler>();

    private void Awake()
    {
        Instance = this;
        FindAllNodes();
    }

    private void Start()
    {
        GameController.Instance.RevisedGameStart += HandleRevisedGameStart;
    }

    private void FindAllNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        foreach (var node in nodes)
        {
            var nh = node.GetComponent<NewNodeHandler>();
            _allNodes.Add(nh);
            _workingNodes.Add(nh);
        }
    }

    private void HandleRevisedGameStart()
    {
        foreach (var node in _allNodes)
        {
            node.Initialize();
        }
    }

    public void ProcessBrokenNode(NewNodeHandler brokenNode)
    {
        if (_workingNodes.Contains(brokenNode))
        {
            _workingNodes.Remove(brokenNode);
        }
        NodeBreak?.Invoke(brokenNode);
    }
}
