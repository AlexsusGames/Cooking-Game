using UnityEngine;

[CreateAssetMenu(menuName = "Create/Device", fileName = "Device")]
public class DeviceConfig : ScriptableObject
{
    public GameObject Prefab;
    public string Name;
}
