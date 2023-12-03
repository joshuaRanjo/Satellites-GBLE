    using UnityEngine;
using System; //For Math.Abs
using System.Collections.Generic; //For Lists


[RequireComponent(typeof(LineRenderer))]
public class ParabolaRenderer : MonoBehaviour
{   

    [Header("Parabola Parameters")]
    public float a = 1f; // Coefficient 'a' from the parabola equation
    public float h = 0f; // X-coordinate of the vertex
    public float k = 0f; // Y-coordinate of the vertex
    public int vertexCount = 1000; // Number of points to draw
    public int positionLimit = 200; // Number of Positions on Final line

    [Header("Parabola Length Limiters")]

    public int limitX = 44;
    public int limitY = 44;

    [Header("Parabola Limiters")]
    public float xMin = -5f; // Minimum x-coordinate
    public float xMax = 5f; // Maximum x-coordinate
    public float yMin = -5f; // Minimum y-coordinate
    public float yMax = 5f; // Maximum y-coordinate

    private LineRenderer lineRenderer;

    private void Start()
    {
        RenderParabola(a,h,k,false);
    }

    public void RenderParabola(float newA, float newH, float newK, bool isVertical)
    {
        a = newA;
        h = newH;
        k = newK;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = vertexCount;

        List<Vector3> positionList = new List<Vector3>();

        if(isVertical) 
        {
            float xStep = (xMax - xMin) / (vertexCount - 1);
            

            for (int i = 0 + positionLimit; i < vertexCount - positionLimit; i++)
            {
                float x = xMin + i * xStep;
                float y = a * Mathf.Pow(x - h, 2) + k;            // (y-k) = a(x-h)^2 
                //float y = (Mathf.Pow(x-h,2) / (4*a)) + k;           //(x-h)^2=4a(y-k)


                if(Math.Abs(x) <=limitX && Math.Abs(y) <= limitY)
                    positionList.Add(new Vector3(x, y, 0f));
            }

            Vector3[] positionsArray = positionList.ToArray();
            lineRenderer.positionCount = positionsArray.Length;
            lineRenderer.SetPositions(positionsArray);
        }
        else
        {
            float yStep = (yMax - yMin) / (vertexCount - 1);
            

            for (int i = 0 + positionLimit; i < vertexCount - positionLimit; i++)
            {
                float y = yMin + i * yStep;
                float x = a * Mathf.Pow(y - k, 2) + h;            //(x-h)=a(y-k)^2
                //float x = (Mathf.Pow(y-k,2) / (4*a)) + h;          //

                if(Math.Abs(x) <=limitX && Math.Abs(y) <= limitY)
                    positionList.Add(new Vector3(x, y, 0f));
            }            

            Vector3[] positionsArray = positionList.ToArray();
            lineRenderer.positionCount = positionsArray.Length;
            lineRenderer.SetPositions(positionsArray);
        }

        
    }
}
