using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    List<FadeHandler> _fadeHandlers = new List<FadeHandler>();

    List<GameObject> _newFaders = new List<GameObject>();
    [SerializeField] float _fadeDuration = 1f;


    //state
    public int _currentPhase;
    Dictionary<int, List<IFadeable>> _phases = new Dictionary<int, List<IFadeable>>();

    private void Awake()
    {
        Instance = this;
        _currentPhase = -1;
        
        var fh = FindObjectsOfType<FadeHandler>();
        foreach (var f in fh)
        {
            _fadeHandlers.Add(f);
        }


    }

    private void Start()
    {
        Invoke(nameof(InstafadeAll), 0.01f);
    }

    public void IncrementFadeInPhase()
    {
        _currentPhase++;
        foreach (var fh in _fadeHandlers)
        {
            fh.FadeInCheck(_currentPhase);
        }

        if (_currentPhase == 4)
        {
            foreach (var fh in _fadeHandlers)
            {
                fh.AllowNonCoreVisuals();
            }
        }

        List<IFadeable> faders = _phases[_currentPhase];
        foreach (var fader in faders)
        {
            fader.FadeIn(_fadeDuration);
        }
    }

    public void DecrementFadeOutPhase()
    {
        _currentPhase--;
        foreach (var fh in _fadeHandlers)
        {
            fh.FadeOutCheck(_currentPhase);
        }
    }

    [ContextMenu("insta fade all")]
    public void InstafadeAll()
    {
        foreach (var fh in _fadeHandlers)
        {
            fh.InstaFadeOut();
        }
    }

    public void AddFadeHandler(FadeHandler newFadeHandler)
    {
        if (!_fadeHandlers.Contains(newFadeHandler))
        {
            _fadeHandlers.Add(newFadeHandler);
        }
    }

    public void FadeOutEverything()
    {
        foreach (var fh in _fadeHandlers)
        {
            fh.ForceFadeoutWithOrganicDelay();
        }
    }
}
