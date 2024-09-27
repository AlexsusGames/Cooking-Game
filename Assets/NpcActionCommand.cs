using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NpcActionCommand 
{
    public Transform Target;
    public Vector3 TargetRotation;
    public float distanceOffset;
    public string mainAction;
    public string additionalAction;
    public int MsDelay;
}
