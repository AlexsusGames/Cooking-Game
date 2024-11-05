using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Delivery : InteractiveManager
{
    [SerializeField] private Animator deliveryAnimator;
    [SerializeField] private GameObject deliveryCollider;
    [SerializeField] private DeliverInventory inventory;
    [SerializeField] private AudioSource audioSource;

    private string[] advices = { };
    private const string QUEST_REQUEST = "tutor3";
    [Inject] private QuestHandler questHander;
    [Inject] private InteractSound sound;

    public bool IsDelivering;
    private Coroutine deliveryCoroutine;

    private void Start()
    {
        if (inventory.HasAnyProduct())
        {
            IsDelivering = true;
            StartCoroutine(StartDelivering(0));
        }

    }

    public void Deliver(DeliveryData deliveryData)
    {
        var products = deliveryData.GetOrderedProducts();

        foreach (var item in products.Keys)
        {
            inventory.AddItemToDeliver(item, products[item]);
        }

        StartCoroutine(StartDelivering(0));
    }

    private IEnumerator StartDelivering(int time)
    {
        IsDelivering = true;
        yield return new WaitForSeconds(time);
        deliveryAnimator.SetTrigger("drivingIn");
        yield return new WaitForSeconds(8);
        sound.Play(NonLoopSounds.DrivingIn, audioSource);
    }

    public void DeleteProducts()
    {
        inventory.RemoveAllProducts();
        StartCoroutine(FinishDelivering());
    }

    public override void Interact()
    {
        var item = inventory.TakeFirstItem();
        var playerInventoryGrid = GetPlayer().GetComponent<Inventory>().Setup();

        if(playerInventoryGrid != null )
        {
            if (playerInventoryGrid.CanTake(item.name, item.amount))
            {
                playerInventoryGrid.AddItems(item.name, item.amount);
                sound.Play(NonLoopSounds.Click);
            }

            else inventory.AddItemToDeliver(item.name, item.amount);

            if (!inventory.HasAnyProduct() && deliveryCoroutine == null)
            {
                deliveryCoroutine = StartCoroutine(FinishDelivering());
            }
        }
    }

    private IEnumerator FinishDelivering()
    {
        questHander.TryChangeProgress(QUEST_REQUEST);

        IsDelivering = false;
        deliveryAnimator.SetTrigger("drivingOut");
        sound.Play(NonLoopSounds.DrivingOut, audioSource);
        yield return new WaitForSeconds(3);
        deliveryCoroutine = null;
    }
    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            CachedKeys = advices;
        }

        return CachedKeys;
    }

    public override void Set(params string[] param) => advices = param;
}
