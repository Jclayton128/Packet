using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    //state
    bool _canBeSelected = true;
    public void ToggleSelectability(bool canBeSelected)
    {
        _canBeSelected = canBeSelected;
    }

    private void OnMouseOver()
    {
        if (!_canBeSelected) return;
        BroadcastMessage("Select");
    }

    private void OnMouseDown()
    {
        if (!_canBeSelected) return;
        GetComponent<ServerLoadHandler>()?.ImposeLoad();
    }

    private void OnMouseExit()
    {
        BroadcastMessage("Deselect");
    }
}
