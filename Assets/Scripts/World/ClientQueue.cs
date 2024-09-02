using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ClientQueue : MonoBehaviour
{
    [SerializeField] private QueuePoint[] queuePoints;
    [SerializeField] private CharacterPool pool;
    [SerializeField] private float time;
    [SerializeField] private int maxPeopleInQueue;
    private List<CharacterMove> characters = new();
    private List<CharacterMove> charactersInQueue = new();
    private KnownRecipes knownRecipes = new KnownRecipes();

    private void Start()
    {
        var characters = pool.GetPool();

        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].TryGetComponent(out CharacterMove movement);
            this.characters.Add(movement);
        }

        StartCoroutine(Timer());
    }

    public QueuePoint GetFreePoint()
    {
        for (int i = 0; i < queuePoints.Length; i++)
        {
            if (queuePoints[i].isAvailable)
            {
                return queuePoints[i];
            }
        }
        return null;
    }

    public void Service()
    {
        charactersInQueue.RemoveAt(0);
        UpdatePositions();
    }

    private void ResetPositions()
    {
        for(int i = 0;i < queuePoints.Length; i++)
        {
            queuePoints[i].isAvailable = true;
        }
    }

    private void UpdatePositions()
    {
        ResetPositions();

        for(int i = 0;i < charactersInQueue.Count;i++)
        {
            AddToQueue(charactersInQueue[i]);
        }
    }
    private void AddNewToQueue(CharacterMove movement)
    {
        if (!charactersInQueue.Contains(movement))
        {
            charactersInQueue.Add(movement);
            AddToQueue(movement);
        }
    }

    private void AddToQueue(CharacterMove movement)
    {
        var freePoint = GetFreePoint();
        var newPosition = freePoint.point.position;
        movement.GoToQueuePoint(newPosition, gameObject.transform.position);
        freePoint.isAvailable = false;
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            int countRecipes = knownRecipes.GetCountOfSellingRecipes();
            if (charactersInQueue.Count < maxPeopleInQueue && countRecipes > 0)
            {
                int random = Random.Range(0, characters.Count);
                var character = characters[random];

                if (character.gameObject.activeInHierarchy)
                {
                    AddNewToQueue(character);
                }
            }

            yield return new WaitForSeconds(time - countRecipes);
        }
    }
}

[System.Serializable]
public class QueuePoint
{
    public Transform point;
    public bool isAvailable;
}
