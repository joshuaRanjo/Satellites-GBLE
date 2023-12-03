using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{

    //Inspector Fields
    [SerializeField] private ParabolaEquationGenerator parabolaGen;
    [SerializeField] private GameObject draggableLine;
    [SerializeField] private Button submitButton;
    [SerializeField] private Camera mainCamera;


    [Header("Text Boxes")]
    
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TEXDraw equationText;


    [Header("Vertex Form Inputs")]

    [SerializeField] private TMP_InputField inputA;
    [SerializeField] private TMP_InputField inputB;
    [SerializeField] private TMP_InputField inputH;
    [SerializeField] private TMP_InputField inputK;

    // 
    private int points = 0;
    private int questionNumber = 1;
    private int questionType;
    private void Start()
    {
        points = 0;
        questionNumber = 1;

        SetUpDraggableQuestion();
    }

    public void CheckAnswer()
    {

        if(inputH.text != "" && inputK.text != "")
        {
            float valueH, valueK;

            valueH = float.Parse(inputH.text);
            valueK = float.Parse(inputK.text);

            if(valueH== parabolaGen.valueH && valueK == parabolaGen.valueK)
            {
                Debug.Log("Correct Answer");
                points += 1;
            }
            else
            {
                Debug.Log("Wrong Answer");
            }

            NextQuestion();
            ClearInputs();
            UpdateTextBoxes();
        }
    }

    private void NextQuestion()
    {
        parabolaGen.GenerateVertexEquation();
    }

    private void UpdateTextBoxes()
    {
        pointsText.text = "Points : " + points;
        questionText.text = "asdf";
    }


    private void ClearInputs()
    {
        inputH.text = "";
        inputK.text = "";
    }

    private void SetUpDraggableQuestion()
    {
        //Set up submit button for draggable Question
        submitButton.onClick.AddListener(CheckDraggableQuestion);

        // Create equation
        string equationString = parabolaGen.GenerateVertexEquation();
    
        // Equation Box
        equationText.text = equationString;        
        // Setup Line
        GameObject lineObject = draggableLine.transform.Find("Line").gameObject;

        if(lineObject != null)
        {
            ParabolaRenderer parabolaRenderer = lineObject.GetComponent<ParabolaRenderer>();
            parabolaRenderer.RenderParabola(parabolaGen.valueA,0,0, parabolaGen.valueIsVertical);

            draggableLine.transform.localPosition = new Vector3(0,0,0);
            inputA.text = parabolaGen.valueA.ToString();
            inputH.text = parabolaGen.valueH.ToString();
            inputK.text = parabolaGen.valueK.ToString();

            inputA.interactable = false;
            inputB.interactable = false;
            inputH.interactable = false;
            inputK.interactable = false;
        }
        else
        {
            Debug.LogError("GameScript : Line Object Not found for draggable parabola question");
        }
    }

    private void CheckDraggableQuestion()
    {
        float xPosition = draggableLine.transform.localPosition.x;
        float yPosition = draggableLine.transform.localPosition.y;

        if(parabolaGen.valueH == xPosition && parabolaGen.valueK == yPosition)
        {
            Debug.Log("Correct");
            submitButton.onClick.RemoveListener(CheckDraggableQuestion);
            SetUpDraggableQuestion();

            //Reset Camera
            mainCamera.transform.position = new Vector3(0,0,-10);
        }
        else
        {
            
            Debug.Log("WORGNGNNG");
        }
        
    }
}
