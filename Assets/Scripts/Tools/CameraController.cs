using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    [Inject] private WindowController windowController;
    public float minOffset;
    public float maxOffset;
    public float speed;

    private CinemachineCameraOffset offsetCamera;

    private void Awake()
    {
        offsetCamera = GetComponent<CinemachineCameraOffset>();
    }

    private void Update()
    {
        if(!windowController.HasOpenedWindows())
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                if (offsetCamera.m_Offset.z < minOffset)
                {
                    offsetCamera.m_Offset.z += speed;
                }
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                if (offsetCamera.m_Offset.z > maxOffset)
                {
                    offsetCamera.m_Offset.z -= speed;
                }
            }
        }
    }
}
