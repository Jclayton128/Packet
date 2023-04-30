using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkConnectionHandler : MonoBehaviour
{
    private LinkHandler _node_0;
    private LinkHandler _node_1;

    //state
    public bool HasBeenActivated;
    
    private void Awake()
    {
        _node_0 = null;
        _node_1 = null;
    }

    public void SetOneNode(LinkHandler node)
    {
        if (_node_0 == null)
        {
            _node_0 = node;
        }
        else if (_node_1 == null)
        {
            _node_1 = node;
        }
        else
        {
            Debug.LogWarning("Both nodes filled!");
        }
    }

    public bool CheckConnection(LinkHandler testNode0, LinkHandler testNode1)
    {
        if (_node_0 == null || _node_1 == null)
        {
            Debug.Log("Missing a node reference");
            return false;
        }

        if (_node_0 == testNode0 && _node_1 == testNode1)
        { 
            return true; 
        }
        if (_node_0 == testNode1 && _node_1 == testNode0)
        {
            return true;
        }
        else return false;
    }

    public void ActivateLink()
    {
        HasBeenActivated = true;
        BroadcastMessage("Activate");
    }

    public void ResetivateLink()
    {
        HasBeenActivated = false;
        GetComponent<LinkVisualHandler>().HasFadedIn = true;
        BroadcastMessage("Resetivate");
    }

}
