using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public enum SoundID
    {
        ButtonHoverOver,
        ButtonHoverExit,
        ButtonClickDown,
        ButtonRelease,
        NewGameStart,
        NodeActivation,
        PacketDelivered,
        PacketFailure,
        PacketCancel,
        ServerImproveCap,
        ServerEncrypt,
        ServerRepair,
        ServerBreak,
        NewPacketArrival_Unencrypted,
        NewPacketArrival_Encrypted,
        GameOver,
    }

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
