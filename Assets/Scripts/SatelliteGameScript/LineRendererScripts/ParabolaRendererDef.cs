using UnityEngine;
using System.Collections.Generic;


public class ParabolaRendererDef : MonoBehaviour
{
    public float a = 1.0f;  // The 'a' coefficient in the y = ax^2 equation
    public int pointCount = 100;  // Number of points to draw the parabola
    public float maxX = 5.0f;  // Maximum x value
    public float maxY = 5.0f;  // Maximum y value

    [SerializeField] private LineRenderer lineRenderer;
    private bool isVertical = true;

    private void Start()
    {
        
    }

    public void DrawParabola()
    {
        List<Vector3> points = new List<Vector3>();


        float xStep = (maxX - -maxX) / (pointCount - 1);
        // If vertical Axis
        for (int i = 0; i < pointCount; i++)
        {
            float x = -maxX + i * xStep; // Vary 'x' from -1 to 1
            float y = a * x * x;

            if ( y >= -maxY && y <= maxY)
            {
                Vector3 point = new Vector3(x, y, 0);
                points.Add(point);
            }
            
        }

        int verticalModifier = 1;
        if(a < 0)
        {
            verticalModifier = -1;
        }

        float lastX = Mathf.Sqrt(verticalModifier*maxY/a);
        points.Insert(0, new Vector3(-lastX,maxY*verticalModifier,0));
        points.Add(new Vector3(lastX,maxY*verticalModifier,0));
        

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void DrawParabola(float newA, bool newIsVertical)
    {
        a = newA;
        isVertical = newIsVertical;

        DrawParabola();
    }
    
}
