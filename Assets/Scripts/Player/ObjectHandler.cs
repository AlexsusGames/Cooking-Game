using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField] private Transform dishHandler;
    [SerializeField] private Transform cupHandler;
    [SerializeField] private Animator animator;

    private GameObject current;

    public void ChangeObject(GameObject gameObject = null, bool isCup = false)
    {
        GetRidOfLastObject();
        current = gameObject;

        if(gameObject == null)
        {
            animator.SetLayerWeight(1, 0);
            return;
        }

        Transform parent = isCup ? cupHandler : dishHandler;
        Vector3 offset = isCup ? new Vector3(-90, 0, 0) : Vector3.zero;
        gameObject.transform.SetParent(parent);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.Euler(offset);
        animator.SetLayerWeight(1, 1);
    }

    private void GetRidOfLastObject()
    {
        Destroy(current);
    }

    public GameObject GetObject()
    {
        if (current == null)
            return null;

        return current;
    }
}
