using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StoryEndView : MonoBehaviour
{
    private const string KEY = "endStory_Key";
    [SerializeField] private StoryEndSlideView slide;
    [SerializeField] private GameObject titre;
    [Inject] private SteamAchievements achievements;

    private bool isSkiped;


    public void Quit()
    {
        SceneManager.LoadScene(0);
    }

    public bool isGamePassed()
    {
        return PlayerPrefs.HasKey(KEY);
    }

    public async void ShowEndStory(StoryEndConfig config)
    {
        if (!PlayerPrefs.HasKey(KEY))
        {
            achievements.TrySetAchievement(achievements.ACH_STORY);

            gameObject.SetActive(true);
            for (int i = 0; i < config.storyEndSlides.Count; i++)
            {
                Cursor.visible = true;
                slide.SetData(config.storyEndSlides[i]);
                isSkiped = false;

                while (!isSkiped)
                {
                    await Task.Yield();
                }
            }

            PlayerPrefs.SetInt(KEY, config.StoryId);
            titre.SetActive(true);
        }
    }

    public int GetStoryEndId() => PlayerPrefs.GetInt(KEY);

    public void Skip() => isSkiped = true;
}
