using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    public static ColorController Instance;



    //settings
    [SerializeField] Color _highlightColor = Color.white;
    public Color HighlightColor => _highlightColor;


    [SerializeField] Color _unloadedColor = Color.gray;
    public Color UnloadedColor => _unloadedColor;


    [SerializeField] Color _loadedColor_Low = Color.white;
    public Color LoadedColor_Low => _loadedColor_Low;


    [SerializeField] Color _loadedColor_Mid = Color.yellow;
    public Color LoadedColor_Mid => _loadedColor_Mid;


    [SerializeField] Color _loadedColor_High = Color.red;
    public Color LoadedColor_High => _loadedColor_High;


    [SerializeField] Color _coldTerminal = Color.gray;
    public Color ColdTerminal => _coldTerminal;


    [SerializeField] Color _startTerminal = Color.blue;
    public Color StartTerminal => _startTerminal;


    [SerializeField] Color _endTerminal = Color.green;
    public Color EndTerminal => _endTerminal;


    [SerializeField] Color _encryption = Color.magenta;
    public Color Encryption => _encryption;


    private void Awake()
    {
        Instance = this;
    }


}
