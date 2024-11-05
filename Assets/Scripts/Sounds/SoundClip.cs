using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundClip : MusicClip
{
    public void Play()
    {
        musicSource.Play();
    }

    public override void UpdateVolume(SoundData data)
    {
        musicSource.volume = data.Sound;
    }
}
