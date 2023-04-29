using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class TitleUIC : MonoBehaviour
{
    [SerializeField] RectTransform _titleTMP;
    [SerializeField] RectTransform _newgameButton;
    [SerializeField] RectTransform _creditsButton;
    [SerializeField] Vector3 _titleSlowPoint;
    [SerializeField] Vector3 _titleRestPoint;
    [SerializeField] Vector3 _newgameRestPoint;
    [SerializeField] Vector3 _creditsRestPoint;
    [SerializeField] float _swoopDuration = 2f;

    //state
    Tween _titleTween;
    Tween _newGameTween;
    Tween _creditsTween;

    private void Start()
    {
        ActivateStart();
    }

    [ContextMenu("Swoop Start")]
    public void ActivateStart()
    {
        FadeController.Instance.InstafadeAll();
        SwoopInTitle();
        SwoopInButtons();
    }

    private void SwoopInButtons()
    {
        _newGameTween.Kill();
        _newGameTween = _newgameButton.DOAnchorPos(_newgameRestPoint, _swoopDuration);

        _creditsTween.Kill();
        _creditsTween = _creditsButton.DOAnchorPos(_creditsRestPoint, _swoopDuration);
    }

    private void SwoopInTitle()
    {
        _titleTween.Kill();
        //_swoopTween = _titleTMP.DOMove(_titleRestPoint, _swoopDuration);
        _titleTween = _titleTMP.DOAnchorPos(_titleRestPoint, _swoopDuration).
            SetEase(Ease.OutSine);
         //_titleTMP.DOAnchorPos(_titleRestPoint, _swoopDuration).
         //   SetDelay(_swoopDuration*.5f);
    }
}
