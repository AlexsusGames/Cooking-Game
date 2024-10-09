using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [Inject] private InteractSound sound;
    private Animator animator;
    private const int ANIMATION_DURATION_MS = 2000;
    private bool isAnimPlay;

    private void Awake() => animator = GetComponent<Animator>();

    public async void OpenDoor(string triggerName)
    {
        if (!isAnimPlay)
        {
            sound.Play(NonLoopSounds.Door, audioSource);

            isAnimPlay = true;
            animator.SetTrigger(triggerName);
            await Task.Delay(ANIMATION_DURATION_MS);
            isAnimPlay = false;
        }
    }
}
