using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
public LineRenderer lineRenderer;
    public float speed = 5f; // The speed at which the object will move along the path
    private float currentDistance;
    
    void Update()
    {
        // Move the object along the path
        if (currentDistance < lineRenderer.positionCount)
        {
            Vector3 targetPosition = lineRenderer.GetPosition(Mathf.FloorToInt(currentDistance));
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            currentDistance += speed * Time.deltaTime;
        }
    }
}
