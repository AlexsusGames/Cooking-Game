using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishKeeper : MonoBehaviour
{
    [SerializeField] private GameObject dishPrefab;
    [SerializeField] private float radius = 5f;
    [SerializeField] private Transform[] parents;
    [SerializeField] private int[] dishPoints;
    private List<GameObject> dishes;
    private int maxDishCount = 22;

    private void CreatePool()
    {
        for (int i = 0; i < maxDishCount; i++)
        {
            var number = Random.Range(0, dishPoints.Length);
            var dish = Instantiate(dishPrefab, parents[number]);
            dishes.Add(dish);
        }   
    }

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if(collider.transform.childCount == 0) continue;

            var child = collider.transform.GetChild(0);

            if(child != null)
            {
                if (child.TryGetComponent(out ObjectHandler handler))
                {
                    var obj = handler.GetObject();

                    if (obj == null)
                    {
                        if (dishes.Count > 0)
                        {
                            var dish = dishes[dishes.Count - 1];
                            dishes.Remove(dish);
                            handler.ChangeObject(dish);
                        }
                    }
                    else
                    {
                        handler.ChangeObject();
                    }
                }
            }
        }
    }
}
