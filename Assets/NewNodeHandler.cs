using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNodeHandler : MonoBehaviour
{
    /// <summary>
    /// White implies that this and another regular packet can be taken without breaking.
    /// Yellow implies that this packet can be taken, but another regular could not be taken.
    /// Red implies that this packet will break the server if taken.
    /// </summary>
    //public enum NodeLoad { White, Yellow, Red}

    NewNodeRenderer _nr;
    NewLinkHandler _lh;

    //settings
    [SerializeField] int _startingBaseTier = 3;


    //state
    public bool IsSelectable = true;
    private int _baseTier;
    int _currentLoad;

    private void Awake()
    {
        _nr = GetComponent<NewNodeRenderer>();
        _lh = GetComponent<NewLinkHandler>();
    }


    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _currentLoad = 0;
        _baseTier = _startingBaseTier;
        IsSelectable = false;
        _nr.SetBase(_baseTier);
        _nr.SetLoadDots(_baseTier, _currentLoad, ColorController.Instance.UnloadedClear);
        _nr.HideSelectionRing();
        _lh.ConnectWithNeighborNodes();
        //_lh.DeselectAllLinks();
        Invoke(nameof(MasterFadeIn),1f);

    }

    public void MasterFadeIn()
    {
        _nr.FadeIn(1f);
        _lh.FadeInAllLinks();
    }

    public void MasterFadeOut()
    {
        _nr.FadeOut(1f);
        _lh.FadeOutAllLinks();
    }




    #region Mouse Handlers

    public void HandleMouseOver()
    {
        if (IsSelectable)
        {
            _lh.SelectAllLinks();
        }
    }
    public void HandleMouseExit()
    {
        _lh.DeselectAllLinks();
    }
    public void HandleMouseDown()
    {
        if (IsSelectable)
        {
            //handle being activated
            _lh.DeselectAllLinks();
        }
    }

    #endregion



}
