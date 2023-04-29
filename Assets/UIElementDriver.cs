using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIElementDriver : MonoBehaviour
{
    //state
    Image[] _images;
    TextMeshProUGUI[] _TMPs;
    Color[] _imageColors;
    Color[] _tmpColors;

    private void Awake()
    {
        _images = GetComponentsInChildren<Image>();
        _TMPs = GetComponentsInChildren<TextMeshProUGUI>();
        _imageColors = new Color[_images.Length];
        _tmpColors = new Color[_TMPs.Length];

        for (int i = 0; i < _images.Length; i++)
        {
            _imageColors[i] = _images[i].color;
        }
        for (int i = 0; i < _TMPs.Length; i++)
        {
            _tmpColors[i] = _TMPs[i].color;
        }
    }

    public void ShowHide(bool shouldBeShown)
    {
        if (!shouldBeShown)
        {
            foreach (var image in _images)
            {
                image.color = Color.clear;
            }
            foreach (var tmp in _TMPs)
            {
                tmp.color = Color.clear;
            }
        }
        else
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].color = _imageColors[i];
            }
            for (int i = 0; i < _TMPs.Length; i++)
            {
                _TMPs[i].color = _tmpColors[i];
            }
        }
    }
}
