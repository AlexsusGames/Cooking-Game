using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryEndConfig", menuName = "Create/StoryEndConfig")]
public class StoryEndConfig : SoLocalization
{
    public List<DialogData> storyEndSlides;
    public int StoryId;

    public override string[] Get()
    {
        if (CachedKeys == null)
        {
            List<string> list = new();

            for (int i = 0; i < storyEndSlides.Count; i++)
            {
                list.Add(storyEndSlides[i].Message);
            }

            CachedKeys = list.ToArray();
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        for (int i = 0; i < storyEndSlides.Count; i++)
        {
            int index = i;
            storyEndSlides[index].Message = param[index];
        }
    }
}
