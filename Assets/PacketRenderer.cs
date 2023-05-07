using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketRenderer : MonoBehaviour
{
    //settings
    [SerializeField] float _baseSpinRate = 90f;
    [SerializeField] SpriteRenderer[] _packets = null;


    //state
    NewNodeHandler _targetNode;
    int _currentPacketCount;
    [SerializeField] bool _isActive;
    Vector3 _eulerAng;
    float _currentSpinRate;

    private void Awake()
    {
        _isActive = false;
        _currentPacketCount = 0;
        float rand = UnityEngine.Random.Range(-180f, 180f);
        _eulerAng = new Vector3(0, 0, rand);
    }

    public void SetupPacket(NewNodeHandler newTargetNode, int packetSize,
        float priority)
    {
        MovePacket(newTargetNode);
        transform.localRotation = Quaternion.Euler(_eulerAng);
        _currentPacketCount = packetSize;
        _currentSpinRate = _baseSpinRate * Mathf.Pow(priority, 1.5f);


    }

    public void MovePacket(NewNodeHandler newTargetNode)
    {
        _targetNode = newTargetNode;
        transform.parent = _targetNode.transform;
        transform.localPosition = Vector3.zero;
    }

    public void ActivatePacket()
    {
        _isActive = true;
        for (int i = 0; i < _packets.Length; i++)
        {
            if (i < _currentPacketCount)
            {
                _packets[i].enabled = true;
                _packets[i].color = ColorController.Instance.SourceNode;
                _packets[i].GetComponentInChildren<ParticleSystem>()?.Play();
            }
            else
            {
                _packets[i].enabled = false;
                _packets[i].color = ColorController.Instance.ColdClear;
                _packets[i].GetComponentInChildren<ParticleSystem>()?.Stop();
            }
        }
    }

    public void DeactivatePacket()
    {
        _isActive = false;
        foreach (var packet in _packets)
        {
            packet.enabled = false;
        }
    }

    private void Update()
    {
        if (!_isActive) return;
        _eulerAng.z += Time.deltaTime * _currentSpinRate;
        transform.localEulerAngles = _eulerAng;
    }
}
