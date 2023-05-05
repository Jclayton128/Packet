using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSelectionHandler : MonoBehaviour
{
    NewNodeHandler _nh;

    private void Awake()
    {
        _nh = GetComponent<NewNodeHandler>();
    }

    private void OnMouseOver()
    {
        _nh.HandleMouseOver();
    }

    private void OnMouseExit()
    {
        _nh.HandleMouseExit();
    }

    private void OnMouseDown()
    {
        _nh.HandleMouseDown();
    }


}

