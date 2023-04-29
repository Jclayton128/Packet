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
    [SerializeField] Vector3 _titleExitPoint;
    [SerializeField] Vector3 _newGameExitPoint;
    [SerializeField] Vector3 _creditsExitPoint;
    [SerializeField] float _swoopDuration = 2f;

    //state
    Tween _titleTween;
    Tween _newGameTween;
    Tween _creditsTween;

    private void Start()
    {
        SwoopInStart();
    }

    public void SwoopInStart()
    {
        FadeController.Instance.InstafadeAll();
        SwoopInTitle();
        SwoopInButtons();
    }

    public void SwoopOutStart()
    {
        SwoopOutTitle();
        SwoopOutButtons();
    }

    private void SwoopOutTitle()
    {
        _titleTween.Kill();
        _titleTween = _titleTMP.DOAnchorPos(_titleExitPoint, _swoopDuration).
            SetEase(Ease.InSine);
    }

    private void SwoopOutButtons()
    {
        _newGameTween.Kill();
        _newGameTween = _newgameButton.DOAnchorPos(_newGameExitPoint, _swoopDuration).
            SetEase(Ease.InSine);

        _creditsTween.Kill();
        _creditsTween = _creditsButton.DOAnchorPos(_creditsExitPoint, _swoopDuration).
            SetEase(Ease.InSine);
    }

    private void SwoopInButtons()
    {
        _newGameTween.Kill();
        _newGameTween = _newgameButton.DOAnchorPos(_newgameRestPoint, _swoopDuration).
            SetEase(Ease.OutSine);

        _creditsTween.Kill();
        _creditsTween = _creditsButton.DOAnchorPos(_creditsRestPoint, _swoopDuration).
            SetEase(Ease.OutSine);
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
