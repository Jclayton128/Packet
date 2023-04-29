using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerVisualHandler : VisualHandler
{
    SelectionHandler _sh;
    ServerLoadHandler _slh;

    [SerializeField] SpriteRenderer _baseSprite = null;
    [SerializeField] SpriteRenderer _encryption = null;
    [SerializeField] SpriteRenderer _selectionSprite = null;
    [SerializeField] SpriteRenderer[] _loadSprites = null;

    private void Awake()
    {
        _slh = GetComponent<ServerLoadHandler>();
        _sh = GetComponent<SelectionHandler>();
    }

    private void Start()
    {
        Deselect();
        Resetivate();
    }

    public override void Select()
    {
        
        _selectionSprite.color = ColorController.Instance.SelectedNode;
    }

    public override void Deselect()
    {
        if (_sh.CanBeSelected)
        {
            _selectionSprite.color = ColorController.Instance.SelectableNode;
        }
        else
        {
            _selectionSprite.color = Color.clear;
        }
    }

    public override void Selectable()
    {

        _selectionSprite.color = ColorController.Instance.SelectableNode;
    }

    public override void Activate()
    {
        if (!_slh.IsBroken)
        {
            _baseSprite.color = ColorController.Instance.SelectedNode;
        }
        else
        {
            _baseSprite.color = ColorController.Instance.BrokenNode;
        }

    }

    public override void Deactivate()
    {
        if (_slh.IsBroken)
        {
            DepictBrokenStatus();
        }
        else if (_sh.HasBeenPreviouslyActivated)
        {
            _baseSprite.color = ColorController.Instance.WarmNode;
        }
        else
        {
            _baseSprite.color = ColorController.Instance.ColdNode;
        }

    }

    public override void Resetivate()
    {
        if (_slh.IsBroken)
        {
            _baseSprite.color = ColorController.Instance.BrokenNode;
        }
        else
        {
            _baseSprite.color = ColorController.Instance.ColdNode;
        }

    }


    public void DepictMaxLoad(int maxCount)
    {
        SetLoadColor(ColorController.Instance.UnloadedColor, 0,  maxCount);
    }

    public void DepictBrokenStatus()
    {
        _baseSprite.color = ColorController.Instance.BrokenNode;
    }

    public void DepictLoadStatus(ServerLoadHandler.LoadStatus status, int currentLoad,
        int maxLoad)
    {
        switch (status)
        {
            case ServerLoadHandler.LoadStatus.NoLoad:
                SetLoadColor(ColorController.Instance.UnloadedColor, 0, maxLoad);
                break;

            case ServerLoadHandler.LoadStatus.Low:
                SetLoadColor(ColorController.Instance.LoadedColor_Low, currentLoad, maxLoad);
                break;

            case ServerLoadHandler.LoadStatus.Mid:
                SetLoadColor(ColorController.Instance.LoadedColor_Mid, currentLoad, maxLoad);
                break;

            case ServerLoadHandler.LoadStatus.High:
                SetLoadColor(ColorController.Instance.LoadedColor_High, currentLoad, maxLoad);
                break;
        }
    }

    private void SetLoadColor(Color loadedColor, int currentCount, int maxCount)
    {
        for (int i = 0; i < _loadSprites.Length; i++)
        {
            if (i < currentCount)
            {
                _loadSprites[i].color = loadedColor;
            }
            else if (i < maxCount)
            {
                _loadSprites[i].color = ColorController.Instance.UnloadedColor;
            }
            else
            {
                _loadSprites[i].color = Color.clear;
            }
        }
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

}
