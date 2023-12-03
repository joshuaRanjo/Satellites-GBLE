using UnityEngine;

public class SetBounds : MonoBehaviour
{
    private void Awake() 
    {
        var bounds = GetComponent<Collider2D>().bounds;
        Globals.WorldBounds = bounds;
    }
}
