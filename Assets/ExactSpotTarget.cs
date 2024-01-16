using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExactSpotTarget : MonoBehaviour
{


    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.name == "ExactSpotSatellite")
        {
            //GameEvents.current.PointGained(250);
            //Debug.Log("exact hit");
        }  
    }

    private void OnTriggerExit2D(Collider2D other) 
    {

    }
}
