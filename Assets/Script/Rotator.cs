using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 50f;
    private Vector3 centerPoint;

    void Start()
    {
        // Get the center of the object's bounding box from its MeshRenderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            centerPoint = renderer.bounds.center;
        }
        else
        {
            // Fallback to the object's transform position if no renderer is found
            centerPoint = transform.position;
        }
    }

    void Update()
    {
        // Rotate the object around its calculated center point
        transform.RotateAround(centerPoint, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}