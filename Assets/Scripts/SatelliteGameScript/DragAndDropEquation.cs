using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class DragAndDropEquation : MonoBehaviour
{
    [SerializeField] private Button submitButton;

    [Header("Allow Dragged Lerp")]
    [SerializeField] private bool draggedLerpAllowed = true;

    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;
/*
    [Header("Equation Controller")]
    [SerializeField] private EquationController equationController;
    
    [Header("Input Controller")]
    [SerializeField] private InputController inputController;
*/
    [Header("Boundaries")]
    [SerializeField] private float maxDragX = 10;
    [SerializeField] private float maxDragY = 10;



    private bool isDragging = false;
    private bool draggedLerp = false;
    private Vector3 offset;
    private Vector3 targetPosition;
    private bool draggingAllowed = true;

#region EVENT_LISTENERS

    private UnityAction allowDrag;
    private UnityAction noAllowDrag;
    
    private void Awake()
    {
        allowDrag = new UnityAction(AllowDrag);
        noAllowDrag = new UnityAction(NoAllowDrag);
    }

    private void OnEnable()
    {
        EventManager.StartListening("SatelliteMoving", NoAllowDrag);
        EventManager.StartListening("SatelliteStopped", AllowDrag);

    }

        private void OnDisable()
    {
        EventManager.StopListening("SatelliteMoving", NoAllowDrag);
        EventManager.StopListening("SatelliteStopped", AllowDrag);
    }

#endregion

    private void AllowDrag()
    {
        draggingAllowed = true;
    }

    private void NoAllowDrag()
    {
        draggingAllowed = false;
    }

    void OnMouseDown()
    {
        isDragging = true;
        submitButton.interactable = false;
        offset = transform.localPosition - transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        EventManager.TriggerEvent("DraggingVertex");
        //Debug.Log("DraggingVertex");
    }

    void OnMouseUp()
    {

        isDragging = false;
        submitButton.interactable = true;
        draggedLerp = true;
        EventManager.TriggerEvent("StoppedDraggingVertex");

        // Calculate the target position by rounding to the nearest whole number
        targetPosition = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), transform.localPosition.z);
    }

    void Update()
    {
        if (isDragging && draggingAllowed)
        {
            Vector3 newPosition = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) + offset;

            float newX = Mathf.Clamp(newPosition.x, -maxDragX, maxDragX);
            float newY = Mathf.Clamp(newPosition.y, -maxDragY, maxDragY);

            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);

            float xPos = Mathf.Round(transform.localPosition.x * Mathf.Pow(10,2)) / Mathf.Pow(10,2);
            float yPos = Mathf.Round(transform.localPosition.y * Mathf.Pow(10,2)) / Mathf.Pow(10,2);

            // convert to scriptable object
            
            //equationController.UpdateH(xPos);
            //equationController.UpdateK(yPos);

            //inputController.UpdateHText(xPos);
            //inputController.UpdateKText(yPos);

            //Scriptable object 
            lineDataScriptableObject.SetH(xPos, "drag");
            lineDataScriptableObject.SetK(yPos, "drag");
            
        }
        else
        {
            // Smoothly interpolate to the target position only if it was moved by a drag and drop action
            
            if(draggedLerp  && draggedLerpAllowed)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 17f); // Adjust the speed as needed
                if(transform.localPosition == targetPosition)
                {
                    draggedLerp = false;
                    submitButton.interactable = true;
                    transform.localPosition = targetPosition;
                }

                float xPos = Mathf.Floor(transform.localPosition.x * Mathf.Pow(10,2)) / Mathf.Pow(10,2);
                float yPos = Mathf.Floor(transform.localPosition.y * Mathf.Pow(10,2)) / Mathf.Pow(10,2);
                

                // convert to scriptable object
                ///equationController.UpdateH(xPos);
                //equationController.UpdateK(yPos);

                //inputController.UpdateHText(xPos);
                //inputController.UpdateKText(yPos);

                //Scriptable object 
                lineDataScriptableObject.SetH(xPos, "drag");
                lineDataScriptableObject.SetK(yPos, "drag");
            }
            
            
        }
        

    }
}
