using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private SpriteRenderer sprite; 
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.name == "SatelliteCollider")
        {
            
            GameEvents.current.PointGained(500);
            sprite.enabled = false;
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
