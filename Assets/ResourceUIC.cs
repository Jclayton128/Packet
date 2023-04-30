using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _sliderPanel = null;
    [SerializeField] Image _resourceFill = null;

    [SerializeField] Color _colorFull = Color.green;
    [SerializeField] Color _colorEmpty = Color.red;

    public void ShowResourcePanel()
    {
        _sliderPanel.ShowHide(true);
    }

    public void HideResourcePanel()
    {
        _sliderPanel.ShowHide(false);
    }

    public void SetResourceSlider(float factor)
    {
        _resourceFill.fillAmount = factor;
        _resourceFill.color = Color.Lerp(_colorEmpty, _colorFull, factor);

        //TODO change color based on factor prox to failure
    }

}
