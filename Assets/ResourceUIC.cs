using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _sliderPanel = null;
    [SerializeField] Slider _resourceSlider = null;

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
        _resourceSlider.value   = factor;
        //TODO change color based on factor prox to failure
    }

}
