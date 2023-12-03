using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath2 : MonoBehaviour
{   
    public LineRenderer lineRenderer;
    public float speed = 2f;

    private float distanceAlongPath = 0f;

    private void Update()
    {
        distanceAlongPath += speed * Time.deltaTime;

        if (distanceAlongPath > lineRenderer.positionCount - 1)
        {
            // Reached the end of the path
            distanceAlongPath = lineRenderer.positionCount - 1;
        }

        int currentSegment = Mathf.FloorToInt(distanceAlongPath);
        float segmentFraction = distanceAlongPath - currentSegment;

        Vector3 startPosition = lineRenderer.GetPosition(currentSegment);
        Vector3 endPosition = lineRenderer.GetPosition(currentSegment + 1);

        Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, segmentFraction);
        transform.position = targetPosition;
    }
}
