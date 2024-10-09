using UnityEngine;

[CreateAssetMenu(menuName = "Create/Device", fileName = "Device")]
public class DeviceConfig : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
    public int Price;
    public Sprite Icon;
    public string Describtion;
}
