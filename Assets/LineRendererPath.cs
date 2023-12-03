using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererPath : MonoBehaviour
{
public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.positionCount = transform.childCount;

        for (int i = 0; i < transform.childCount; i++)
        {
            lineRenderer.SetPosition(i, transform.GetChild(i).position);
        }
    }
}
