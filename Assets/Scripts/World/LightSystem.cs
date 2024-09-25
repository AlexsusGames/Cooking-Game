using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private Light[] worldLight;
    [SerializeField] private Light shadowLight;
    [SerializeField] private Light[] kitchenLight;
    [SerializeField] private Light bedroomLight;

    [SerializeField] private TimeChanger view;
    private Coroutine coroutine;

    private const float IntensityChangeForSecond = 0.008f;
    public bool isDayTime;
    public bool IsOpen;

    public event Action OnDayStart;
    public event Action OnDayEnd;

    public bool Kitchen
    {
        get => kitchenValue;
        set
        {
            kitchenValue = !value;
            ChangeKitchenLight();
        }

    }
    public bool Bedroom
    {
        get => bedroomValue;
        set
        {
            bedroomValue = !value;
            ChangeBedroomLight();
        }
    }

    private bool kitchenValue;
    private bool bedroomValue;

    private void Start()
    {
        isDayTime = true;
        view.OnFinish += () =>
        {
            IsOpen = false;
            OnDayEnd?.Invoke();
        };
    }

    public void StartDayCycle()
    {
        if(coroutine == null)
        {
            IsOpen = true;
            coroutine = StartCoroutine(DayCycle());
            OnDayStart?.Invoke();
        }
    }

    public void ChangeBedroomLight()
    {
        bedroomValue = !bedroomValue;
        bedroomLight.intensity = bedroomValue ? 1 : 0;
    }

    public void ChangeKitchenLight()
    {
        kitchenValue = !kitchenValue;
        for(int i = 0; i < kitchenLight.Length; i++)
        {
            kitchenLight[i].intensity = kitchenValue ? 1 : 0;
        }
    }

    private IEnumerator DayCycle()
    {
        while (IsOpen)
        {
            yield return new WaitForSeconds(2);

            float time = worldLight[0].intensity < 1 && isDayTime
                ? IntensityChangeForSecond
                : -IntensityChangeForSecond;

            ChangeLightIntensity(time);

            if (worldLight[0].intensity >= 1)
                isDayTime = false;

            shadowLight.transform.Rotate(0f, -0.5f, 0f);
            view.ChangeTime(300);
        }
    }
    private void ChangeLightIntensity(float value)
    {
        for (int i = 0; i < worldLight.Length; i++)
        {
            worldLight[i].intensity += value;
        }
    }
}
