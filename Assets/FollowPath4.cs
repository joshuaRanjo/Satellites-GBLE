using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPath4 : MonoBehaviour
{   
    [Header("Button")]
    [SerializeField] private Button button;
    public LineRenderer lineRenderer;
    public float totalPathLength = 0f;
    public float speed = 2f;

    private float distanceAlongPath = 0f;

    public void ResetPath()
    {
        distanceAlongPath = 0f;
    }


    // Coroutine that makes object move
    public IEnumerator FollowPathCoroutine(System.Action onCompleteCallback)
    {
        CalculateTotalPathLength();
        while (distanceAlongPath < 1f)
        {
            distanceAlongPath += (speed / totalPathLength) * Time.deltaTime;
            
            int currentSegment = GetPathSegmentIndex(distanceAlongPath);
            float segmentFraction = GetSegmentFraction(distanceAlongPath, currentSegment);
            if (currentSegment >= 0 && currentSegment < lineRenderer.positionCount -1) 
            {
                Vector3 startPosition = lineRenderer.GetPosition(currentSegment);
                Vector3 endPosition = lineRenderer.GetPosition(currentSegment + 1);

                Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, segmentFraction);  
                transform.localPosition = targetPosition;
            }

            
            yield return null;
        }

        // loop? 
        //StartCoroutine(LoopFollowPath());
        onCompleteCallback?.Invoke();
    }

    private IEnumerator LoopFollowPath()
    {
        CalculateTotalPathLength();
        while(true)
        {   
            ResetPath();
            while (distanceAlongPath < 1f)
            {
                distanceAlongPath += (speed / totalPathLength) * Time.deltaTime;
                
                int currentSegment = GetPathSegmentIndex(distanceAlongPath);
                float segmentFraction = GetSegmentFraction(distanceAlongPath, currentSegment);
                if (currentSegment >= 0 && currentSegment < lineRenderer.positionCount -1) 
                {
                    Vector3 startPosition = lineRenderer.GetPosition(currentSegment);
                    Vector3 endPosition = lineRenderer.GetPosition(currentSegment + 1);

                    Vector3 targetPosition = Vector3.Lerp(startPosition, endPosition, segmentFraction);  
                    transform.localPosition = targetPosition;
                }


                yield return null;
            }
            ResetPath();
        }
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
        
        if (segmentIndex >= 0 && segmentIndex < lineRenderer.positionCount - 1)
        {
            float segmentLength = segmentEndDistance - segmentStartDistance;
            float segmentFraction = (pathDistance * totalPathLength - segmentStartDistance) / segmentLength;

            return segmentFraction;
        }
        
        return 0f;
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
        if (segmentIndex >= 0 && segmentIndex < lineRenderer.positionCount - 1)
        {
            float distance = 0f;

            for (int i = 0; i <= segmentIndex; i++)
            {
                distance += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
            }

            return distance;
        }

        return 0f;
    }
}
