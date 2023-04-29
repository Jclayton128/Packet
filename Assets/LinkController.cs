using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LinkController : MonoBehaviour
{
    public static LinkController Instance;

    public Action LinkBridgeDistanceChanged;

    //settings
    [SerializeField] LineRenderer _linePrefab = null;
    [SerializeField] float _linkBridgeDistance = 5f;

    //state
    List<LineRenderer> _links = new List<LineRenderer>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateAllLinks();
    }

    private void CreateAllLinks()
    {
        List<ServerLinkHandler> servers = new List<ServerLinkHandler>();
        foreach (var server in ServerController.Instance.Servers)
        {
            servers.Add(server.GetComponent<ServerLinkHandler>());
        }
        foreach (var server in servers)
        {
            CreateLinksAtServer(server, servers);
        }
    }

    private void CreateLinksAtServer(ServerLinkHandler testServer,
        List<ServerLinkHandler> possibleNeighbors)
    {
        float dist;
        foreach (var pn in possibleNeighbors)
        {
            dist = (testServer.transform.position - pn.transform.position).magnitude;
            if (dist < _linkBridgeDistance)
            {
                bool isNewConnection_t = testServer.CheckConnectWithNeighbor(pn);
                bool isNewConnection_p = pn.CheckConnectWithNeighbor(testServer);
                if (isNewConnection_t && isNewConnection_p)
                {
                    CreateNewLink(testServer, pn);
                }
            }
        }
    }

    private void CreateNewLink(ServerLinkHandler testServer, ServerLinkHandler pn)
    {
        LineRenderer newLink = Instantiate(_linePrefab);
        newLink.positionCount = 2;
        newLink.SetPosition(0, testServer.transform.position);
        newLink.SetPosition(1, pn.transform.position);
    }
}
