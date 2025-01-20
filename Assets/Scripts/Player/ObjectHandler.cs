using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField] private Image heldFoodImage;
    [SerializeField] private Sprite standartFood;

    [SerializeField] private Transform[] dishPlaces;
    [SerializeField] private Transform[] cupPlaces;
    [SerializeField] private Animator[] animators;

    [SerializeField] private EmojiSender emojiSender;

    public EmojiSender EmojiSender { get => emojiSender; }

    private Animator animator;

    private Transform dishPlace;
    private Transform cupPlace;

    private GameObject current;

    public void ChangeCharacter(int id)
    {
        animator = animators[id];
        dishPlace = dishPlaces[id];
        cupPlace = cupPlaces[id];
    }

    public void ChangeObject(GameObject gameObject = null, bool isCup = false)
    {
        current = gameObject;

        if(gameObject == null)
        {
            animator.SetLayerWeight(1, 0);
            heldFoodImage.sprite = standartFood;
            return;
        }

        Transform parent = isCup ? cupPlace : dishPlace;
        gameObject.transform.SetParent(parent);
        gameObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(Vector3.zero));
        animator.SetLayerWeight(1, 1);

        UpdateFoodImage();
    }

    public void UpdateFoodImage()
    {
        if(current.TryGetComponent(out IFood food))
        {
            if (food.GetFood() != null)
                heldFoodImage.sprite = food.GetFood().picture;
        }
    }

    public void GetRidOfLastObject()
    {
        Destroy(current);

        ChangeObject();
    }

    public GameObject GetObject()
    {
        if (current == null)
            return null;

        return current;
    }
}
