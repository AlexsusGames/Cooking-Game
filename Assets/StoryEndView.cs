using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryEndView : MonoBehaviour
{
    private const string KEY = "EndStoryKey";
    [SerializeField] private StoryEndSlideView slide;

    public bool isSkiped;

    public async void ShowEndStory(StoryEndConfig config)
    {
        if(!PlayerPrefs.HasKey(KEY))
        {
            gameObject.SetActive(true);
            for (int i = 0; i < config.storyEndSlides.Count; i++)
            {
                slide.SetData(config.storyEndSlides[i]);

                while (!isSkiped)
                {
                    await Task.Yield();
                }
            }

            PlayerPrefs.SetInt(KEY, config.storyEndSlides.Count);
            SceneManager.LoadScene(0);
        }
    }
}
