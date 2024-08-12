using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField] private Transform gameObjectHandler;

    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void ChangeObject(GameObject gameObject = null)
    {
        if(gameObject == null)
        {
            GetRidOfLastObject();
            animator.SetLayerWeight(1, 0);
            return;
        }

        GetRidOfLastObject();

        gameObject.transform.SetParent(gameObjectHandler);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        animator.SetLayerWeight(1, 1);
    }

    private void GetRidOfLastObject()
    {
        if(gameObjectHandler.childCount != 0)
        {
            Destroy(gameObjectHandler.GetChild(0).gameObject);
        }
    }

    public GameObject GetObject()
    {
        if (gameObjectHandler.childCount != 0)
        {
            return gameObjectHandler.GetChild(0).gameObject;
        }
        return null;
    }
}
