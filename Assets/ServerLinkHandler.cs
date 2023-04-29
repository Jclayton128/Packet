using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLinkHandler : MonoBehaviour
{
    public List<ServerLinkHandler> _neighbors = new List<ServerLinkHandler>();

    /// <summary>
    /// Checks if this SLH already has a connection made with the neighbor. Returns FALSE 
    /// if it hasn't already connected.
    /// </summary>
    /// <param name="newNeighbor"></param>
    /// <returns></returns>
    public bool CheckConnectWithNeighbor(ServerLinkHandler newNeighbor)
    {
        if (!_neighbors.Contains(newNeighbor))
        {
            _neighbors.Add(newNeighbor);
            return false;
        }
        else return true;
    }
}
