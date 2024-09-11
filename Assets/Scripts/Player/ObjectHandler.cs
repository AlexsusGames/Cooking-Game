using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField] private Transform dishPlace;
    [SerializeField] private Transform cupPlace;
    [SerializeField] private Animator animator;

    private GameObject current;

    public void ChangeObject(GameObject gameObject = null, bool isCup = false)
    {
        current = gameObject;

        if(gameObject == null)
        {
            animator.SetLayerWeight(1, 0);
            return;
        }

        Transform parent = isCup ? cupPlace : dishPlace;
        gameObject.transform.SetParent(parent);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        animator.SetLayerWeight(1, 1);
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
