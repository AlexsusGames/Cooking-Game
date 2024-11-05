using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class InteractSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] cookingPlaceSounds;
    [SerializeField] private AudioClip[] nonLoopSounds;

    [Header("Order")]
    [SerializeField] private InteractivePlaces interactivePlacesOrder;
    [SerializeField] private NonLoopSounds nonLoopSoundsOrder;

    private Dictionary<InteractivePlaces, AudioClip> interactSoundsMap = new();
    private Dictionary<NonLoopSounds, AudioClip> nonLoopSoundsMap = new();
    private AudioSource[] audioSources;

    private void Awake()
    {
        CreateSoundMaps();

        audioSources = GetComponents<AudioSource>();
    }

    private void CreateSoundMaps()
    {
        for (int i = 0; i < Enum.GetValues(typeof(InteractivePlaces)).Length; i++)
        {
            interactSoundsMap[(InteractivePlaces)i] = cookingPlaceSounds[i];
        }

        for (int i = 0; i < Enum.GetValues(typeof(NonLoopSounds)).Length; i++)
        {
            nonLoopSoundsMap[(NonLoopSounds)i] = nonLoopSounds[i];
        }
    }

    public async void Play(InteractivePlaces place, int msTime)
    {
        for(int i = 0; i < audioSources.Length; i ++)
        {
            var audioSource = audioSources[i];

            if(!audioSource.isPlaying)
            {
                audioSource.loop = true;
                audioSource.clip = interactSoundsMap[place];
                audioSource.Play();
                await Task.Delay(msTime);
                audioSource.Stop();
                audioSource.loop = false;
                return;
            }
        }
    }
    public void Play(NonLoopSounds place)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            var audioSource = audioSources[i];

            if(Play(place, audioSource)) return;
        }
    }
    public bool Play(NonLoopSounds place, AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.loop = false;
            audioSource.clip = nonLoopSoundsMap[place];
            audioSource.Play();
            return true;
        }
        return false;
    }
}
public enum NonLoopSounds
{
    Door = 0,
    Turner = 1,
    Plate = 2,
    Fridge = 3,
    Click = 4,
    DrivingIn = 5,
    DrivingOut = 6,
    Cash = 7,
    Page = 8
}

