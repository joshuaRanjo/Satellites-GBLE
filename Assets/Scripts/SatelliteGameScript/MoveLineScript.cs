using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLineScript : MonoBehaviour
{
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

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0f,0f,0f);
    }

}
