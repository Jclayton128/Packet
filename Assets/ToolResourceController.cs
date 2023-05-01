using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolResourceController : MonoBehaviour
{
    public static ToolResourceController Instance;

    //settings
    [SerializeField] int[] _toolCosts = new int[4];
    float _maxResources = 50f;

    //state
    int _currentResources;
    [SerializeField] int _currentTool;
    public int CurrentTool => _currentTool;

    private void Awake()
    {
        _currentResources = 0;
        _currentTool = 0;
        Instance = this;
    }

    private void Start()
    {
        SetToolCosts();
        PushToolAvailability();
        Invoke(nameof(SelectRouteTool_Delay), 0.01f);
    }

    private void SelectRouteTool_Delay()
    {
        HandleToolSelection(0);
    }

    public void SetToolCosts()
    {
        for (int i = 0; i < _toolCosts.Length; i++)
        {
            UIController.Instance.Tool.SetToolCost(i, _toolCosts[i]);
        }
    }

    public void GainResources(int amountToGain)
    {
        _currentResources += amountToGain;
        UIController.Instance.Resource.SetResourceSlider(_currentResources / _maxResources);
        PushToolAvailability();
    }

    public void LoseResource(int amountToLose)
    {

        _currentResources -= amountToLose;
        if (GameController.Instance.IsTutorialMode)
        {
            _currentResources = Mathf.Clamp(_currentResources, 20, 99);
        }
        UIController.Instance.Resource.SetResourceSlider(_currentResources/ _maxResources);
        PushToolAvailability();

        if (_currentResources < 0)
        {
            GameController.Instance.EnterGameOver();            
        }
    }

    public void PushToolAvailability()
    {
        for (int i = 0; i < _toolCosts.Length; i++)
        {
            if (_currentResources >= _toolCosts[i])
            {
                UIController.Instance.Tool.SetToolInteractable(i, true);
            }
            else
            {
                if (_currentTool == i)
                {
                    UIController.Instance.Tool.SelectTool(0);
                }
                UIController.Instance.Tool.SetToolInteractable(i, false);
            }
        }
    }

    public void HandleToolSelection(int tool)
    {
        _currentTool = tool;
        if (_currentTool == 0)
        {
            ServerController.Instance.RecheckCurrentActivatedNodesNeighbors();
        }
    }

    public void HandleSelectedToolUsageCost()
    {
        if (_toolCosts[CurrentTool] > _currentResources)
        {
            Debug.Log("not enough resources;");
            UIController.Instance.Tool.SelectTool(0);
        }
        else
        {
            LoseResource(_toolCosts[CurrentTool]);
            UIController.Instance.Tool.SelectTool(0);
        }
    }



}
