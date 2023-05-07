using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLinkHandler : MonoBehaviour
{
    NewNodeHandler _nh;

    [SerializeField] List<NewNodeHandler> _neighbors = new List<NewNodeHandler> ();
    public List<NewNodeHandler> Neighbors => _neighbors;
    public List<NewLinkRenderer> _links = new List<NewLinkRenderer>();

    //settings
    [SerializeField] LineRenderer _linkPrefab = null;

    private void Awake()
    {
        _nh = GetComponent<NewNodeHandler>();
    }

    private void Start()
    {
        NodeController.Instance.NodeBreak += HandleNodeBreak;
    }

    private void HandleNodeBreak(NewNodeHandler brokenNode)
    {
        if (brokenNode == _nh) return;
        if (_neighbors.Contains(brokenNode))
        {
            Debug.Log($"{name} is affected by broken node");
            ConnectWithWorkingNeighborNodes();
        }
    }

    public void ConnectWithWorkingNeighborNodes()
    {
        if (_nh.IsBroken) return;

        DestroyExistingLinks();
        _links.Clear();
        _neighbors.Clear();
        float dist = 0;
        foreach (var node in NodeController.Instance.WorkingNodes)
        {
            if (this == node) continue;
            if (node.IsBroken) continue;
            dist = (transform.position - node.transform.position).magnitude;
            if (dist < NodeController.Instance.MaxPairDistance && dist > Mathf.Epsilon)
            {
                _neighbors.Add(node);
                CreateNewLink(node);
            }
        }
    }

    private void DestroyExistingLinks()
    {
        for (int i = _links.Count - 1; i > 0; i--)
        {
            Destroy(_links[i].gameObject);
        }
    }

    private void CreateNewLink(NewNodeHandler neighbor)
    {
        LineRenderer newLink = Instantiate(_linkPrefab, transform);

        newLink.positionCount = 2;
        newLink.SetPosition(0, transform.position);
        newLink.SetPosition(1, neighbor.transform.position);
        newLink.startColor = ColorController.Instance.ColdClear;
        newLink.endColor = ColorController.Instance.ColdClear;


        NewLinkRenderer link = newLink.GetComponent<NewLinkRenderer>();
        _links.Add(link);
        link.ConnectedNode = neighbor;
        Debug.Log($"{name} linking with {neighbor.name}");
    }


    public void SelectAllLinks()
    {
        foreach (var link in _links)
        {
            link.SelectPossibleLink();
        }
    }

    public void DeselectAllLinks()
    {
        foreach (var link in _links)
        {
            link.DeselectPossibleLink();
        }
    }
    
    public void ActivateLink(NewNodeHandler nextNode)
    {
        foreach (var link in _links)
        {
            if (link.ConnectedNode == nextNode)
            {
                link.ActivateLink();
            }
        }
    }

    public void DeactivateAllLinks()
    {
        foreach (var link in _links)
        {
            link.DeactivateLink();
        }
    }

    public void FadeInAllLinks()
    {
        foreach (var link in _links)
        {
            link.FadeInLink();
        }
    }

    public void FadeOutAllLinks()
    {
        foreach (var link in _links)
        {
            link.FadeOutLink();
        }
    }


}
