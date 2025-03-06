using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider mSlider;
    [SerializeField] private Slider sSlider;
    [Inject] private SoundDataProvider dataProvider;
    private SoundData data;

    private void Awake()
    {
        mSlider.onValueChanged.AddListener(ChangeMusic);
        sSlider.onValueChanged.AddListener(ChangeSound);
    }

    private void OnEnable()
    {
        data = dataProvider.GetData();
        mSlider.value = data.Music;
        sSlider.value = data.Sound;
    }

    public void ChangeMusic(float newValue)
    {
        data.Music = newValue;
        dataProvider.ChangeData(data);
    }

    public void ChangeSound(float newValue)
    {
        data.Sound = newValue;
        dataProvider.ChangeData(data);
    }
}
