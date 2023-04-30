using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;

    public enum MusicID
    {
        Intro,
        MainGame,
        EndGame,
        Credits
    }

    [SerializeField] AudioSource _auso = null;
    [SerializeField] float[] _volumeLevels = new float[3] { .7f, .3f, .0f };


    [SerializeField] AudioClip[] _rawClips = null;
    Dictionary<MusicID, AudioClip> _clips = new Dictionary<MusicID, AudioClip>();


    //state
    int _currentVolumeIndex = 0;


    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < _rawClips.Length; i++)
        {
            _clips.Add((MusicID)i, _rawClips[i]);
        }
    }

    public void PlaySound(int index)
    {
        PlaySound((MusicID)index);
    }

    public void PlaySound(MusicID music)
    {
        _auso.clip = _clips[music];
        _auso.Play();
    }

    public int ToggleVolume()
    {
        _currentVolumeIndex++;
        if (_currentVolumeIndex >= _volumeLevels.Length)
        {
            _currentVolumeIndex = 0;
        }
        _auso.volume = _volumeLevels[_currentVolumeIndex];
        return _currentVolumeIndex;
    }
}
