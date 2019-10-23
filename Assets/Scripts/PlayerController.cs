using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public CharacterController characterController;
    public Camera mainCamera;

    public float speed = 6.0f;
    public float airControlModifier = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;

    private float xMoveJumpSnapshot;
    private float zMoveJumpSnapshot;

    void Start()
    {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            xMoveJumpSnapshot = 0;
            zMoveJumpSnapshot = 0;

            Vector3 cameraPosition = mainCamera.transform.position;

            float moveDirectionMedial = this.transform.position.x - mainCamera.transform.position.x;
            float moveDirectionLateral = this.transform.position.z - mainCamera.transform.position.z;

            Vector2 moveDirectionVector = new Vector2(moveDirectionMedial, moveDirectionLateral);
            Vector2 normalizedMoveDirectionVector = moveDirectionVector.normalized;

            moveDirection = (Input.GetAxis("Vertical") * new Vector3(normalizedMoveDirectionVector.x, 0.0f, normalizedMoveDirectionVector.y)) + (-Input.GetAxis("Horizontal") * new Vector3(-normalizedMoveDirectionVector.y, 0.0f, normalizedMoveDirectionVector.x));

            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                xMoveJumpSnapshot = characterController.velocity.x;
                zMoveJumpSnapshot = characterController.velocity.z;
            }
        }
        else {
            Vector3 cameraPosition = mainCamera.transform.position;

            float moveDirectionMedial = this.transform.position.x - mainCamera.transform.position.x;
            float moveDirectionLateral = this.transform.position.z - mainCamera.transform.position.z;

            Vector2 moveDirectionVector = new Vector2(moveDirectionMedial, moveDirectionLateral);
            Vector2 normalizedMoveDirectionVector = moveDirectionVector.normalized;

            if(xMoveJumpSnapshot > 0) {
                moveDirection.x = Mathf.Min(((Input.GetAxis("Vertical") * normalizedMoveDirectionVector.x + Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.y) * airControlModifier + xMoveJumpSnapshot), xMoveJumpSnapshot);
            }
            if(xMoveJumpSnapshot < 0) {
                moveDirection.x = Mathf.Max(((Input.GetAxis("Vertical") * normalizedMoveDirectionVector.x + Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.y) * airControlModifier + xMoveJumpSnapshot), xMoveJumpSnapshot);
            }
            if(xMoveJumpSnapshot == 0)
            {
                moveDirection.x = (Input.GetAxis("Vertical") * normalizedMoveDirectionVector.x + Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.y) * airControlModifier + xMoveJumpSnapshot;
            }
            if(zMoveJumpSnapshot > 0) {
                moveDirection.z = Mathf.Min(((Input.GetAxis("Vertical") * normalizedMoveDirectionVector.y - Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.x) * airControlModifier + zMoveJumpSnapshot), zMoveJumpSnapshot);
            }
            if(zMoveJumpSnapshot < 0) {
                moveDirection.z = Mathf.Max(((Input.GetAxis("Vertical") * normalizedMoveDirectionVector.y - Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.x) * airControlModifier + zMoveJumpSnapshot), zMoveJumpSnapshot);
            }
            if(zMoveJumpSnapshot == 0)
            {
                moveDirection.z = (Input.GetAxis("Vertical") * normalizedMoveDirectionVector.y - Input.GetAxis("Horizontal") * normalizedMoveDirectionVector.x) * airControlModifier + zMoveJumpSnapshot;
            }
        }
       
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }
}
