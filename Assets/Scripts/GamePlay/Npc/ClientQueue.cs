using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

public class ClientQueue : MonoBehaviour
{
    [SerializeField] private QueuePoint[] queuePoints;
    [SerializeField] private CharacterPool pool;
    [SerializeField] private float time;
    [SerializeField] private int maxPeopleInQueue;
    [SerializeField] private LightSystem lightSystem;
    [SerializeField] private Bank bank;

    [Inject] private RatingManager ratingManager;
    private RatingStats ratingStats;

    private List<CharacterMove> characters = new();
    private List<CharacterMove> charactersInQueue = new();
    private KnownRecipes knownRecipes = new();

    private void Start()
    {
        ratingStats = ratingManager.GetStats();

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

    public void Service(CharacterMove movement, float time, bool isServed)
    {
        charactersInQueue.Remove(movement);

        int tips = 0;

        if (time > 30 && isServed)
        {
            movement.EmojiSender.SendEmoji(EmojiType.Heart);
            ratingStats.PerfectReviews++;
            tips = 3;
        }
        else if (time > 10 && isServed)
        {
            movement.EmojiSender.SendEmoji(EmojiType.Happy);
            ratingStats.GoodReviews++;
            tips = 1;
        }
        else if (time > 1 && isServed)
        {
            movement.EmojiSender.SendEmoji(EmojiType.Upset);
            ratingStats.NormalReviews++;
        }
        else
        {
            movement.EmojiSender.SendEmoji(EmojiType.Angry);
            ratingStats.BadReviews++;
        }

        GiveMoneyWithDelay(tips);

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

        charactersInQueue.Sort((a, b) => a.GetDistanceFromQueue(queuePoints[0].point).CompareTo(b.GetDistanceFromQueue(queuePoints[0].point)));

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
            UpdatePositions();
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
        for (int i = charactersInQueue.Count - 1; i > 0; i--)
        {
            charactersInQueue[i].ContinueWalking();
            charactersInQueue.RemoveAt(i);
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
    private async void GiveMoneyWithDelay(int amount)
    {
        await Task.Delay(2500);
        if (amount > 0) 
        bank.Change(amount);
    }
}

[System.Serializable]
public class QueuePoint
{
    public Transform point;
    public bool isAvailable;
}
