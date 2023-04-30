using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _toolPanel = null;
    [SerializeField] TextMeshProUGUI _resourcesTMP = null;
    [SerializeField] Button[] _toolButtons = null;
    [SerializeField] TextMeshProUGUI[] _toolCosts = null;

    private void Start()
    {
        SetResourceAmount(0);
    }

    public void ShowToolPanel()
    {
        _toolPanel.ShowHide(true);
    }

    public void HideToolPanel()
    {
        _toolPanel.ShowHide(false);
    }

    public void SetResourceAmount(int amount)
    {
        _resourcesTMP.text = amount.ToString();
    }

    public void SetToolCost(int tool, int cost)
    {
        _toolCosts[tool].text = cost.ToString();
    }

    public void SetToolInteractable(int tool, bool isInteractable)
    {
        //Debug.Log($"tool {tool} is {isInteractable}");
        _toolButtons[tool].interactable = isInteractable;
        //_toolButtons[tool].gameObject.SetActive( isInteractable);
    }
}
