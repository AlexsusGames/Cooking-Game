using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashTrigger : MonoBehaviour
{
    [SerializeField] private OrderView view;
    [SerializeField] private float timeToService;
    private GameObject lastPerson;
    private RecipeConfig lastOrder;

    private event Action<float> timer;
    private float time;

    private void Awake()
    {
        timer += view.ChangePatience;
    }

    private void FixedUpdate()
    {
        if(time > 0)
        {
            time -= Time.fixedDeltaTime;
            var fillAmount = time / timeToService;
            timer?.Invoke(fillAmount);
            Debug.Log(fillAmount);
        }

        if(lastPerson != null && time <= 0)
        {
            Service();
        }
    }

    private void Service()
    {
        lastPerson.TryGetComponent(out CharacterMove movement);
        if(movement != null) { movement.OnServiced(false); }
        lastPerson = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        time = timeToService;
        other.TryGetComponent(out ClientsOrder order);
        lastPerson = other.gameObject;
        lastOrder = order.Order;

        view.ShowClientOrder(lastOrder.picture);
    }
}
