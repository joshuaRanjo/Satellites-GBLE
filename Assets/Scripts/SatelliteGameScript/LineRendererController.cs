using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererController : MonoBehaviour
{
    [Header("Draggable Line")]
    [SerializeField] private GameObject draggableLine;
        
    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;

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

        lineDataScriptableObject.dataChangeEvent.AddListener(UpdateLine);
    }

        private void OnDisable()
    {
        EventManager.StopListening("NewQuestion", ResetLine);
        EventManager.StopListening("QuestionCompleted", ResetLine);

        lineDataScriptableObject.dataChangeEvent.RemoveListener(UpdateLine);
    } 

#endregion

    void Start()
    {
        if(hyperbola == null)
        {
            Debug.Log("Hyperbola object variable is null");
        }
    }

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
        //UpdateLine();
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
        bool newIsVertical;
        string orientationString = lineDataScriptableObject.orientation;
            if(orientationString == "Vertical")
            {
                newIsVertical = true;
            }
            else
            {
                newIsVertical = false;
            }
        if(a != lineDataScriptableObject.a || b != lineDataScriptableObject.b ||  isVertical != newIsVertical)
        {
            a = lineDataScriptableObject.a;
            b = lineDataScriptableObject.b;
            isVertical = newIsVertical;
            
            if (lineDataScriptableObject.type == "Parabola")
            {
                UpdateParabola();
            }
            else if (lineDataScriptableObject.type == "Circle")
            {
                UpdateCircle();
            }
            else if (lineDataScriptableObject.type == "Ellipse")
            {
                UpdateEllipse();
            }
            else if (lineDataScriptableObject.type == "Hyperbola")
            {
                UpdateHyperbola();
            }
            else
            {
                Debug.Log("LineRendererController: No Valid LineType set");
            }
        }



    }

    private void UpdateParabola()
    {
       parabolaScript.DrawParabola(lineDataScriptableObject.a, isVertical);
    }

    private void UpdateCircle()
    {
        circleScript.DrawCircle(lineDataScriptableObject.a);
    }

    private void UpdateEllipse()
    {
        
        ellipseScript.DrawEllipse(lineDataScriptableObject.a, lineDataScriptableObject.b);
    }

    private void UpdateHyperbola()
    {
        
        hyperbolaScript.DrawHyperbola(lineDataScriptableObject.a, lineDataScriptableObject.b, lineDataScriptableObject.orientation);
    }
}
