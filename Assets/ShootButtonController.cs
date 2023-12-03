using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootButtonController : MonoBehaviour
{
        public Button followButton;
        public FollowPath4 followPathScript;

        private void Start()
        {
            followButton.onClick.AddListener(StartFollowingPath);
        }

        private void StartFollowingPath()
        {
            followButton.interactable = false;
            followPathScript.ResetPath();
            StartCoroutine(followPathScript.FollowPathCoroutine(OnPathFollowComplete));
        }

        private void OnPathFollowComplete()
        {
            // Code to be executed after the path following is finished
            Debug.Log("Path following is complete!");
            followButton.interactable = true;
        }
}
