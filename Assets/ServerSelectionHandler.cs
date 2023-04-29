using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSelectionHandler : MonoBehaviour
{
    ServerVisualHandler _svh;

    private void Awake()
    {
        _svh = GetComponent<ServerVisualHandler>();
    }

    private void OnMouseOver()
    {
        _svh.Select();
    }

    private void OnMouseDown()
    {
        GetComponent<ServerLoadHandler>().ImposeLoad();
    }

    private void OnMouseExit()
    {
        _svh.Deselect();
    }


}
