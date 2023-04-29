using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalVisualHandler : VisualHandler
{
    [SerializeField] SpriteRenderer _base;
    [SerializeField] SpriteRenderer _encryption;
    [SerializeField] SpriteRenderer _selection;

    private void Start()
    {
        _base.color = ColorController.Instance.ColdTerminal;
        SetEncryptionStatus(false);
    }

    public override void Select()
    {
        _selection.color = ColorController.Instance.Selection;
    }

    public override void Deselect()
    {
        _selection.color = Color.clear;
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
        _base.color = ColorController.Instance.StartTerminal;
    }

    public void SetAsEndTerminal()
    {
        _base.color = ColorController.Instance.EndTerminal;
    }
}
