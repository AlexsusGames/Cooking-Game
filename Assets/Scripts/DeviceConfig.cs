using UnityEngine;

[CreateAssetMenu(menuName = "Create/Device", fileName = "Device")]
public class DeviceConfig : SoLocalization
{
    public GameObject Prefab;
    public string Name;
    public int Price;
    public Sprite Icon;
    public string Describtion;

    public override string[] Get()
    {
        return new string[] { Name };
    }

    public override void Set(params string[] param)
    {
        Name = param[0];
    }
}
