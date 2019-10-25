using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FiringController: MonoBehaviour
{
    public Camera mainCamera;
    public GameObject cameraTarget;

    public GameObject prefabCube;

    RaycastHit hit;
    Vector3 hitPosition;
    string hitObjectName;

    Vector3 mapVector3(Vector3 originalVector3, Func<float, float> mapFunction) {
        return new Vector3(
            mapFunction(originalVector3.x),
            mapFunction(originalVector3.y),
            mapFunction(originalVector3.z)
        );
    }

    void Update() {

        Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(cameraTarget.transform.position));

        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit)) {
                hitPosition = hit.point;
                hitObjectName = hit.collider.gameObject.name;

                if (hitObjectName == "Ground") {
                    print(hitPosition);
                    Vector3 flooredHitPosition = mapVector3(hitPosition, x => Mathf.Floor(x));
                    Instantiate(prefabCube, mapVector3(flooredHitPosition, value => value + .5f), Quaternion.identity);
                }
            }
        }
    }
}
