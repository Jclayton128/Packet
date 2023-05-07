using System;
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
    [SerializeField] int _loadDotsHealedPerDelivery = 1;

    //state
    public bool IsSelectable;
    [SerializeField] bool _isWarm;
    [SerializeField] bool _isBroken;
    public bool IsBroken => _isBroken;
    private int _baseTier;
    [SerializeField] int _currentLoad;

    private void Awake()
    {
        _nr = GetComponent<NewNodeRenderer>();
        _lh = GetComponent<NewLinkHandler>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        NodeController.Instance.PacketDelivered += HandlePacketDelivered;
    }


    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _currentLoad = 0;
        _baseTier = _startingBaseTier;
        IsSelectable = false;
        _isWarm = false;
        _isBroken = false;
        _nr.SetBase(_baseTier, ColorController.Instance.ColdClear);
        _nr.SetLoadDots(_baseTier, _currentLoad, ColorController.Instance.UnloadedClear);
        _nr.HideSelectionRing();
        _lh.ConnectWithWorkingNeighborNodes();
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
        if (_isBroken) return;
        if (_isWarm) return;
        IsSelectable = true;
        _nr.SetSelectionRing(ColorController.Instance.SelectableRing);
    }


    public void ActivateNodeAsSource(bool imposeCost)
    {
        if (imposeCost)
        {
            _currentLoad += NewPacketController.Instance.CurrentPacketSize;
            _currentLoad = Mathf.Clamp(_currentLoad, 0, 99);
        }

        if (_currentLoad > _baseTier)
        {
            _isBroken = true;
            _nr.SetBase(_baseTier, ColorController.Instance.BrokenNode);
        }
        else
        {
            _nr.SetBase(_baseTier, ColorController.Instance.SourceNode);
        }

        _lh.DeselectAllLinks();
        SetNodeAsUnselectable();
        _ps.Play();
        _lh.ActivateLink(NodeController.Instance.PreviousSourceNode);

        Color col = FindCurrentLoadColor();
        _nr.SetLoadDots(_baseTier, _currentLoad, col);


        //illumine link to previous source node
    }

    private Color FindCurrentLoadColor()
    {
        int remainingLoad = _currentLoad - NewPacketController.Instance.CurrentPacketSize;
        if (remainingLoad > NewPacketController.Instance.PacketSizeMax)
        {
            return ColorController.Instance.LoadedColor_Low;
        }
        else if (remainingLoad > 0)
        {
            return ColorController.Instance.LoadedColor_Mid;
        }
        else
        {
            return ColorController.Instance.LoadedColor_High;
        }
    }

    public void ActivateNodeAsTarget()
    {
        _nr.SetBase(_baseTier, ColorController.Instance.TargetTerminal);
    }

    public void ActivateNodeAsWarm()
    {
        SetNodeAsUnselectable();
        _ps.Stop();
        if (_isBroken)
        {
            //NodeController.Instance.ProcessBrokenNode(this);
            //_nr.SetBase(_baseTier, ColorController.Instance.BrokenNode);
        }
        else
        {
            _isWarm = true;
            _nr.SetBase(_baseTier, ColorController.Instance.WarmNode);
        }

    }

    public void DeactivateNode()
    {
        //_lh.DeactivateAllLinks();
        _ps.Stop();
        if (_isBroken)
        {

        }
        else
        {
            _isWarm = false;
            SetNodeAsUnselectable();
            _nr.SetBase(_baseTier, ColorController.Instance.ColdNode);
            _lh.DeactivateAllLinks();
        }

    }

    private void BreakNode()
    {
        _nr.FadeOut(1f);
        _lh.FadeOutAllLinks();
        //_lh.DeactivateAllLinks();
        _ps.Stop();
        NodeController.Instance.ProcessBrokenNode(this);
    }

    #endregion

    public List<NewNodeHandler> ProvideListOfNeighbors()
    {
        return _lh.Neighbors;
    }

    public void HandlePacketDelivered()
    {
        if (_isBroken)
        {
            BreakNode();
        }
        else
        {
            _currentLoad -= _loadDotsHealedPerDelivery;
            _currentLoad = Mathf.Clamp(_currentLoad, 0, 99);
            Color col = FindCurrentLoadColor();

            _nr.SetLoadDots(_baseTier, _currentLoad, col);
        }

    }

}
