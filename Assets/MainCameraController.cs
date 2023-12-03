using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MainCameraController : MonoBehaviour
{
    [SerializeField] private GameObject gridSpaceObject;

    private Vector3 targetPosition = new Vector3(0,0,-10);
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private GameObject draggableLine;


    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBrounds;
    private Vector3 _targetPosition;
    
    private float height;

#region EVENT_LISTENERS


    private void OnEnable()
    {
        EventManager.StartListening("NewQuestion", ResetCameraPosition);
        EventManager.StartListening("EndingGame", ResetCameraPosition);
        //EventManager.StartListening("SatelliteMoving", MoveCameraToPoint);


    }

        private void OnDisable()
    {
        EventManager.StopListening("NewQuestion", ResetCameraPosition);
        EventManager.StopListening("EndingGame", ResetCameraPosition);
        //EventManager.StopListening("SatelliteMoving", MoveCameraToPoint);

    } 

#endregion
    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    public void ResetCameraPosition()
    {
        targetPosition = new Vector3(0,0,-10);
        duration = 1.5f;
        StartCoroutine(MoveCamera());
    }

    private void SetUpCameraVariables()
    {
        height = _mainCamera.orthographicSize;
        var width = height * (1420/1080);

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        
        _cameraBrounds = new Bounds();
        _cameraBrounds.SetMinMax(
                new Vector3(minX,minY,0.0f),
                new Vector3(maxX,maxY,0.0f)
            );
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBrounds.min.x, _cameraBrounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBrounds.min.y, _cameraBrounds.max.y),
            transform.position.z
        );
    }

    private void MoveCameraToPoint()
    {
        if(height != _mainCamera.orthographicSize)
            SetUpCameraVariables();

        _targetPosition = draggableLine.transform.position;

        targetPosition = GetCameraBounds();
        targetPosition.x -= 3.25f;

        duration = 0.5f;
        StartCoroutine(MoveCamera());

    }

    
    IEnumerator MoveCamera()
    {
        EventManager.TriggerEvent("CameraMoving");
        // Get the initial position of the camera
        Vector3 startPosition = transform.position;

        // Time elapsed since the start of the movement
        float elapsedTime = 0f;
        float percentageComplete = 0f;

        // Loop until the elapsed time reaches the moveTime
        while (percentageComplete < 1f)
        {
             elapsedTime += Time.deltaTime;
             percentageComplete = elapsedTime / duration;

            // Use Lerp to interpolate between the start and target positions
            transform.position = Vector3.Lerp(startPosition, targetPosition, curve.Evaluate(percentageComplete));

            // Wait for the next frame
            yield return null;
        }

        // Ensure the camera reaches the exact target position
        transform.position = targetPosition;
        EventManager.TriggerEvent("CameraStoppedMoving");
    }

    
}
