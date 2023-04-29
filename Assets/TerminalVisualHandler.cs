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
            _selection.color = ColorController.Instance.SelectableNode;
        }
        else
        {
            _selection.color = Color.clear;
        }
    }

    public override void Deselect()
    {
        _selection.color = Color.clear;
    }

    public override void Selectable()
    {
        _selection.color = ColorController.Instance.SelectableNode;
    }

    public override void Activate()
    {
        _base.color = ColorController.Instance.SelectedNode;
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
            _encryption.color = ColorController.Instance.Encryption;
        }
        else
        {
            _encryption.color = Color.clear;
        }

    }

    public void SetAsStartTerminal()
    {
        _base.color = ColorController.Instance.WarmNode;
    }

    public void SetAsEndTerminal()
    {
        _base.color = ColorController.Instance.TargetTerminal;
    }
}
