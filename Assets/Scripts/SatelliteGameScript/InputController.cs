using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


public class InputController : MonoBehaviour
{

    [Header("Vertex Form Inputs")]

    [SerializeField] private TMP_InputField inputA;
    [SerializeField] private TMP_InputField inputB;
    [SerializeField] private TMP_InputField inputH;
    [SerializeField] private TMP_InputField inputK;

    [Header("Conic Selectors")]
    [SerializeField] private Button circleButton;
    [SerializeField] private Button parabolaButton;
    [SerializeField] private Button ellipseButton;
    [SerializeField] private Button hyperbolaButton;

    [Header("Equation Controller")]
    [SerializeField] private EquationController equationController;

    [Header("Draggable Line")]
    [SerializeField] private MoveLineScript draggableLine;

    [Header("Line Renderer Controller")]
    [SerializeField] private LineRendererController lineRendererController;

    [Header("Submit Button")]
    [SerializeField] private Button submitButton;

    // Default variable values
    private float defaultA = 1;
    private float defaultB = 1;
    private float defaultH = 0;
    private float defaultK = 0;
    

    // Variable Values
    private float valueA = 1;
    private float valueB = 1;
    private float valueH = 0;
    private float valueK = 0;

    private List<bool> toggleList = new List<bool> {false,false,false,false,false,false,false,false};

#region EVENT_LISTENERS

    private UnityAction toggleInteractable;
    
    private void Awake()
    {
        toggleInteractable = new UnityAction(ToggleInteractableForSatellite);
    }

    private void OnEnable()
    {
        EventManager.StartListening("SatelliteMoving", ToggleInteractableForSatellite);
        EventManager.StartListening("SatelliteStopped",ToggleInteractableForSatellite);
        EventManager.StartListening("QuestionCompleted", DisableAll);
        EventManager.StartListening("DraggingVertex", InputStopListening);
        EventManager.StartListening("StoppedDraggingVertex", InputStartListening);
    }

        private void OnDisable()
    {
        EventManager.StopListening("SatelliteMoving", ToggleInteractableForSatellite);
        EventManager.StopListening("SatelliteStopped", ToggleInteractableForSatellite);
        EventManager.StopListening("QuestionCompleted", DisableAll);
        EventManager.StopListening("DraggingVertex", InputStopListening);
        EventManager.StopListening("StoppedDraggingVertex", InputStartListening);
    }

#endregion

    private bool onlyA = true;
    private void Start() {
        InputStartListening();
    }

    private void InputStartListening()
    {
        inputA.onValueChanged.AddListener(UpdateA);
        inputB.onValueChanged.AddListener(UpdateB);
        inputH.onValueChanged.AddListener(UpdateH);
        inputK.onValueChanged.AddListener(UpdateK);

        inputA.onEndEdit.AddListener(UpdateAOnEndEdit);
        inputB.onEndEdit.AddListener(UpdateBOnEndEdit);
        inputH.onEndEdit.AddListener(UpdateHOnEndEdit);
        inputK.onEndEdit.AddListener(UpdateKOnEndEdit);
    }

    private void InputStopListening()
    {
        inputA.onValueChanged.RemoveListener(UpdateA);
        inputB.onValueChanged.RemoveListener(UpdateB);
        inputH.onValueChanged.RemoveListener(UpdateH);
        inputK.onValueChanged.RemoveListener(UpdateK);

        inputA.onEndEdit.RemoveListener(UpdateAOnEndEdit);
        inputB.onEndEdit.RemoveListener(UpdateBOnEndEdit);
        inputH.onEndEdit.RemoveListener(UpdateHOnEndEdit);
        inputK.onEndEdit.RemoveListener(UpdateKOnEndEdit);
    }

