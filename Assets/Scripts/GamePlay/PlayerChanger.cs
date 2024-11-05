using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerChanger : MonoBehaviour
{
    [SerializeField] private GameObject maleObject;
    [SerializeField] private GameObject femeleObject;
    [SerializeField] private Animator maleAnimator;
    [SerializeField] private Animator femaleAnimator;

    private MoveController moveController;
    private ObjectHandler objectHandler;

    private void Awake()
    {
        moveController = GetComponent<MoveController>();
        objectHandler = GetComponent<ObjectHandler>();
    }

    public async void SetPlayer(bool isMale)
    {
        await Task.Yield();

        if (isMale)
        {
            moveController.Init(maleAnimator);
            objectHandler.ChangeCharacter(0);
        }
        else
        {
            moveController.Init(femaleAnimator);
            objectHandler.ChangeCharacter(1);
        }

        maleObject.SetActive(isMale);
        femeleObject.SetActive(!isMale);
    }
}
