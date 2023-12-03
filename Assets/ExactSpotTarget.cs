using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExactSpotTarget : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
     private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.name == "SatelliteCollider")
        {
            //Grant points
        }  
    }


}
