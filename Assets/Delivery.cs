using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private Animator deliveryAnimator;
    private DeliverInventory inventory;
    public event Action OnDelivered;

    private void Awake()
    {
        inventory = new();
    }

    public void Deliver(DeliveryData deliveryData)
    {
        StartCoroutine(StartDelivering());
    }

    private IEnumerator StartDelivering()
    {
        yield return new WaitForSeconds(60);
        deliveryAnimator.SetTrigger("drivingIn");
        yield return new WaitForSeconds(15);
    }

    private IEnumerator FinishDelivering()
    {
        deliveryAnimator.SetTrigger("drivingOut");
        yield return new WaitForSeconds(5);
        OnDelivered?.Invoke();
    }
}
