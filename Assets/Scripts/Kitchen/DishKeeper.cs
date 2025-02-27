using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class DishKeeper : InteractiveManager
{
    [SerializeField] private GameObject dishPrefab;
    [SerializeField] private Transform[] parents;
    [Inject] private InteractSound sound;
    private List<GameObject> dishes = new();
    public int MaxDishCount { get; } = 6;
    private string[] advices = { "������ ������� ���..\n�������� ����." };

    public int CountOfDish
    {
        get => dishes.Count;
        set
        {
            for (int i = 0; i < value; ++i)
            {
                CreateDish();
            }
        }
    }
    public bool IsMaxCountOfDish => dishes.Count >= MaxDishCount;

    public void CreateDish()
    {
        var number = Random.Range(0, parents.Length);
        var dish = Instantiate(dishPrefab, parents[number]);

        float offset = (float)parents[number].childCount / 10;
        dish.transform.localPosition = new Vector3(0, offset, 0);
        dishes.Add(dish);
    }

    public override void Interact()
    {
        var player = GetPlayer().gameObject;

        if (player.TryGetComponent(out ObjectHandler handler))
        {
            var obj = handler.GetObject();

            if (obj == null)
            {
                if (dishes.Count > 0)
                {
                    var dish = dishes[dishes.Count - 1];
                    dishes.Remove(dish);
                    handler.ChangeObject(dish);
                    sound.Play(NonLoopSounds.Plate);
                }
                else ShowAdvice(advices[0]);
            }

            else if (obj.TryGetComponent(out Dish dish))
            {
                if(dish.GetFood() == null)
                {
                    CreateDish();
                    sound.Play(NonLoopSounds.Plate);
                    handler.GetRidOfLastObject();
                }
            }
        }
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
