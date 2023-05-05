using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController Instance;



    [Header("Load Dots")]
    [SerializeField] Color _unloadedColor = Color.gray;
    public Color UnloadedColor => _unloadedColor;
    public Color UnloadedClear => new Color(_unloadedColor.r, _unloadedColor.g, _unloadedColor.b, 0);


    [SerializeField] Color _loadedColor_Low = Color.white;
    public Color LoadedColor_Low => _loadedColor_Low;


    [SerializeField] Color _loadedColor_Mid = Color.yellow;
    public Color LoadedColor_Mid => _loadedColor_Mid;


    [SerializeField] Color _loadedColor_High = Color.red;
    public Color LoadedColor_High => _loadedColor_High;

    [Header("Links")]
    [SerializeField] Color _coldLink = Color.gray;
    public Color ColdLink => _coldLink;

    [SerializeField] Color _warmLink = Color.gray;
    public Color WarmLink => _warmLink;

    [SerializeField] Color _selectedLink = Color.green;
    public Color SelectedLink => _selectedLink;


    [Header("Nodes")]
    [SerializeField] Color _coldNode = Color.gray;
    public Color ColdNode => _coldNode;
    public Color ColdClear => new Color(_coldNode.r, _coldNode.g, _coldNode.b, 0);


    [SerializeField] Color _warmNode = Color.blue;
    public Color WarmNode => _warmNode;
    

    [SerializeField] Color _selectedNode = Color.white;
    public Color SelectedNode => _selectedNode;


    [SerializeField] Color _brokenNode = Color.red;
    public Color BrokenNode => _brokenNode;


    [SerializeField] Color _selectableNode = Color.white;
    public Color SelectableRing => _selectableNode;


    [SerializeField] Color _upgradeableNode = Color.cyan;
    public Color UpgradeableNode => _upgradeableNode;


    [SerializeField] Color _targetTerminal = Color.green;
    public Color TargetTerminal => _targetTerminal;


    [SerializeField] Color _encryption = Color.magenta;
    public Color Encryption => _encryption;



    private void Awake()
    {
        Instance = this;
    }


}
