using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefButtonHandler : MonoBehaviour
{
    [SerializeField] Sprite[] _spritesToCycleThroughLoudToMute = null;
    [SerializeField] Image _image = null;

    [SerializeField] SoundController _soundController = null;
    [SerializeField] MusicController _musicController = null;

    [SerializeField] bool _isForSound = true;

    public void HandleClick()
    {
        if (_isForSound)
        {
            int index = _soundController.ToggleVolume();
            _image.sprite = _spritesToCycleThroughLoudToMute[index];
        }
        else
        {
            int index = _musicController.ToggleVolume();
            _image.sprite = _spritesToCycleThroughLoudToMute[index];
        }
    }
}
