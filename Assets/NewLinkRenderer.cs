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

    public void FadeInLink(float duration)
    {
        float rand = UnityEngine.Random.Range(0, 1.5f);
        //DeselectPossibleLink();

        //Tween this later
        Color clear = ColorController.Instance.ColdClear;
        clear.a = 0;
        Color2 clear2 = new Color2(clear, clear);

        Color cold = ColorController.Instance.ColdLink;
        Color2 cold2 = new Color2(cold, cold);
        _lr.DOColor(clear2, cold2, duration).SetDelay(rand);
    }

    public void FadeOutLink()
    {
        //Tween this later
        _lr.sortingOrder = 0;
        _lr.enabled = false;
        _lr.startColor = ColorController.Instance.ColdClear;
        _lr.endColor = ColorController.Instance.ColdClear;
        _lph.Stop();
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
