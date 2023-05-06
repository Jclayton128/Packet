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
    ParticleSystem _ps;

    //settings
    [SerializeField] int _startingBaseTier = 3;


    //state
    public bool IsSelectable;
    bool _isWarm;
    private int _baseTier;
    int _currentLoad;

    private void Awake()
    {
        _nr = GetComponent<NewNodeRenderer>();
        _lh = GetComponent<NewLinkHandler>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }


    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _currentLoad = 0;
        _baseTier = _startingBaseTier;
        IsSelectable = false;
        _isWarm = false;
        _nr.SetBase(_baseTier, ColorController.Instance.ColdClear);
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
            _nr.SetSelectionRing(ColorController.Instance.SourceNode);
        }
    }
    public void HandleMouseExit()
    {
        _lh.DeselectAllLinks();
        if (IsSelectable)
        {           
            _nr.SetSelectionRing(ColorController.Instance.SelectableRing);
        }
        else
        {
            _nr.HideSelectionRing();
        }

    }
    public void HandleMouseDown()
    {
        if (IsSelectable)
        {
            NodeController.Instance.HandleNodeActivation(this);
        }
    }

    #endregion


    #region Node Status Changers
    public void SetNodeAsUnselectable()
    {
        IsSelectable = false;
        _nr.HideSelectionRing();
    }

    public void SetNodeAsSelectable()
    {
        if (_isWarm) return;
        IsSelectable = true;
        _nr.SetSelectionRing(ColorController.Instance.SelectableRing);
    }


    public void ActivateNodeAsSource()
    {
        _lh.DeselectAllLinks();
        SetNodeAsUnselectable();
        _nr.SetBase(_baseTier, ColorController.Instance.SourceNode);
        _ps.Play();
        _lh.ActivateLink(NodeController.Instance.PreviousSourceNode);
        //illumine link to previous source node
    }

    public void ActivateNodeAsTarget()
    {
        _nr.SetBase(_baseTier, ColorController.Instance.TargetTerminal);
    }

    public void ActivateNodeAsWarm()
    {
        Debug.Log($"{name} is warm");
        _isWarm = true;
        _ps.Stop();
        SetNodeAsUnselectable();
        _nr.SetBase(_baseTier, ColorController.Instance.WarmNode);
    }

    public void DeactivateNode()
    {
        _isWarm = false;
        SetNodeAsUnselectable();
        _nr.SetBase(_baseTier, ColorController.Instance.ColdNode);
        _lh.DeactivateAllLinks();
        _ps.Stop();
    }

    private void BreakNode()
    {
        NodeController.Instance.ProcessBrokenNode(this);
        SetNodeAsUnselectable();
        _nr.SetBase(_baseTier, ColorController.Instance.BrokenNode);
        _nr.FadeOut(1f);
        _lh.FadeOutAllLinks();
        _lh.DeactivateAllLinks();
        _ps.Stop();
    }

    #endregion

    public List<NewNodeHandler> ProvideListOfNeighbors()
    {
        return _lh.Neighbors;
    }



}
