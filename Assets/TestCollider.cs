using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollider : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger Entered by " + other);
    }
}
