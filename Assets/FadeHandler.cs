using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FadeHandler : MonoBehaviour
{
    //settings
    [SerializeField] float _fadeDuration = 1f;
    [SerializeField] float _minDelay = 0.5f;
    [SerializeField] float _maxDelay = 2f;
    [SerializeField] int _phase = 0;

    //state
    LineRenderer _lineRenderer;
    SpriteRenderer[] _spriteRenderers;
    Tween[] _srTweens;
    Color[] _startingColors;
    Tween _lineTween;

    private void Awake()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        _srTweens = new Tween[_spriteRenderers.Length];
        _startingColors = new Color[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            _startingColors[i] = _spriteRenderers[i].color;
        }
        _lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    public void FadeInCheck(int currentPhase)
    {
        if (currentPhase == _phase)
        {
            FadeIn(_fadeDuration);
        }
    }

    public void FadeOutCheck(int currentPhase)
    {
        if (currentPhase == _phase)
        {
            FadeOut(_fadeDuration);
        }
    }

    public void InstaFadeOut()
    {
        for (int i = 0; i < _srTweens.Length; i++)
        {
            _srTweens[i].Kill();
            Color col = new Color(
                _spriteRenderers[i].color.r,
                _spriteRenderers[i].color.g,
                _spriteRenderers[i].color.b, 0);
            _spriteRenderers[i].color = col;
        }
        if (_lineRenderer)
        {
            _lineRenderer.enabled = true;
            _lineTween.Kill();
            Color col = new Color(
                _lineRenderer.startColor.r,
                _lineRenderer.startColor.g,
                _lineRenderer.startColor.b, 0);
            _lineRenderer.startColor = col;
            _lineRenderer.endColor = col;
        }
    }


    private void FadeIn(float fadeDuration)
    {
        for (int i = 0; i < _srTweens.Length; i++)
        {
            _srTweens[i].Kill();
            float delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
            _srTweens[i] = _spriteRenderers[i].DOFade(1, fadeDuration).SetDelay(delay);
        }

        if (_lineRenderer)
        {
            _lineRenderer.enabled = true;
            _lineTween.Kill();
            Color2 col2_clear = new Color2(Color.clear, Color.clear);
            Color2 col2_coldLink = new Color2(ColorController.Instance.ColdLink,
                ColorController.Instance.ColdLink);
            _lineTween = _lineRenderer.DOColor(col2_clear, col2_coldLink, _fadeDuration);
        }

    }

    private void FadeOut(float fadeDuration)
    {
        for (int i = 0; i < _srTweens.Length; i++)
        {
            _srTweens[i].Kill();
            float delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
            _srTweens[i] = _spriteRenderers[i].DOFade(0, fadeDuration).SetDelay(delay);
        }
        if (_lineRenderer)
        {
            _lineRenderer.enabled = true;
            _lineTween.Kill();
            Color2 col2_clear = new Color2(Color.clear, Color.clear);
            Color2 col2_coldLink = new Color2(ColorController.Instance.ColdLink,
                ColorController.Instance.ColdLink);
            _lineTween = _lineRenderer.DOColor(col2_coldLink, col2_clear, _fadeDuration);
        }
    }
}