    private void UpdateAOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputA.text = "1";
            equationController.UpdateA(1f);
            lineRendererController.SetA(1f);
        }
        
    }

    private void UpdateBOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputB.text = "1";
            equationController.UpdateB(1f);
            lineRendererController.SetB(1f);
        }
    }

    private void UpdateHOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputH.text = "0";
            equationController.UpdateH(0f);
            draggableLine.SetX(0f);
        }
    }

    private void UpdateKOnEndEdit(string newValue)
    {
        if(!float.TryParse(newValue, out _))
        {
            inputK.text = "0";
            equationController.UpdateK(0f);
            draggableLine.SetY(0f);
        }
    }
    private void UpdateA(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {

            equationController.UpdateA(floatValue);
            lineRendererController.SetA(floatValue);
        }
        
    }
    private void UpdateB(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            equationController.UpdateB(floatValue);
            lineRendererController.SetB(floatValue);
        }
    }
    private void UpdateH(string newValue)
    {

        if(float.TryParse(newValue, out float floatValue))
        {
            equationController.UpdateH(floatValue);
            draggableLine.SetX(floatValue);
        }
        
    }
    private void UpdateK(string newValue)
    {
        if(float.TryParse(newValue, out float floatValue))
        {
            equationController.UpdateH(floatValue);
            draggableLine.SetY(floatValue);
        }
    }

    private void SetVariablesToDefault()
    {
        UpdateA("1");
        UpdateB("1");
        UpdateH("0");
        UpdateK("0");
        inputA.text = "";
        inputB.text = "";
        inputH.text = "";
        inputK.text = "";
    }

    public void UpdateAText(float newValue)
    {
        inputA.text = newValue.ToString();
        valueA = newValue;
    }

    public void UpdateBText(float newValue)
    {
        inputB.text = newValue.ToString();
        valueB = newValue;
    }

    public void UpdateHText(float newValue)
    {
        inputH.text = newValue.ToString();
        valueH = newValue;
    }

    public void UpdateKText(float newValue)
    {
        inputK.text = newValue.ToString();
        valueK = newValue;
    }


    public void ConicSelectorPressed(bool onlyA)
    {
        this.onlyA = onlyA;
        ChangeInteractableInputs(onlyA);
        ChangeInteractableSubmitButton(true);
        ChangeDefaultValues(onlyA);
    }
    
    public void ChangeDefaultValues(bool onlyA)
    {
        valueA = defaultA;
        valueB = defaultB;
        valueH = defaultH;
        valueK = defaultK;

        inputA.text = defaultA.ToString();
        inputH.text = defaultH.ToString();
        inputK.text = defaultK.ToString();

        if(!onlyA)
        {
            inputB.text = defaultB.ToString();
        }
    }
    
    public void ChangeInteractableInputs(bool onlyA)
    {
        //Default
        
        inputA.interactable = true;
        inputH.interactable = true;
        inputK.interactable = true;
        inputB.interactable = !onlyA;
    }

    public void ChangeInteractableButtonsSpecial(bool A, bool B , bool H, bool K)
    {
        inputA.interactable = A;
        inputB.interactable = B;
        inputH.interactable = H;
        inputK.interactable = K;
    }

    public void ChangeInteractableButtons(bool A, bool B , bool C, bool D)
    {
        circleButton.interactable = A;
        parabolaButton.interactable = B;
        ellipseButton.interactable = C;
        hyperbolaButton.interactable = D;
        Debug.Log("ActivateButtons");
    }

    public void DisableAll()
    {
        inputA.interactable = false;
        inputB.interactable = false;
        inputH.interactable = false;
        inputK.interactable = false;

        circleButton.interactable = false;
        parabolaButton.interactable = false;
        ellipseButton.interactable = false;
        hyperbolaButton.interactable = false;

        Debug.Log("Deactivate");
        ChangeInteractableSubmitButton(false);
        SetVariablesToDefault();
    }
    public void ChangeInteractableSubmitButton(bool interactable)
    {
        submitButton.interactable = interactable;
    }

    private void ToggleInteractableForSatellite()
    {

        if(!toggleList.Contains(true))
        {
            toggleList[0] = inputA.interactable;
            toggleList[1] = inputB.interactable;
            toggleList[2] = inputH.interactable;
            toggleList[3] = inputK.interactable;

            toggleList[4] = circleButton.interactable;
            toggleList[5] = parabolaButton.interactable;
            toggleList[6] = ellipseButton.interactable;
            toggleList[7] = hyperbolaButton.interactable;

            inputA.interactable = false;
            inputH.interactable = false;
            inputK.interactable = false;
            inputB.interactable = false;

            circleButton.interactable = false;
            parabolaButton.interactable = false;
            ellipseButton.interactable = false;
            hyperbolaButton.interactable = false;
        }
        else
        {
            inputA.interactable = toggleList[0];
            inputB.interactable = toggleList[1];
            inputH.interactable = toggleList[2];
            inputK.interactable = toggleList[3];

            circleButton.interactable = toggleList[4];
            parabolaButton.interactable = toggleList[5];
            ellipseButton.interactable = toggleList[6];
            hyperbolaButton.interactable = toggleList[7];

            toggleList[0] = false;
            toggleList[1] = false;
            toggleList[2] = false;
            toggleList[3] = false;

            toggleList[4] = false;
            toggleList[5] = false;
            toggleList[6] = false;
            toggleList[7] = false;
        }
        
    }

}
