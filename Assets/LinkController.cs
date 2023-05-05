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
    List<LinkConnectionHandler> _links = new List<LinkConnectionHandler>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindAllServers();
        FindAllTerminals();
        CreateAllLinks();
        ServerController.Instance.NodeActivated += HandleServerConnection;
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

        newLink.positionCount = 2;
        newLink.SetPosition(0, testServer.transform.position);
        newLink.SetPosition(1, pn.transform.position);

        LinkConnectionHandler lch = newLink.GetComponent<LinkConnectionHandler>();
        lch.SetOneNode(testServer);
        lch.SetOneNode(pn);
        _links.Add(lch);

        testServer.AddLink(newLink.GetComponent<LinkVisualHandler>());
        pn.AddLink(newLink.GetComponent<LinkVisualHandler>());

        var fh = newLink.GetComponent<FadeHandler>();
        int phase_t = testServer.GetComponent<FadeHandler>().Phase;
        int phase_p = pn.GetComponent<FadeHandler>().Phase;
        int linePhase;
        if (phase_p < 3 && phase_t < 3) linePhase = 3;
        else linePhase = 5;
        fh.Phase = linePhase;
        FadeController.Instance.AddFadeHandler(fh);
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

    private void HandleServerConnection(SelectionHandler arg1, SelectionHandler arg2)
    {
        foreach (var link in _links)
        {
            if (link.CheckConnection(arg1.GetComponent<LinkHandler>(),
                arg2.GetComponent<LinkHandler>()))
            {
                link.ActivateLink();
            }
            

        }
    }

    public void ResetivateAllLinks()
    {
        foreach (var link in _links)
        {
            link.ResetivateLink();
        }
    }


}
