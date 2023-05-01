using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public enum SoundID
    {
        ButtonHoverOver0,
        ButtonHoverExit1,
        Unused2,
        ButtonRelease3,
        NewGameStart4,
        Unused5,
        Unused6,
        Unused7,
        Unused8,
        ServerImproveCap9,
        ServerEncrypt10,
        ServerRepair11,
        ServerBreak12,
        Unused13,
        Unused14,
        GameOver15,
    }

    [SerializeField] AudioClip[] _activationSounds = null;

    [SerializeField] AudioClip[] _completionSounds = null;
    [SerializeField] AudioClip[] _failureSounds = null;
    [SerializeField] AudioClip[] _newPaxSounds = null;
    [SerializeField] AudioClip[] _encPaxSounds = null;

    [SerializeField] AudioSource _auso = null;
    [SerializeField] float[] _volumeLevels = new float[3] { 1f, .5f, 0f };


    [SerializeField] AudioClip[] _rawClips = null;
    Dictionary<SoundID, AudioClip> _clips = new Dictionary<SoundID, AudioClip>();

    //state
    int _currentVolumeIndex = 0;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < _rawClips.Length; i++)
        {
            _clips.Add((SoundID)i, _rawClips[i]);
        }

    }

    public void PlaySound(int index)
    {
        PlaySound((SoundID)index);
    }

    public void PlayRandomActivation()
    {
        int rand = UnityEngine.Random.Range(0, _activationSounds.Length);
        _auso.PlayOneShot(_activationSounds[rand]);
    }

    public void PlayRandomCompletion()
    {
        int rand = UnityEngine.Random.Range(0, _completionSounds.Length);
        _auso.PlayOneShot(_completionSounds[rand]);
    }
    public void PlayRandomFailure()
    {
        int rand = UnityEngine.Random.Range(0, _failureSounds.Length);
        _auso.PlayOneShot(_failureSounds[rand]);
    }
    public void PlayRandomNewPacket()
    {
        int rand = UnityEngine.Random.Range(0, _newPaxSounds.Length);
        _auso.PlayOneShot(_newPaxSounds[rand]);
    }
    public void PlayRandomNewEncPacket()
    {
        int rand = UnityEngine.Random.Range(0, _encPaxSounds.Length);
        _auso.PlayOneShot(_encPaxSounds[rand]);
    }

    public void PlaySound(SoundID sound)
    {
        _auso.PlayOneShot(_clips[sound]);
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
