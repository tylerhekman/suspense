using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetController : MonoBehaviour
{

    public new Camera camera;
    public GameObject player;

    public float cameraTargetHeightModifier = .4f;

    private float cameraTargetOffset = 1;

    public void setCameraTargetOffset(float offset)
    {
        cameraTargetOffset = offset;
    }

    public float getCameraTargetOffset()
    {
        return cameraTargetOffset;
    }
    
    void Start()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        Vector3 cameraPosition = camera.transform.position;

        float moveDirectionMedial = player.transform.position.x - camera.transform.position.x;
        float moveDirectionLateral = player.transform.position.z - camera.transform.position.z;

        Vector2 moveDirectionVector = new Vector2(moveDirectionMedial, moveDirectionLateral);
        Vector2 normalizedMoveDirectionVector = moveDirectionVector.normalized;

        Vector2 perpendicularVector = new Vector2(normalizedMoveDirectionVector.y, -normalizedMoveDirectionVector.x);
        this.transform.position = new Vector3(
            player.transform.position.x + perpendicularVector.x * cameraTargetOffset, 
            player.transform.position.y + cameraTargetHeightModifier,
            player.transform.position.z + perpendicularVector.y * cameraTargetOffset
        );
    }

    void LateUpdate()
    {

    }
}
