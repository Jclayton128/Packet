using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineParticleHandler : MonoBehaviour
{
    ParticleSystem _ps;
    ParticleSystem.MainModule _main;
    LineRenderer _renderer;

    public void Play()
    {
        if (!_ps)
        {
            _ps = GetComponent<ParticleSystem>();
            _main = _ps.main;
        }

        if (!_renderer)
        {
            _renderer = GetComponentInParent<LineRenderer>();
            Vector3 start = _renderer.GetPosition(0);
            Vector3 end = _renderer.GetPosition(1);
            Vector2 mid = ( start + end)/2f;

            Vector2 dir = (end - start);
            //Quaternion ang = Quaternion.LookRotation(Vector3.forward, dir);
            transform.position = mid;
            transform.right = dir;

            var shape = _ps.shape;
            shape.radius = dir.magnitude / 2f;

        } 

        if (PacketController.Instance.GetPacketEncryption())
        {
            _main = _ps.main;
            _main.startColor = ColorController.Instance.Encryption;
        }
        else
        {
            _main = _ps.main;
            _main.startColor = ColorController.Instance.SelectedLink;
        }

        _ps.Play();
    }

    public void Stop()
    {
        if (!_ps) _ps = GetComponent<ParticleSystem>();
        _ps.Stop();
    }
}
