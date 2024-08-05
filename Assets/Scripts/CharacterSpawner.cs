using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;
    [SerializeField] private CharacterPool characterPool;
    [SerializeField] private WayPoints[] wayPoints;
    [SerializeField] private bool addCollider;
    private WaitForSeconds spawnTime => new WaitForSeconds(Random.Range(minSpawnRate,maxSpawnRate));

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        while (true)
        {
            var way = wayPoints[Random.Range(0, wayPoints.Length)].Points;
            var character = characterPool.TryGetCharacter(way[0]);

            if(character != null)
            {
                character.TryGetComponent(out CharacterMove movement);

                movement.Bind(way);
                movement.ColliderEnable(addCollider);
            }

            yield return spawnTime;
        }
    }
}
