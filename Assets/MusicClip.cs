using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class MusicClip : MonoBehaviour
{
    [SerializeField] protected AudioSource musicSource;
    [Inject] private SoundDataProvider dataProvider;

    public virtual void OnEnable()
    {
        UpdateVolume(dataProvider.GetData());
        dataProvider.OnDataChanged += UpdateVolume;
    }
    public virtual void OnDisable() => dataProvider.OnDataChanged -= UpdateVolume;

    public virtual void UpdateVolume(SoundData data)
    {
        musicSource.volume = data.Music;
    }
}
