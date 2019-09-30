using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //HELPER FUNCTIONS
    private float toRadian(float degree) {
        return degree * Mathf.PI / 180;
    }

    public GameObject player;
    public CameraTargetController cameraTarget;

    private float maxRadius = 5.0f;
    private float angle = 270;
    private float cameraHeight = 2.0f;
    private float backgroundHeightTracker = 0;
    private float radius;
    private float rotationalSensitivity = 4.0f;
    private float verticalSensitivityDown = 0.5f;
    private float verticalSensitivityUp;

    private float maxVerticalSensitivityUp = 16;

    void Start()
    {
        verticalSensitivityUp = verticalSensitivityDown * 2.0f;
        maxVerticalSensitivityUp = 0;
    }

    void Update()
    {
        angle -= Input.GetAxis("Mouse X") * rotationalSensitivity;

        float dyUp = Mathf.Clamp((Input.GetAxis("Mouse Y") * Mathf.Abs(Input.GetAxis("Mouse Y"))) * verticalSensitivityUp, -32.0f, 32.0f);

        if (cameraHeight == 0.1f && backgroundHeightTracker <= 0)
        {
            backgroundHeightTracker -= dyUp;
            if(backgroundHeightTracker < -20)
            {
                backgroundHeightTracker = -20;
            } else
            {
                cameraTarget.cameraTargetHeightModifier += dyUp;
            }
        }
        else
        {
            backgroundHeightTracker = 0;
            cameraTarget.cameraTargetHeightModifier = .4f;
            cameraHeight -= Input.GetAxis("Mouse Y") * verticalSensitivityDown;
            cameraHeight = Mathf.Clamp(cameraHeight, 0.1f, 4.5f);
            radius = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(maxRadius, 2) - Mathf.Pow(cameraHeight, 2)), 2, 5);
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.transform.position.x + (radius * Mathf.Cos(toRadian(angle))),
            player.transform.position.y + cameraHeight,
            player.transform.position.z + (radius * Mathf.Sin(toRadian(angle)))
        );
        this.transform.LookAt(cameraTarget.transform);
    }
}
