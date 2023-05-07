using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewNodeRenderer : MonoBehaviour, IFadeable
{
    [SerializeField] SpriteRenderer _selectionRing = null;
    [SerializeField] ParticleSystem _particleSystem = null;


    [Header("Base 3 Server")]
    [SerializeField] SpriteRenderer _base_3 = null;
    [SerializeField] SpriteRenderer[] _load_3 = null;

    [Header("Base 4 Server")]
    [SerializeField] SpriteRenderer _base_4 = null;
    [SerializeField] SpriteRenderer[] _load_4 = null;

    [Header("Base 5 Server")]
    [SerializeField] SpriteRenderer _base_5 = null;
    [SerializeField] SpriteRenderer[] _load_5= null;

    [Header("Base 6 Server")]
    [SerializeField] SpriteRenderer _base_6 = null;
    [SerializeField] SpriteRenderer[] _load_6 = null;

    //settings



    //state
    Tween _fadeTween_3;
    Tween _fadeTween_4;
    Tween _fadeTween_5;
    Tween _fadeTween_6;


    public void Start()
    {
        HideSelectionRing();

        _base_3.color = ColorController.Instance.ColdClear;
        _base_4.color = ColorController.Instance.ColdClear;
        _base_5.color = ColorController.Instance.ColdClear;
        _base_6.color = ColorController.Instance.ColdClear;
        foreach (var dot in _load_3)
        {
            dot.color = ColorController.Instance.UnloadedClear;
        }
        foreach (var dot in _load_4)
        {
            dot.color = ColorController.Instance.UnloadedClear;
        }
        foreach (var dot in _load_5)
        {
            dot.color = ColorController.Instance.UnloadedClear;
        }
        foreach (var dot in _load_6)
        {
            dot.color = ColorController.Instance.UnloadedClear;
        }

    }


    [ContextMenu("Fade in")]
    public void FadeIn(float fadeDuration)
    {
        _fadeTween_3.Kill();
        _fadeTween_4.Kill();
        _fadeTween_5.Kill();
        _fadeTween_6.Kill();
        _fadeTween_3 = _base_3.DOFade(1, fadeDuration);
        _fadeTween_4 = _base_4.DOFade(1, fadeDuration);
        _fadeTween_5 = _base_5.DOFade(1, fadeDuration);
        _fadeTween_6 = _base_6.DOFade(1, fadeDuration);

        foreach (var dot in _load_3)
        {
            dot.DOFade(1, fadeDuration);
        }
        foreach (var dot in _load_4)
        {
            dot.DOFade(1, fadeDuration);
        }
        foreach (var dot in _load_5)
        {
            dot.DOFade(1, fadeDuration);
        }
        foreach (var dot in _load_6)
        {
            dot.DOFade(1, fadeDuration);
        }



    }

    [ContextMenu("Fade out")]
    public void FadeOut(float fadeDuration)
    {
        _fadeTween_3.Kill();
        _fadeTween_4.Kill();
        _fadeTween_5.Kill();
        _fadeTween_6.Kill();
        _fadeTween_3 = _base_3.DOFade(0, fadeDuration);
        _fadeTween_4 = _base_4.DOFade(0, fadeDuration);
        _fadeTween_5 = _base_5.DOFade(0, fadeDuration);
        _fadeTween_6 = _base_6.DOFade(0, fadeDuration);

        foreach (var dot in _load_3)
        {
            dot.DOFade(0, fadeDuration);
        }
        foreach (var dot in _load_4)
        {
            dot.DOFade(0, fadeDuration);
        }
        foreach (var dot in _load_5)
        {
            dot.DOFade(0, fadeDuration);
        }
        foreach (var dot in _load_6)
        {
            dot.DOFade(0, fadeDuration);
        }
    }

    public void SetBase(int baseTier, Color baseColor)
    {
        _base_3.color = baseColor;
        _base_4.color = baseColor;
        _base_5.color = baseColor;
        _base_6.color = baseColor;

        switch (baseTier)
        {
            case 3:
                _base_3.gameObject.SetActive(true);
                _base_4.gameObject.SetActive(false);
                _base_5.gameObject.SetActive(false);
                _base_6.gameObject.SetActive(false);
                break;

            case 4:
                _base_3.gameObject.SetActive(false);
                _base_4.gameObject.SetActive(true);
                _base_5.gameObject.SetActive(false);
                _base_6.gameObject.SetActive(false);
                break;

            case 5:
                _base_3.gameObject.SetActive(false);
                _base_4.gameObject.SetActive(false);
                _base_5.gameObject.SetActive(true);
                _base_6.gameObject.SetActive(false);
                break;

            case 6:
                _base_3.gameObject.SetActive(false);
                _base_4.gameObject.SetActive(false);
                _base_5.gameObject.SetActive(false);
                _base_6.gameObject.SetActive(true);
                break;


        }

    }

    public void HideSelectionRing()
    {
        _selectionRing.color = Color.clear;
    }

    public void SetSelectionRing(Color ringColor)
    {
        _selectionRing.color = ringColor;
    }

    public void SetLoadDots(int baseTier, int dotsToIllumine, Color packetLoadColor)
    {
        switch (baseTier)
        {
            case 3:
                for (int i = 0; i < _load_3.Length; i++)
                {
                    if (i < dotsToIllumine)
                    {
                        _load_3[i].color = packetLoadColor;                        
                    }
                    else
                    {
                        _load_3[i].color = ColorController.Instance.UnloadedColor;
                    }
                }
                break;

            case 4:
                for (int i = 0; i < _load_4.Length; i++)
                {
                    if (i < dotsToIllumine)
                    {
                        _load_4[i].color = packetLoadColor;
                    }
                    else
                    {
                        _load_4[i].color = ColorController.Instance.UnloadedColor;

                    }
                }
                break;

            case 5:
                for (int i = 0; i < _load_5.Length; i++)
                {
                    if (i < dotsToIllumine)
                    {
                        _load_5[i].color = packetLoadColor;
                    }
                    else
                    {
                        _load_5[i].color = ColorController.Instance.UnloadedColor;
                    }
                }
                break;

            case 6:
                for (int i = 0; i < _load_6.Length; i++)
                {
                    if (i < dotsToIllumine)
                    {
                        _load_6[i].color = packetLoadColor;
                    }
                    else
                    {
                        _load_6[i].color = ColorController.Instance.UnloadedColor;

                    }
                }
                break;



        }
    }

}
