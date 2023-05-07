using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPacketController : MonoBehaviour
{
    public static NewPacketController Instance;
    public enum PacketType
    {
        Regular0,
        Multi1,

        CountX
    }

    //settings

    
    [SerializeField] int _packetSize_Regular = 3;
    public int PacketSizeMax => _packetSize_Regular;
    [SerializeField] int _packetSize_Multi = 1;
    [SerializeField] int _minPriority = 1;
    [SerializeField] int _maxPriority = 4;

    //state
    int _currentPacketSize;
    public int CurrentPacketSize => _currentPacketSize;
    PacketType _currentPacketType;
    int _currentPacketPriority;
    public int CurrentPacketPriority => _currentPacketPriority;
    public PacketType CurrentPacketType => _currentPacketType;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateNewPacket()
    {
        int rand_type = UnityEngine.Random.Range(0, 1);//(int)PacketType.CountX);
        int rand_pri = UnityEngine.Random.Range(_minPriority, _maxPriority);
        
        if (rand_type == 0)
        {
            _currentPacketType = PacketType.Regular0;
            _currentPacketSize = _packetSize_Regular;
            _currentPacketPriority = rand_pri;
        }
        if (rand_type == 1)
        {
            _currentPacketType = PacketType.Multi1;
            _currentPacketSize = _packetSize_Multi;
            _currentPacketPriority = rand_pri;
        }

    }
}
