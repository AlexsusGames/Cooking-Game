using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryEndConfig", menuName = "Create/StoryEndConfig")]
public class StoryEndConfig : ScriptableObject
{
    public List<DialogData> storyEndSlides;
}
