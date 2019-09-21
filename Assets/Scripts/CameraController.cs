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

    private float radius = 5.0f;
    private float angle = 270;
    private float cameraHeight = 2.0f;
    private float backgroundHeightTracker = 0;
    private float foo;
    private float rotationalSensitivity = 4.0f;
    private float verticalSensitivity = 1.0f;

    void Start()
    {
        print(cameraTarget.getCameraTargetOffset());
    }

    void Update()
    {
        angle -= Input.GetAxis("Mouse X") * rotationalSensitivity;

        if(cameraHeight == 0.1f && backgroundHeightTracker <= 0)
        {
            backgroundHeightTracker -= Input.GetAxis("Mouse Y") * verticalSensitivity;
            if(backgroundHeightTracker < -20)
            {
                backgroundHeightTracker = -20;
            } else
            {
                cameraTarget.cameraTargetHeightModifier += Input.GetAxis("Mouse Y");
            }
        }
        else
        {
            backgroundHeightTracker = 0;
            cameraTarget.cameraTargetHeightModifier = .4f;
            cameraHeight -= Input.GetAxis("Mouse Y") * verticalSensitivity;
            cameraHeight = Mathf.Clamp(cameraHeight, 0.1f, 4.5f);
            foo = Mathf.Clamp(Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(cameraHeight, 2)), 2, 5);
        }
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            player.transform.position.x + (foo * Mathf.Cos(toRadian(angle))),
            player.transform.position.y + cameraHeight,
            player.transform.position.z + (foo * Mathf.Sin(toRadian(angle)))
        );
        this.transform.LookAt(cameraTarget.transform);
    }
}
