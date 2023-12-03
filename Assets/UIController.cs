using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject introWindow;
    [SerializeField] private TextMeshProUGUI windowText;
    [SerializeField] private PointsController pointsController;

#region EVENT_LISTENERS


        private void OnEnable()
        {
            EventManager.StartListening("EndingGame", ShowWindow);


        }

            private void OnDisable()
        {
            EventManager.StopListening("EndingGame", ShowWindow);


        } 

#endregion
    void Start()
    {
        continueButton.onClick.AddListener(CloseWindow);
    }

    private void  CloseWindow()
    {   
        introWindow.SetActive(false);
        EventManager.TriggerEvent("StartGame");
    }

    private void ShowWindow()
    {
        introWindow.SetActive(true);
        windowText.text = "Congratulations on completing your mission with "+ pointsController.points +" points!!\n\nYou may continue your duties or take a well earned break!";
    }


}
