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
    [SerializeField] private LightSystem lightSystem;

    private List<CharacterMove> characters = new();
    private List<CharacterMove> charactersInQueue = new();
    private KnownRecipes knownRecipes = new();

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

    private void OnDisable() => StopAllCoroutines();

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

    public void Service(CharacterMove movement)
    {
        charactersInQueue.Remove(movement);
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
        Print(charactersInQueue);
        ResetPositions();

        for(int i = 0;i < charactersInQueue.Count;i++)
        {
            AddToQueue(charactersInQueue[i]);
        }
    }

    private void Print(List<CharacterMove> movements)
    {
        string info = string.Empty;

        for (int i = 0; i < movements.Count; i++)
        {
            info += movements[i].name;
        }

        Debug.Log(info);
    }
    private void AddNewToQueue(CharacterMove movement)
    {
        Debug.Log("Trying to add new to queue");

        if (!charactersInQueue.Contains(movement))
        {
            Debug.Log(movement.name + " added");
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

    private void OnShopClose()
    {
        for (int i = 1; i < charactersInQueue.Count; i++)
        {
            charactersInQueue[i].ContinueWalking();
        }
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            if(lightSystem.IsOpen)
            {
                int countRecipes = knownRecipes.GetCountOfSellingRecipes();
                if (charactersInQueue.Count < maxPeopleInQueue && countRecipes > 0)
                {
                    int random = Random.Range(0, characters.Count);
                    var character = characters[random];

                    if (character.gameObject.activeInHierarchy && !character.IsServed)
                    {
                        AddNewToQueue(character);
                    }
                }

                float delay = this.time - countRecipes * 2;
                float time = delay < 10 ? 10 : delay;

                yield return new WaitForSeconds(time);
            }
            else
            {
                OnShopClose();
            }

            yield return null;
        }
    }
}

[System.Serializable]
public class QueuePoint
{
    public Transform point;
    public bool isAvailable;
}
