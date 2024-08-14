using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGroup : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particleSystem;

    public void Stop()
    {
        for (int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].Stop();
        }
    }

    public void Play()
    {
        for (int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].Play();
        }
    }
}
