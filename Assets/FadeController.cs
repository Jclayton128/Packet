using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static FadeController Instance;

    List<FadeHandler> _fadeHandlers = new List<FadeHandler>();

    //state
    public int _currentPhase;

    private void Awake()
    {
        Instance = this;
        _currentPhase = -2;
        
        var fh = FindObjectsOfType<FadeHandler>();
        foreach (var f in fh)
        {
            _fadeHandlers.Add(f);
        }
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
    }

    public void DecrementFadeOutPhase()
    {
        _currentPhase--;
        foreach (var fh in _fadeHandlers)
        {
            fh.FadeOutCheck(_currentPhase);
        }
    }

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
}
