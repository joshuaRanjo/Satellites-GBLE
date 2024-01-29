using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class EquationController : MonoBehaviour
{
    [Header("Text Boxes")]
    [SerializeField] private TEXDraw equationText;

    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;

    private int equationType = 0;
    public bool isVertical = true;

    // Equation Variables
/*
    private float h = 0;
    private float k = 0;
    private float a = 1;
    private float b = 1;
*/
#region EVENT_LISTENERS


    private void OnEnable()
    {
        EventManager.StartListening("NewQuestion", ResetVariables);
        EventManager.StartListening("QuestionCompleted", ResetVariables);

        lineDataScriptableObject.dataChangeEvent.AddListener(UpdateEquation);

    }

        private void OnDisable()
    {
        EventManager.StopListening("NewQuestion", ResetVariables);
        EventManager.StopListening("QuestionCompleted", ResetVariables);

        lineDataScriptableObject.dataChangeEvent.RemoveListener(UpdateEquation);
    } 

#endregion

    private void Start() {
        equationType = 0;
    }

    public void SetEquationType(int type)
    {
        equationType = type;
        
        //UpdateEquation();
    }

    // Update h and k on drag
    /*
    public void DragUpdateHK(float newH, float newK)
    {
        h = newH;
        k = newK;
        UpdateEquation();
    }
    */
    // Reset values / set up new equation
    public void ResetVariables()
    {
        /*
        a = 1f;
        b = 1f;
        h = 0f;
        k = 0f;
        */
        isVertical = true;
        equationText.text = "\\uparrow \\uparrow \\uparrow conic not selected \\uparrow \\uparrow \\uparrow";
    }
/*
    public void UpdateH(float newValue)
    {
        h = newValue;
        UpdateEquation();
    }

    public void UpdateK(float newValue)
    {
        k = newValue;
        UpdateEquation();
    }

    public void UpdateA(float newValue)
    {
        a = newValue;
        UpdateEquation();
    }

    public void UpdateB(float newValue)
    {
        b = newValue;
        UpdateEquation();
    }
*/
    private void UpdateEquation()
    {
        switch (lineDataScriptableObject.type)
        {
            
            case "Circle":
                CircleUpdate();
                break;
            case "Parabola":
                ParabolaUpdate();
                break;   
            case "Ellipse":
                EllipseUpdate();
                break;
            case "Hyperbola":
                HyperbolaUpdate();
                break;    
            default:
                Debug.Log("Equation controller: Equation Type not properly set equation controller");    
                break;
        }
    }

    private void ParabolaUpdate()
    {
        //\[y=(x-5)^2+5\]
        //Debug.Log("Equation Updated P");
        equationText.text = "\\[y=" + lineDataScriptableObject.a + "(x-" + lineDataScriptableObject.h + ")^2+"+ lineDataScriptableObject.k +"\\]";
    }

    private void CircleUpdate()
    {
        //Debug.Log("Equation Updated C");
        equationText.text = "\\[(x-" + lineDataScriptableObject.h + ")^2+(y-" + lineDataScriptableObject.k + ")^2=" + lineDataScriptableObject.a + "^2\\]";
    }

    private void EllipseUpdate()
    {
        //Debug.Log("Equation Updated E");
        equationText.text = "\\[\\frac{(x-" + lineDataScriptableObject.h + ")^2}{" + lineDataScriptableObject.a +"^2} + \\frac{(y-" + lineDataScriptableObject.k + ")^2}{" + lineDataScriptableObject.b + "^2} = 1\\]";
    }

    private void HyperbolaUpdate()
    {
        //Debug.Log("Equation Updated H");
        equationText.text = "\\[\\frac{(x-" + lineDataScriptableObject.h + ")^2}{" + lineDataScriptableObject.a +"^2} - \\frac{(y-" + lineDataScriptableObject.k + ")^2}{" + lineDataScriptableObject.b + "^2} = 1\\]";
    }
    
}
