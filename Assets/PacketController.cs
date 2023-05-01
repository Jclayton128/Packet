using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketController : MonoBehaviour
{
    public static PacketController Instance;

    //settings
    [SerializeField] int _minValue = 1;
    [SerializeField] int _maxValue = 5;
    [SerializeField] float _minTime = 10f; //seconds
    [SerializeField] float _maxTime = 40f;
    [SerializeField] float _moveCostMultiplier = 1f;
    [SerializeField] float _encryptionPenaltyChance = 0.5f;
    public float EncryptionPenaltyChance => _encryptionPenaltyChance;

    //state
    Packet _currentPacket;
    float _timeRemainingForCurrentPacket;
    int _packetsDelivered;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        TerminalController.Instance.TargetTerminalActivated += GainCurrentPacket;
        ServerController.Instance.NodeActivated += HandleNodeJump;
        _packetsDelivered = 0;
    }

    private void HandleNodeJump(SelectionHandler arg1, SelectionHandler arg2)
    {
        float cost = (arg1.transform.position - arg2.transform.position)
            .magnitude * _moveCostMultiplier;
        _timeRemainingForCurrentPacket -= cost;
    }

    public void GenerateRandomPacket()
    {
        int value = UnityEngine.Random.Range(_minValue, _maxValue+1);
        float time = UnityEngine.Random.Range(_minTime, _maxTime);

        if (!UIController.Instance.Packet.IsTimerArcEnabled)
        {
            time = 600;
        }

        int boolvalue = UnityEngine.Random.Range(0, 6); //0: encrypted, 8 gives a 12.5% encrypted chance
        bool encrypt;
        if (!TutorialController.Instance.IsInTutorialPair && boolvalue == 0)
        {
            value += 1;
            value = Mathf.Clamp(value, _minValue, _maxValue);
            encrypt = true;
            SoundController.Instance.PlayRandomNewEncPacket();
        }
        else encrypt = false;
        SoundController.Instance.PlayRandomNewEncPacket();
        Packet packet = new Packet(value, encrypt, time);
        _currentPacket = packet;
        _timeRemainingForCurrentPacket = _currentPacket.StartTime;
        UIController.Instance.Packet.SetPacketValue(_currentPacket.Value);
        UIController.Instance.Packet.SetEncryptionStatus(_currentPacket.Encrypted);
    }

    public void ClearPacket()
    {
        _currentPacket = null;
        UIController.Instance.Packet.ClearPacketPanel();
    }

    private void Update()
    {
        if (_currentPacket != null)
        {
            _timeRemainingForCurrentPacket -= Time.deltaTime;
            float factor = _timeRemainingForCurrentPacket / _maxTime;
            UIController.Instance.Packet.SetPacketTiming(factor);
            if (_timeRemainingForCurrentPacket <= 0)
            {
                Debug.Log("out of time!");
                LoseCurrentPackage();
            }
        }

   
    }

    public void GainCurrentPacket()
    {
        SoundController.Instance.PlayRandomCompletion();
        _packetsDelivered++;
        UIController.Instance.Endgame.SetPacketCounter(_packetsDelivered);
        ToolResourceController.Instance.GainResources(_currentPacket.Value);
        ClearPacket();
        PathController.Instance.CreateNewPathProblem();
    }

    public void LoseCurrentPackage()
    {
        var ps = ServerController.Instance._currentActivatedNode.
            GetComponentInChildren<ParticleSystem>();
        if (ps) ps.Stop();

        if (_currentPacket != null)
        {
            SoundController.Instance.PlayRandomFailure();
            ToolResourceController.Instance.LoseResource(_currentPacket.Value);
            ClearPacket();
            PathController.Instance.CreateNewPathProblem();
        }

    }

    public bool GetPacketEncryption()
    {
        if (_currentPacket != null && _currentPacket.Encrypted)
        {
            return true;
        }
        else return false;
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
