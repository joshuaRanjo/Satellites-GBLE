using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VertexCircleController : MonoBehaviour
{
    [SerializeField] private GameObject vertex;

#region EVENT_LISTENERS

    private UnityAction showVertex;
    private UnityAction hideVertex;
    
    private void Awake()
    {
        showVertex = new UnityAction(ShowVertex);
        hideVertex = new UnityAction(HideVertex);
    }
    private void OnEnable()
    {
        EventManager.StartListening("SatelliteMoving", hideVertex);
        EventManager.StartListening("SatelliteStopped", showVertex);
    }

        private void OnDisable()
    {
        EventManager.StopListening("SatelliteMoving", hideVertex);
        EventManager.StopListening("SatelliteStopped", showVertex);
    }

#endregion

    private void ShowVertex()
    {
        vertex.SetActive(true);
    }
    
    private void HideVertex()
    {
        vertex.SetActive(false);
    }
}
