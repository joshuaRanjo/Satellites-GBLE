using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCollider : MonoBehaviour
{
    // Start is called before the first frame update
    private SatelliteController satelliteController;
    void Start()
    {
        satelliteController = GameObject.FindWithTag("SatelliteController").GetComponent<SatelliteController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        

    }


}
