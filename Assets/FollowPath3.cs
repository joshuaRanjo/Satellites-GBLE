using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath3 : MonoBehaviour
{
       public LineRenderer lineRenderer;
    public float totalPathLength = 0f;
    public float speed = 2f;

    private float distanceAlongPath = 0f;

    private void Start()
    {
        CalculateTotalPathLength();
    }

    private void Update()
    {
        distanceAlongPath += (speed / totalPathLength) * Time.deltaTime;

        if (distanceAlongPath > 1f)
        {
            // Reached the end of the path
            distanceAlongPath = 1f;
        }

        int currentSegment = GetPathSegmentIndex(distanceAlongPath);
        float segmentFraction = GetSegmentFraction(distanceAlongPath, currentSegment);

        Vector3 startPosition = lineRenderer.GetPosition(currentSegment);
        Vector3 endPosition = lineRenderer.GetPosition(currentSegment + 1);

        Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, segmentFraction);
        transform.position = targetPosition;
    }

    private void CalculateTotalPathLength()
    {
        totalPathLength = 0f;

        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            totalPathLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }
    }

    private int GetPathSegmentIndex(float pathDistance)
    {
        float currentDistance = 0f;
        int segmentIndex = 0;

        while (segmentIndex < lineRenderer.positionCount - 1)
        {
            float segmentLength = Vector3.Distance(lineRenderer.GetPosition(segmentIndex), lineRenderer.GetPosition(segmentIndex + 1));

            if (currentDistance + segmentLength >= pathDistance * totalPathLength)
            {
                break;
            }

            currentDistance += segmentLength;
            segmentIndex++;
        }

        return segmentIndex;
    }

    private float GetSegmentFraction(float pathDistance, int segmentIndex)
    {
        float segmentStartDistance = GetDistanceToSegmentStart(segmentIndex);
        float segmentEndDistance = GetDistanceToSegmentEnd(segmentIndex);

        float segmentLength = segmentEndDistance - segmentStartDistance;
        float segmentFraction = (pathDistance * totalPathLength - segmentStartDistance) / segmentLength;

        return segmentFraction;
    }

    private float GetDistanceToSegmentStart(int segmentIndex)
    {
        float distance = 0f;

        for (int i = 0; i < segmentIndex; i++)
        {
            distance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        return distance;
    }

    private float GetDistanceToSegmentEnd(int segmentIndex)
    {
        float distance = 0f;

        for (int i = 0; i <= segmentIndex; i++)
        {
            distance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }

        return distance;
    }
}
