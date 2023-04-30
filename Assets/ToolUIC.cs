using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ToolUIC : MonoBehaviour
{
    [SerializeField] UIElementDriver _toolPanel = null;
    [SerializeField] TextMeshProUGUI _resourcesTMP = null;
    [SerializeField] Button[] _toolButtons = null;
    [SerializeField] TextMeshProUGUI[] _toolCosts = null;

    private void Start()
    {
        SetResourceAmount(0);
        _toolButtons[0].Select();
        ToolResourceController.Instance.HandleToolSelection(0);
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

    private void Update()
    {
        ListenForKeyboard();

    }

    private void ListenForKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) &&
            _toolButtons[0].interactable)
        {
            SelectTool(0);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) &&
    _toolButtons[1].interactable)
        {
            SelectTool(1);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) &&
    _toolButtons[2].interactable)
        {
            SelectTool(2);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) &&
    _toolButtons[3].interactable)
        {
            SelectTool(3);
            return;
        }
    }

    public void SelectTool(int tool)
    {
        _toolButtons[tool].Select();
        ToolResourceController.Instance.HandleToolSelection(tool);
    }
}
