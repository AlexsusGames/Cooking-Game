using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private CharacterPool characterPool;
    [SerializeField] private WayPoints[] wayPoints;
    private WaitForSeconds spawnTime => new WaitForSeconds(Random.Range(minSpawnRate,maxSpawnRate));
    public int MaxCountOfPeopleInQueue { get; set; }

    private void Start()
    {
        StartCoroutine(Spawner());
        MaxCountOfPeopleInQueue = 2;
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            var way = wayPoints[Random.Range(0, wayPoints.Length)].Points;

            if (!IskBigQueue(way[0]))
            {
                var character = characterPool.TryGetCharacter(way[0]);

                if (character != null)
                {
                    character.TryGetComponent(out CharacterMove movement);

                    if (movement != null)
                    {
                        movement.Bind(way);
                    }
                    else
                        throw new System.Exception("Character must contain 'CharacterMove'");
                }
            }

            yield return spawnTime;
        }
    }
    private bool IskBigQueue(Transform transform)
    {
        int countOfActive = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                countOfActive++;
            }
        }

        return countOfActive >= MaxCountOfPeopleInQueue;
    }
}
