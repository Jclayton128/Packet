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
    List<LinkHandler> _servers = new List<LinkHandler>();
    List<LinkHandler> _terminals = new List<LinkHandler> ();
    List<LineRenderer> _links = new List<LineRenderer>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindAllServers();
        FindAllTerminals();
        CreateAllLinks();
    }

    private void FindAllServers()
    {
        foreach (var server in ServerController.Instance.Servers)
        {
            _servers.Add(server.GetComponent<LinkHandler>());
        }
    }

    private void FindAllTerminals()
    {
        foreach (var terminal in TerminalController.Instance.Terminals)
        {
            _terminals.Add(terminal.GetComponent<LinkHandler>());
        }
    }

    private void CreateAllLinks()
    {
        foreach (var server in _servers)
        {
            CreateLinksWithServer(server, _servers);
            CreateLinksWithTerminal(server);
        }
    }

    private void CreateLinksWithTerminal(LinkHandler server)
    {
        float dist = 0;
        foreach (var terminal in _terminals)
        {
            dist = (server.transform.position - terminal.transform.position).magnitude;
            if (dist < _linkBridgeDistance)
            {
                bool isNewConnection_1 = server.CheckConnectWithNeighborTerminal(terminal);

                bool isNewConnection_2 = terminal.CheckConnectWithNeighborServer(server);

                if (!isNewConnection_1 && !isNewConnection_2)
                {
                    CreateNewLink(server, terminal);
                }
            }
        }
    }

    private void CreateLinksWithServer(LinkHandler testServer,
        List<LinkHandler> possibleNeighbors)
    {
        float dist;
        foreach (var pn in possibleNeighbors)
        {
            dist = (testServer.transform.position - pn.transform.position).magnitude;
            if (dist < _linkBridgeDistance)
            {
                bool isNewConnection_t = testServer.CheckConnectWithNeighborServer(pn);
                bool isNewConnection_p = pn.CheckConnectWithNeighborServer(testServer);
                if (!isNewConnection_t && !isNewConnection_p)
                {
                    CreateNewLink(testServer, pn);
                }
            }
        }
    }

    private void CreateNewLink(LinkHandler testServer, LinkHandler pn)
    {
        LineRenderer newLink = Instantiate(_linePrefab);
        _links.Add(newLink);
        newLink.positionCount = 2;
        newLink.SetPosition(0, testServer.transform.position);
        newLink.SetPosition(1, pn.transform.position);

        testServer.AddLink(newLink.GetComponent<LinkVisualHandler>());
        pn.AddLink(newLink.GetComponent<LinkVisualHandler>());
    }


    [ContextMenu("Recheck Links")]
    public void RecheckLinks_Debug()
    {
        for (int i = _links.Count -1; i > 0; i--)
        {
            Destroy(_links[i].gameObject);
        }
        _links.Clear();
        foreach (var server in _servers)
        {
            server.ForgetAllNeighbors();
        }
        CreateAllLinks();
    }
}
