using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkHandler : MonoBehaviour
{
    public List<LinkHandler> _neighborServers = new List<LinkHandler>();
    public List<LinkHandler> _neighborTerminals = new List<LinkHandler>();
    public List<LinkVisualHandler> _links = new List<LinkVisualHandler>();

    //state
    VisualHandler _vh;

    private void Awake()
    {
        _vh = GetComponent<VisualHandler>();    
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
        if (newNeighbor != this && !_neighborServers.Contains(newNeighbor))
        {
            _neighborServers.Add(newNeighbor);
            return false;
        }
        else return true;
    }

    public bool CheckConnectWithNeighborTerminal(LinkHandler newNeighbor)
    {
        if (newNeighbor != this && !_neighborTerminals.Contains(newNeighbor))
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

    public void RefreshNeighborSelectability()
    {
        foreach (var neighbor in _neighborServers)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.ToggleSelectability(true); //if I am activated, all neighbors  become selectable
        }
        foreach (var neighbor in _neighborTerminals)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.ToggleSelectability(true);
        }
    }

    public void Activate()
    {
        foreach (var neighbor in _neighborServers)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.StopActivation(); //if I am activated, all neighbors must be deactivated
        }
        foreach (var neighbor in _neighborTerminals)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.StopActivation();
        }

        foreach (var neighbor in _neighborServers)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.ToggleSelectability(true); //if I am activated, all neighbors  become selectable
        }
        foreach (var neighbor in _neighborTerminals)
        {
            var sh = neighbor.GetComponent<SelectionHandler>();
            sh.ToggleSelectability(true);
        }
    }

    public void Deactivate()
    {
        //if (GetComponent<SelectionHandler>().IsActivated) return;
        foreach (var neighbor in _neighborServers)
        {
            neighbor.GetComponent<SelectionHandler>().ToggleSelectability(false);
        }
        foreach (var neighbor in _neighborTerminals)
        {
            neighbor.GetComponent<SelectionHandler>().ToggleSelectability(false);
        }
    }

}
