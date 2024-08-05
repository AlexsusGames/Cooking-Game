using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minOffset;
    public float maxOffset;

    private CinemachineCameraOffset offset;

    private void Awake()
    {
        offset = GetComponent<CinemachineCameraOffset>();
    }

    private void Update()
    {
        if(Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if(offset.m_Offset.z < minOffset)
            {
                offset.m_Offset.z += 1f;
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (offset.m_Offset.z > maxOffset)
            {
                offset.m_Offset.z -= 1f;
            }
        }
    }
}
