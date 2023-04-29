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
    }

    public override void Deselect()
    {
        Color color = ColorController.Instance.ColdLink;
        color.a = _alpha;
        _lr.startColor = color;
    }

    public override void Select()
    {
        Color color = ColorController.Instance.Selection;
        color.a = _alpha;
        _lr.startColor = color;
    }
}
