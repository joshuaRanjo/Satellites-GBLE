using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.EventSystems;

public class CameraDrag : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;

    private Camera _mainCamera;

    private bool _isDragging;

    private Bounds _cameraBrounds;
    private Vector3 _targetPosition;
    
    private float height;

    private bool satelliteDraggingAllowed = true;
    private bool movingCameraDraggingAllowed = true;

#region EVENT_LISTENERS


    private void OnEnable()
    {
        EventManager.StartListening("SatelliteMoving", SatelliteNoAllowDrag);
        EventManager.StartListening("SatelliteStopped",SatelliteAllowDrag);
        EventManager.StartListening("CameraMoving",CameraNoAllowDrag);
        EventManager.StartListening("CameraStoppedMoving",CameraAllowDrag);
    }

        private void OnDisable()
    {
        EventManager.StopListening("SatelliteMoving", SatelliteNoAllowDrag);
        EventManager.StopListening("SatelliteStopped", SatelliteAllowDrag);
        EventManager.StopListening("CameraMoving",CameraNoAllowDrag);
        EventManager.StopListening("CameraStoppedMoving",CameraAllowDrag);
    }

#endregion

    private void SatelliteAllowDrag()       {satelliteDraggingAllowed = true;}
    private void SatelliteNoAllowDrag()     {satelliteDraggingAllowed = false;}
    private void CameraAllowDrag()          {movingCameraDraggingAllowed = true;}
    private void CameraNoAllowDrag()        {movingCameraDraggingAllowed = false;}

    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    private void Start()
    {
        SetUpCameraVariables();
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

    public void OnDrag(InputAction.CallbackContext ctx)
    {
        
        if(!CheckIfDraggableClicked() && !CheckIfUIClicked() && satelliteDraggingAllowed && movingCameraDraggingAllowed)
        {
            if(ctx.started) _origin = GetMousePosition;
            _isDragging = ctx.started || ctx.performed;
        }
    }

    private void LateUpdate()
    {
        if(!_isDragging) return;

        _difference = GetMousePosition - transform.position;

        _targetPosition = _origin - _difference;
        _targetPosition = GetCameraBounds();

        transform.position = _targetPosition;

        if(height != _mainCamera.orthographicSize)
            SetUpCameraVariables();
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBrounds.min.x, _cameraBrounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBrounds.min.y, _cameraBrounds.max.y),
            transform.position.z
        );
    }

    private bool CheckIfDraggableClicked()
    {
        bool draggable = false;

        RaycastHit2D hit  = Physics2D.Raycast(GetMousePosition, Vector2.zero);

        if(hit.collider != null )
        {
            GameObject clickedObj = hit.collider.gameObject;
            if(clickedObj.CompareTag("Draggable"))
            {
                draggable = true;
            }
        }
        
        return draggable;
    }

    private bool CheckIfUIClicked()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
    

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
