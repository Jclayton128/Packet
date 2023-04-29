using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisualHandler : VisualHandler
{
    LineRenderer _lr;

    //settings
    [SerializeField] float _alpha = 0.5f;
    

    private void Awake()
    {
        _lr = GetComponent<LineRenderer>();
        Deselect();
        Resetivate();
    }

    public override void Deselect()
    {
        Color color = ColorController.Instance.ColdLink;
        color.a = _alpha;
        _lr.startColor = color;
    }

    public override void Select()
    {
        Color color = ColorController.Instance.SelectedLink;
        color.a = _alpha;
        _lr.startColor = color;
    }

    public override void Activate()
    {
        Color color = ColorController.Instance.Encryption;
        color.a = _alpha;
        _lr.startColor = color;
        _lr.endColor = color;
    }

    public override void Deactivate()
    {
        Color color = ColorController.Instance.WarmLink;
        color.a = _alpha;
        _lr.startColor = color;
        _lr.endColor = color;
    }

    public override void Resetivate()
    {
        Color color = ColorController.Instance.ColdLink;
        color.a = _alpha;
        _lr.startColor = color;
        _lr.endColor = color;
    }

    public override void Selectable()
    {
        // no selectable for links
    }
}
