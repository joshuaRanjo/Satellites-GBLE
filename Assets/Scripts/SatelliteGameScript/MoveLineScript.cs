using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLineScript : MonoBehaviour
{

    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;
#region EVENT_LISTENERS
    private void OnEnable()
    {
        lineDataScriptableObject.dataChangeEvent.AddListener(ChangePosition);
    }

    private void OnDisable()
    {
        lineDataScriptableObject.dataChangeEvent.RemoveListener(ChangePosition);
    }
#endregion

    private void ChangePosition()
    {
        float newXPosition = lineDataScriptableObject.h;
        float newYPosition = lineDataScriptableObject.k;
        Vector3 currentPosition = transform.localPosition;

        currentPosition.x = newXPosition;
        currentPosition.y = newYPosition;

        transform.localPosition = currentPosition;
    }
    /*
    public void SetX(float xPosition)
    {
            float newXPosition = xPosition;
            Vector3 currentPosition = transform.localPosition;

            currentPosition.x = newXPosition;

            transform.localPosition = currentPosition;
        
    }
    public void SetY(float yPosition)
    {
            float newYPosition = yPosition;
            Vector3 currentPosition = transform.localPosition;

            currentPosition.y = newYPosition;

            transform.localPosition = currentPosition;
    }   
    */
    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0f,0f,0f);
    }

}
