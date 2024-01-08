using UnityEngine;

public class FollowPath6 : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float totalTimeToReachEnd = 10f; // Adjust this to set the total time for movement

    private float t = 0f;
    private float pathLength;

    void Start()
    {
        if (lineRenderer != null)
        {
            CalculatePathLength();
        }
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            MoveAlongPath();
        }
    }

    void CalculatePathLength()
    {
        pathLength = 0f;

        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            pathLength += Vector3.Distance(lineRenderer.GetPosition(i - 1), lineRenderer.GetPosition(i));
        }
    }

    void MoveAlongPath()
    {
        t += Time.deltaTime / totalTimeToReachEnd;

        if (t > 1f)
        {
            t = 1f;
        }

        float distanceCovered = t * pathLength;
        
        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            Vector3 startPoint = lineRenderer.GetPosition(i - 1);
            Vector3 endPoint = lineRenderer.GetPosition(i);
            float segmentLength = Vector3.Distance(startPoint, endPoint);

            if (distanceCovered <= segmentLength)
            {
                // Interpolate within this segment
                float tSegment = distanceCovered / segmentLength;
                transform.localPosition = Vector3.Lerp(startPoint, endPoint, tSegment);
                break;
            }
            else
            {
                // Move to the next segment
                distanceCovered -= segmentLength;
            }
        }

        if (t == 1f)
        {
            // The object has reached the end of the path, you can handle what to do next.
        }
    }
}
