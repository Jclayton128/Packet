using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkHandler : MonoBehaviour
{
    public List<LinkHandler> _neighborServers = new List<LinkHandler>();
    public List<LinkHandler> _neighborTerminals = new List<LinkHandler>();
    public List<LinkVisualHandler> _links = new List<LinkVisualHandler>();

    private void Awake()
    {
        _neighborServers.Clear();
        _neighborTerminals.Clear();
    }

    /// <summary>
    /// Checks if this SLH already has a connection made with the neighbor. Returns FALSE 
    /// if it hasn't already connected.
    /// </summary>
    /// <param name="newNeighbor"></param>
    /// <returns></returns>
    public bool CheckConnectWithNeighborServer(LinkHandler newNeighbor)
    {
        if (!_neighborServers.Contains(newNeighbor))
        {
            _neighborServers.Add(newNeighbor);
            return false;
        }
        else return true;
    }

    public bool CheckConnectWithNeighborTerminal(LinkHandler newNeighbor)
    {
        if (!_neighborTerminals.Contains(newNeighbor))
        {
            _neighborTerminals.Add(newNeighbor);
            return false;
        }
        else return true;
    }

    public void AddLink(LinkVisualHandler lvh)
    {
        if (!_links.Contains(lvh))
        {
            _links.Add(lvh);
        }
    }

    public void Select()
    {
        SelectLinks();
    }

    public void Deselect()
    {
        DeselectLinks();
    }

    private void SelectLinks()
    {
        foreach (var link in _links)
        {
            link.Select();
        }
    }

    private void DeselectLinks()
    {
        foreach (var link in _links)
        {
            link.Deselect();
        }
    }

    public void ForgetAllNeighbors()
    {
        _neighborServers.Clear();
        _neighborTerminals.Clear();
    }
}
