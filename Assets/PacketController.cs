using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketController : MonoBehaviour
{
    public static PacketController Instance;

    //settings
    [SerializeField] int _minValue = 3;
    [SerializeField] int _maxValue = 9;
    [SerializeField] float _minTime = 10f; //seconds
    [SerializeField] float _maxTime = 40f;

    //state
    Packet _currentPacket;
    float _timeRemainingForCurrentPacket;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateRandomPacket()
    {
        int value = UnityEngine.Random.Range(_minValue, _maxValue+1);
        float time = UnityEngine.Random.Range(_minTime, _maxTime);
        int boolvalue = UnityEngine.Random.Range(0, 4); //0: encrypted, 8 gives a 12.5% encrypted chance
        bool encrypt;
        if (boolvalue == 0) encrypt = true;
        else encrypt = false;

        Packet packet = new Packet(value, encrypt, time);
        _currentPacket = packet;
        _timeRemainingForCurrentPacket = _currentPacket.StartTime;
        UIController.Instance.Packet.SetPacketValue(_currentPacket.Value);
        UIController.Instance.Packet.SetEncryptionStatus(_currentPacket.Encrypted);
    }

    private void Update()
    {
        if (_currentPacket != null)
        {
            _timeRemainingForCurrentPacket -= Time.deltaTime;
            float factor = _timeRemainingForCurrentPacket / _maxTime;
            UIController.Instance.Packet.SetPacketTiming(factor);
        }
    }

}

public class Packet
{
    public int Value;
    public bool Encrypted;
    public float StartTime;

    public Packet(int value, bool encrypted, float startTime)
    {
        this.Value = value;
        this.Encrypted = encrypted;
        this.StartTime = startTime;
    }


}
