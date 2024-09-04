using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image statusImage;
    [SerializeField] private Sprite openStatus;
    [SerializeField] private Sprite closeStatus;
    [SerializeField] private Animator animator;
    private TimeSpan startTime = new TimeSpan(7, 0, 0);
    private TimeSpan finishTime = new TimeSpan(21, 0, 0); 
    private TimeSpan time;

    public event Action OnFinish;

    private void Awake()
    {
        time = startTime;
    }

    public void ChangeTime(int remainingSeconds)
    {
        if(animator.enabled == false)
        {
            animator.enabled = true;
            statusImage.sprite = openStatus;
        }

        TimeSpan seconds = TimeSpan.FromSeconds(remainingSeconds);
        time += seconds;
        timeText.text = time.ToString(@"hh\:mm");


        if(time == finishTime)
        {
            OnFinish?.Invoke();
            statusImage.sprite = closeStatus;
        }
    }
}
