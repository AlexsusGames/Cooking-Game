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
        if(CachedKeys == null)
        {
            CachedKeys = new string[] { Name, Describtion };
        }

        return CachedKeys;
    }

    public override void Set(params string[] param)
    {
        Name = param[0];
        Describtion = param[1];
    }
}
