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

    private int equationType = 0;
    public bool isVertical = true;

    // Equation Variables

    private float h = 0;
    private float k = 0;
    private float a = 1;
    private float b = 1;

#region EVENT_LISTENERS


    private void OnEnable()
    {
        EventManager.StartListening("NewQuestion", ResetVariables);
        EventManager.StartListening("QuestionCompleted", ResetVariables);

    }

        private void OnDisable()
    {
        EventManager.StopListening("NewQuestion", ResetVariables);
        EventManager.StopListening("QuestionCompleted", ResetVariables);

    } 

#endregion

    private void Start() {
        equationType = 0;
    }

    public void SetEquationType(int type)
    {
        equationType = type;
        ResetVariables();
        UpdateEquation();
    }

    // Update h and k on drag
    public void DragUpdateHK(float newH, float newK)
    {
        h = newH;
        k = newK;
        UpdateEquation();
    }

    // Reset values / set up new equation
    public void ResetVariables()
    {
        a = 1f;
        b = 1f;
        h = 0f;
        k = 0f;
        isVertical = true;
        equationText.text = "\\uparrow \\uparrow \\uparrow conic not selected \\uparrow \\uparrow \\uparrow";
    }

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

    private void UpdateEquation()
    {
        
        switch (equationType)
        {
            case 1:
                CircleUpdate();
                break;
            case 2:
                ParabolaUpdate();
                break;   
            case 3:
                EllipseUpdate();
                break;
            case 4:
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
        equationText.text = "\\[y=" + a + "(x-" + h + ")^2+"+ k +"\\]";
    }

    private void CircleUpdate()
    {
        equationText.text = "\\[(x-"+h+")^2+(y-"+k+")^2="+a+"^2\\]";
    }

    private void EllipseUpdate()
    {
        equationText.text = "\\[frac{x^2,\\]";
    }

    private void HyperbolaUpdate()
    {

    }
    
}
