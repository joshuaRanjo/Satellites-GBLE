using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    [Header("Draggable Line")]
    [SerializeField] private GameObject draggableLine;

    [Header("Line Renderer Objects")]
    [SerializeField] private GameObject parabola;
    [SerializeField]  private GameObject circle;
    [SerializeField] private GameObject ellipse;
    [SerializeField] private GameObject hyperbola;

    [Header("Line Renderer Scripts")]
    [SerializeField] private ParabolaRendererDef parabolaScript;
    [SerializeField]  private CircleRenderer circleScript;
    [SerializeField] private EllipseRenderer ellipseScript;
    [SerializeField] private HyperbolaRenderer hyperbolaScript;

    [Header("Satellite Controller")]
    [SerializeField] private SatelliteController satelliteController;
    

    //variables
    private float a;
    private float b;
    private bool isVertical = true; // only important for parabola and hyperbola
    private string lineType = "Not Set";

    private GameObject currentLine;

#region EVENT_LISTENERS


    private void OnEnable()
    {
        EventManager.StartListening("NewQuestion", ResetLine);
        EventManager.StartListening("QuestionCompleted", ResetLine);

    }

        private void OnDisable()
    {
        EventManager.StopListening("NewQuestion", ResetLine);
        EventManager.StopListening("QuestionCompleted", ResetLine);

    } 

#endregion

    private void ResetLine()
    {
        if(currentLine != null)
        {
            currentLine.SetActive(false);
        }   
        
        currentLine = null;

        EnableDraggableLine(false);
    }

    public void EnableDraggableLine(bool isActive)
    {
        draggableLine.SetActive(isActive);
    }

    public void ChangeActiveLine(string type)
    {
        
        lineType = type;

        // Default settings when selecting a line
        a = 1f;
        b = 1f;
        isVertical = true;
        
        if(currentLine != null)
        {
            currentLine.SetActive(false);
        }

        draggableLine.transform.localPosition = new Vector3(0,0,0);

        if (lineType == "Parabola")
        {
            currentLine = parabola;
        }
        else if (lineType == "Circle")
        {
            currentLine = circle;
        }
        else if (lineType == "Ellipse")
        {
            currentLine = ellipse;
        }
        else if (lineType == "Hyperbola")
        {
            currentLine = hyperbola;
        }
        else
        {
            Debug.Log("LineRendererController: No Valid LineType set");
        }

        currentLine.SetActive(true);
        UpdateLine();
        satelliteController.SetLineRenderer(currentLine);
    }

    public void SetA(float newA)
    {
        a = newA;
        UpdateLine();
    }

    public void SetB(float newB)
    {
        b = newB;
        UpdateLine();
    }

    public void SetIsVertical(bool newIsVertical)
    {
        isVertical = newIsVertical;
        UpdateLine();
    }

    private void UpdateLine()
    {
        if (lineType == "Parabola")
        {
            UpdateParabola();
        }
        else if (lineType == "Circle")
        {
            UpdateCircle();
        }
        else if (lineType == "Ellipse")
        {
            UpdateEllipse();
        }
        else if (lineType == "Hyperbola")
        {
            UpdateHyperbola();
        }
        else
        {
            Debug.Log("LineRendererController: No Valid LineType set");
        }

    }

    private void UpdateParabola()
    {
       parabolaScript.DrawParabola(a, isVertical);
    }

    private void UpdateCircle()
    {
        circleScript.DrawCircle(a);
    }

    private void UpdateEllipse()
    {

    }

    private void UpdateHyperbola()
    {

    }
}
