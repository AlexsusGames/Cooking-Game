using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSystem : MonoBehaviour
{
    [SerializeField] private Light[] worldLight;
    [SerializeField] private Light shadowLight;
    [SerializeField] private Light kitchenLight;
    [SerializeField] private Light bedroomLight;

    private bool kitchenValue;
    private bool bedroomValue;

    private void Start()
    {
        StartCoroutine(DayCycle());
    }

    public void ChangeBedroomLight()
    {
        bedroomValue = !bedroomValue;
        bedroomLight.intensity = bedroomValue ? 1 : 0;
    }

    public void ChangeKitchenLight()
    {
        kitchenValue = !kitchenValue;
        kitchenLight.intensity = kitchenValue ? 1 : 0;
    }

    private IEnumerator DayCycle()
    {
        while (worldLight[0].intensity > 0)
        {
            yield return new WaitForSeconds(1);

            for (int i = 0; i < worldLight.Length; i++)
            {
                worldLight[i].intensity -= 0.004f;
            }

            shadowLight.transform.Rotate(0f, -0.4f, 0f);
        }
    }
}
