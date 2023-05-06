using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    public Action<NewNodeHandler> NodeBreak;

    public static NodeController Instance { get; private set; }

    //settings
    [SerializeField] float _minPairDistance = 2f;
    [SerializeField] float _maxPairDistance = 3f;
    public float MaxPairDistance => _maxPairDistance;


    //state
    List<NewNodeHandler> _allNodes = new List<NewNodeHandler>();
    public List<NewNodeHandler> Nodes => _allNodes;
    [SerializeField] List<NewNodeHandler> _workingNodes = new List<NewNodeHandler>();

    [SerializeField] NewNodeHandler _currentSourceNode;
    public NewNodeHandler CurrentSourceNode => _currentSourceNode;
    [SerializeField] NewNodeHandler _currentTargetNode;
    public NewNodeHandler CurrentTargetNode => _currentTargetNode;
    [SerializeField] NewNodeHandler _previousSourceNode;
    public NewNodeHandler PreviousSourceNode => _previousSourceNode;

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

    [ContextMenu("Create Pair")]
    public void CreateSourceTargetPair()
    {
        Debug.Log($"creating new pair");
        DeactivateAllNodes();

        _previousSourceNode = null;
        int rand = UnityEngine.Random.Range(0, _workingNodes.Count);
        _currentSourceNode = _workingNodes[rand];

        int breaker = 10;
        float dist;
        do
        {
            int randTgt = UnityEngine.Random.Range(0, _workingNodes.Count);
            _currentTargetNode = _workingNodes[randTgt];
            dist = (_currentSourceNode.transform.position - _currentTargetNode.transform.position).magnitude;
            breaker--;
            if (breaker <= 0)
            {
                Debug.LogWarning("breaker!");
                break;
            }
        }
        while (dist < _minPairDistance);

        ActivateCurrentSourceMode_NewPair();
        _currentTargetNode.ActivateNodeAsTarget();
    }

    private void DeactivateAllNodes()
    {
        //_currentSourceNode?.DeactivateNode();
        //_currentTargetNode?.DeactivateNode();
        foreach (var node in _workingNodes)
        {
            node.DeactivateNode();
        }
    }

    private void SetAllNodesUnselectable()
    {
        foreach (var node in _workingNodes)
        {
            node.SetNodeAsUnselectable();
        }
    }

    private void ActivateCurrentSourceMode_NewPair()
    {
        _currentSourceNode.ActivateNodeAsSource();
        foreach (var neighbor in _currentSourceNode.ProvideListOfNeighbors())
        {
            neighbor.SetNodeAsSelectable();
        }
    }

    private void ActivateCurrentSourceNode_ServerClick()
    {
        if (_previousSourceNode)
        {
            Debug.Log($"Warming previous source: {_previousSourceNode}");
            _previousSourceNode.ActivateNodeAsWarm();
        } 
        _currentSourceNode.ActivateNodeAsSource();
        foreach (var neighbor in _currentSourceNode.ProvideListOfNeighbors())
        {
            neighbor.SetNodeAsSelectable();
        }
    }

    public void HandleNodeActivation(NewNodeHandler selectedNode)
    {
        SetAllNodesUnselectable();
        if (_currentSourceNode) _previousSourceNode = _currentSourceNode;
        _currentSourceNode = selectedNode;
        ActivateCurrentSourceNode_ServerClick();



        if (selectedNode == _currentTargetNode)
        {
            HandleSuccessfulDelivery();
        }
        else
        {
            SoundController.Instance.PlayRandomActivation();
        }


    }

    private void HandleSuccessfulDelivery()
    {
        SoundController.Instance.PlayRandomCompletion();
        CreateSourceTargetPair();
    }
}
