using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewLinkRenderer : MonoBehaviour
{
    LineParticleHandler _lph;
    LineRenderer _lr;

    //settings
    [SerializeField] float _fadeDuration = 1f;

    //state
    public NewNodeHandler ConnectedNode;
    Tween _colorTween;

    private void Awake()
    {
        _lph = GetComponentInChildren<LineParticleHandler>();
        _lr = GetComponent<LineRenderer>();
    }

    public void SelectPossibleLink()
    {
        _lr.sortingOrder = 1;
        _lr.startColor = ColorController.Instance.SelectedLink;
        _lr.endColor = ColorController.Instance.ColdLink;
    }

    public void DeselectPossibleLink()
    {
        _lr.sortingOrder = 0;
        _lr.startColor = ColorController.Instance.ColdLink;
        _lr.endColor = ColorController.Instance.ColdLink;
    }

    public void ActivateLink()
    {
        _lr.sortingOrder = 1;
        _lr.startColor = ColorController.Instance.WarmLink;
        _lr.endColor = ColorController.Instance.WarmLink;
        _lph.Play();
    }

    public void DeactivateLink()
    {
        _lr.sortingOrder = 0;
        _lr.startColor = ColorController.Instance.ColdLink;
        _lr.endColor = ColorController.Instance.ColdLink;
        _lph.Stop();
    }
}
