using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisualHandler : VisualHandler
{
    LineRenderer _lr;
    LinkConnectionHandler _lch;

    //settings
    [SerializeField] float _deselectAlpha = 0.15f;
    [SerializeField] float _selectAlpha = 1f;
    public bool HasFadedIn;

    private void Awake()
    {
        HasFadedIn = false;
        _lr = GetComponent<LineRenderer>();
        _lch = GetComponent<LinkConnectionHandler>();
        //Deselect();
        //Resetivate();
    }

    public override void Deselect()
    {
        if (HasFadedIn && !_lch.HasBeenActivated)
        {
            Color color = ColorController.Instance.ColdLink;
            color.a = _deselectAlpha;
            _lr.startColor = color;
            _lr.endColor = color;
        }
    }

    public override void Select()
    {
        if (HasFadedIn && !_lch.HasBeenActivated)
        {
            Color color = ColorController.Instance.SelectedLink;
            color.a = _selectAlpha;
            _lr.startColor = color;
            //_lr.endColor = color;
        }
    }

    public override void Activate()
    {
        Color color = ColorController.Instance.WarmLink;
        color.a = _selectAlpha;
        _lr.startColor = color;
        _lr.endColor = color;
    }

    public override void Deactivate()
    {
        //Color color = ColorController.Instance.WarmLink;
        //color.a = _selectAlpha;
        //_lr.startColor = color;
        //_lr.endColor = color;
    }

    public override void Resetivate()
    {
        Color color = ColorController.Instance.ColdLink;
        color.a = _deselectAlpha;
        _lr.startColor = color;
        _lr.endColor = color;
    }

    public override void Selectable()
    {
        // no selectable for links
    }
}
