using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerVisualHandler : MonoBehaviour
{

    
    [SerializeField] SpriteRenderer _baseSprite = null;
    [SerializeField] SpriteRenderer _selectionSprite = null;
    [SerializeField] SpriteRenderer[] _loadSprites = null;

    private void Start()
    {
        Deselect();
    }

    public void Select()
    {
        _selectionSprite.color = ColorController.Instance.HighlightColor;
    }

    public void Deselect()
    {
        _selectionSprite.color = Color.clear;
    }

    public void DepictMaxLoad(int maxCount)
    {
        SetLoadColor(ColorController.Instance.UnloadedColor, 0,  maxCount);
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
}
