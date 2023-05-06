using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalVisualHandler : VisualHandler
{
    SelectionHandler _sh;

    [SerializeField] SpriteRenderer _base;
    [SerializeField] SpriteRenderer _encryption;
    [SerializeField] SpriteRenderer _selection;

    private void Awake()
    {
        _sh = GetComponent<SelectionHandler>();
    }

    private void Start()
    {
        _base.color = ColorController.Instance.ColdNode;
        SetEncryptionStatus(false);
        Deselect();
        Resetivate();
    }

    public override void Select()
    {
        if (_sh.CanBeSelected)
        {
            _selection.color = ColorController.Instance.SourceNode;
        }
        else
        {
            _selection.color = Color.clear;
        }
    }

    public override void Deselect()
    {
        if (_sh.CanBeSelected)
        {
            _selection.color = ColorController.Instance.SelectableRing;
        }
        else
        {
            //_selectionSprite.enabled = false;
            _selection.color = Color.clear;
        }
    }

    public override void Selectable()
    {
        _selection.enabled = true;
        _selection.color = ColorController.Instance.SelectableRing;
    }

    public override void Activate()
    {
        _base.color = ColorController.Instance.SourceNode;
    }

    public override void Deactivate()
    {
        if (_sh.HasBeenPreviouslyActivated)
        {
            _base.color = ColorController.Instance.WarmNode;
        }
        else
        {
            if (_sh.IsTargetNode)
            {
                _base.color = ColorController.Instance.TargetTerminal;
            }
            else
            {
                _base.color = ColorController.Instance.ColdNode;
            }
        }
    }

    public override void Resetivate()
    {
        _base.color = ColorController.Instance.ColdNode;        
    }


    public void SetEncryptionStatus(bool isEncrypted)
    {
        if (isEncrypted)
        {
            //_encryption.enabled = true;
            _encryption.color = ColorController.Instance.Encryption;
        }
        else
        {
            //_encryption.enabled = false;
            _encryption.color = Color.clear;
        }

    }

    public void SetAsStartTerminal()
    {
        _base.color = ColorController.Instance.SourceNode;
    }

    public void SetAsEndTerminal()
    {
        _base.color = ColorController.Instance.TargetTerminal;
    }

    public void EnableNonCoreVisual()
    {
        _encryption.enabled = true;
        _selection.enabled = true;
    }
}
