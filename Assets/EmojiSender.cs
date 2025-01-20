using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EmojiSender : MonoBehaviour
{
    [SerializeField] private Image emojiImage;
    [SerializeField] private Sprite[] emojiArray;
    [SerializeField] private GameObject container;

    private LookAtConstraint cameraHelper;

    private void Awake()
    {
        cameraHelper = GetComponent<LookAtConstraint>();
        if(cameraHelper.sourceCount == 0)
        {
            var cameraTransform = Camera.main.transform;
            cameraHelper.AddSource(new ConstraintSource { sourceTransform = cameraTransform, weight = 1 });
        }
    }

    private bool isSended;

    private void Send(int type)
    {
        isSended = true;
        container.SetActive(true);
        emojiImage.sprite = emojiArray[type];
    }

    private IEnumerator SendImage(int type)
    {
        Send(type);
        yield return new WaitForSeconds(2);
        container.SetActive(false);
        isSended = false;
    }

    public void SendEmoji(EmojiType type)
    {
        if(!isSended)
        {
            StartCoroutine(SendImage((int)type));
        }
    }
}
public enum EmojiType
{
    Heart,
    Happy,
    Sleap,
    Angry,
    Upset
}
