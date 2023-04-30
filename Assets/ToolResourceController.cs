using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolResourceController : MonoBehaviour
{
    public static ToolResourceController Instance;

    //settings
    [SerializeField] int[] _toolCosts = new int[3];


    //state
    int _currentResources;  

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIController.Instance.Tool.SetResourceAmount(_currentResources);
        SetToolCosts();
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
        UIController.Instance.Tool.SetResourceAmount(_currentResources);
        PushToolAvailability();
    }

    public void LoseResource(int amountToLose)
    {
        _currentResources -= amountToLose;
        UIController.Instance.Tool.SetResourceAmount(_currentResources);
        PushToolAvailability();
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
                UIController.Instance.Tool.SetToolInteractable(i, false);
            }
        }
    }

}
