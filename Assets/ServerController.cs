using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerController : MonoBehaviour
{
    public static ServerController Instance;

    //state
    List<ServerLoadHandler> _servers = new List<ServerLoadHandler>();
    public List<ServerLoadHandler> Servers => _servers;

    private void Awake()
    {
        Instance = this;
        FindAllServers();
    }

    private void FindAllServers()
    {
        GameObject[] servers = GameObject.FindGameObjectsWithTag("Server");
        foreach (var server in servers)
        {
            _servers.Add(server.GetComponent<ServerLoadHandler>());
        }
    }


}
