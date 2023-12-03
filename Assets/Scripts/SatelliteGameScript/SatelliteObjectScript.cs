using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteObjectScript : MonoBehaviour
{

    // list of collided targets

    //
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            // Stop follow coroutine, play broken anim,  reset satellite position,
            // re activate collided target objects 
        }

        if (collision.collider.CompareTag("Target"))
        {
            // deactivate target, 
        }    
    }

}
