using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawScript_Parabola : MonoBehaviour
{

    private LineRenderer lineRenderer;
    //private EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {

        int position = 0;
        lineRenderer = GetComponent<LineRenderer>();
    //    edgeCollider = GetComponent<EdgeCollider2D>();
        lineRenderer.positionCount = 100;
    //    Vector2[] colliderPoints = new Vector2[100];
        Vector3[] points = new Vector3[100];
        float h = 0;
        float k = 0;
        for (float i = -10.0f; i <= 10.0f; i += 0.2f)
        {
            if (position < 100)
            {
                points[position] = new Vector3(i+h, (i * i) + k, 0);
    //            colliderPoints[position] = new Vector2(i+h + xOffset, (i * i) + k +yOffset);

                position = position + 1;
            }
            
        }

        lineRenderer.SetPositions(points);
       // edgeCollider.points = colliderPoints;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
