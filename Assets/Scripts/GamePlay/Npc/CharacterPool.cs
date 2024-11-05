using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour 
{
    private GameObject[] characters;
    private List<GameObject> charactersPool = new();

    private void Awake()
    {
        characters = Resources.LoadAll<GameObject>("Characters");
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            var character = Instantiate(characters[i]);
            charactersPool.Add(character);
            character.SetActive(false);
        }
    }

    public GameObject TryGetCharacter(Transform transform)
    {
        for(int i = 0;i < charactersPool.Count;i++)
        {
            if (!charactersPool[i].activeInHierarchy)
            {
                var character = charactersPool[i];
                character.transform.SetParent(transform);
                character.transform.localPosition = Vector3.zero;


                return character;
            }
        }
        return null;
    }

    public List<GameObject> GetPool()
    {
        return charactersPool;
    }
}
