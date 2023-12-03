using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Button Controller for satellite game
public class SatelliteController : MonoBehaviour
{
        public Button submitButton;
        public FollowPath5 followPathScript;
        [SerializeField] private GameObject satelliteObject;
        [SerializeField] private GameObject satellitePrefab;
        [SerializeField] private Transform draggableLineTransform;

        private GameObject lineRendererObject;

        
        private Coroutine followPathCoroutine;

#region EVENT_LISTENERS

        private void OnEnable()
        {
            EventManager.StartListening("GamePaused", StopFollowingPath);

        }

            private void OnDisable()
        {
            EventManager.StopListening("GamePaused", StopFollowingPath);

        } 

#endregion

        private void Start()
        {
            submitButton.onClick.AddListener(StartFollowingPath);

        }

        private void StartFollowingPath()
        {
            //submitButton.interactable = false;
            EventManager.TriggerEvent("SatelliteMoving");
            //satelliteObject.SetActive(true);
            satelliteObject = Instantiate(satellitePrefab,draggableLineTransform);
            followPathScript = satelliteObject.GetComponent<FollowPath5>();
            
            followPathScript.SetLineRenderer(lineRendererObject);

            followPathScript.StartFollowingPath();
            followPathCoroutine = StartCoroutine(followPathScript.FollowPathCoroutine(OnPathFollowComplete));

            //
            submitButton.onClick.AddListener(StopFollowingPath);
            submitButton.onClick.RemoveListener(StartFollowingPath);
        }

        private void StopFollowingPath()
        {
            if(satelliteObject != null)
            {
                if(followPathCoroutine != null)
                {
                    followPathScript.StopFollowingPath();
                    StopCoroutine(followPathCoroutine);
                }

                Destroy(satelliteObject);
                submitButton.onClick.RemoveListener(StopFollowingPath);
                submitButton.onClick.AddListener(StartFollowingPath);
                
                EventManager.TriggerEvent("SatelliteStopped");

                EventManager.TriggerEvent("ResetQuestion");
            }
            
        }

        private void OnPathFollowComplete()
        {
            // Code to be executed after the path following is finished
            Debug.Log("Path following is complete!");

            Destroy(satelliteObject);

            submitButton.onClick.RemoveListener(StopFollowingPath);
            submitButton.onClick.AddListener(StartFollowingPath);
            
                       
            submitButton.interactable = true;

            EventManager.TriggerEvent("SatelliteStopped");

            //Check if question complete
            EventManager.TriggerEvent("CheckQuestion"); // Temp just reset the question

            // maybe start the Coroutine here to loop

            // trigger game to continue

            //Next Question
        }
        public void SetLineRenderer(GameObject line)
        {
            lineRendererObject = line;
        }
}
