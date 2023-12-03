using UnityEngine;
using UnityEngine.UI;
public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Button submitButton;

    private bool isDragging = false;
    private bool draggedLerp = false;
    private Vector3 offset;
    private Vector3 targetPosition;

    void OnMouseDown()
    {
        isDragging = true;
        submitButton.interactable = false;
        offset = transform.localPosition - transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    void OnMouseUp()
    {

        isDragging = false;
        draggedLerp = true;

        // Calculate the target position by rounding to the nearest whole number
        targetPosition = new Vector3(Mathf.Round(transform.localPosition.x), Mathf.Round(transform.localPosition.y), transform.localPosition.z);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 newPosition = transform.parent.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) + offset;
            transform.localPosition = new Vector3(newPosition.x, newPosition.y, transform.localPosition.z);
        }
        else
        {
            // Smoothly interpolate to the target position only if it was moved by a drag and drop action
            if(draggedLerp)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * 17f); // Adjust the speed as needed
                if(transform.localPosition == targetPosition)
                {
                    draggedLerp = false;
                    submitButton.interactable = true;
                    transform.localPosition = targetPosition;
                }
            }
            
        }
    }
}
