using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private SpriteRenderer sprite; 

    [Header("Target Transform")]
    [SerializeField] private Transform targetTransform;
    [Header("Line Data")]
    [SerializeField] private LineData lineDataScriptableObject;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.name == "SatelliteCollider")
        {
            
            GameEvents.current.PointGained(500);
            sprite.enabled = false;

            float a = lineDataScriptableObject.a;
            float b = lineDataScriptableObject.b;
            float h = lineDataScriptableObject.h;
            float k = lineDataScriptableObject.k;
            float x = targetTransform.transform.localPosition.x;
            float y = targetTransform.transform.localPosition.y;
            float xh = x-h;
            float yk = y-k;

            bool grantExactBonus = false;
            if (lineDataScriptableObject.type == "Parabola")
            {
                if(lineDataScriptableObject.orientation == "Vertical")
                {
                    if( xh*xh == 4*a*yk)
                    {
                        grantExactBonus = true;
                    }
                }
                else{
                    if( yk*yk == 4*a*xh)
                    {
                        grantExactBonus = true;
                    }
                }
            }
            else if (lineDataScriptableObject.type == "Circle")
            {
                if( ( (xh*xh) + (yk*yk)) == (a*a) )
                {
                    grantExactBonus = true;
                }
            }
            else if (lineDataScriptableObject.type == "Ellipse")
            {
                if( ( ( ((xh*xh)/ (a*a)) + ( (yk*yk) / (b*b)) ) == 1 ))
                {
                    grantExactBonus = true;
                }
            }
            else if (lineDataScriptableObject.type == "Hyperbola")
            {
                if(lineDataScriptableObject.orientation == "Horizontal")
                {
                    if( ( ( ( (xh*xh) / (a*a) ) - ( (yk*yk) / (b*b)) ) == 1 ))
                    {
                        grantExactBonus = true;
                    }
                }
                else
                {
                    if( ( ( ( (yk*yk) / (a*a) ) - ( (xh*xh) / (b*b)) ) == 1 ))
                    {
                        grantExactBonus = true;
                    }
                }
            }
            else
            {
                Debug.Log("Target Script: No Valid LineType set");
            }

            if(grantExactBonus)
            {
                GameEvents.current.PointGained(250);
                Debug.Log("Exact Bonus Granted");
            }
        }
          
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.name == "SatelliteCollider")
        {
            sprite.enabled = true;
            targetObject.SetActive(false);
            
        }  
    }
}
