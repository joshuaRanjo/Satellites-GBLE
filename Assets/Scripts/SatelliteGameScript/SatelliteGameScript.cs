using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TexDrawLib;
using UnityEngine.UI;
using UnityEngine.Events;

public class SatelliteGameScript : MonoBehaviour
{
    int difficulty = 0;
    [Header("Controller")]
    [SerializeField] private InputController    inputController;
    [SerializeField] private QuestionController questionController;


    [Header("Conic Selectors")]
    [SerializeField] private Button circleButton;
    [SerializeField] private Button parabolaButton;
    [SerializeField] private Button ellipseButton;
    [SerializeField] private Button hyperbolaButton;

    [Header("Submit Button")]
    [SerializeField] private Button submitButton;

    [SerializeField] private int maxQuestions = 5;


#region EVENT_LISTENERS


        private void OnEnable()
        {
            EventManager.StartListening("StartGame", StartGame);
            EventManager.StartListening("QuestionCompleted", SetUpQuestion);

            //Other Event Manager
            GameEvents.current.pointGained += AddPoints;

        }

            private void OnDisable()
        {
            EventManager.StopListening("StartGame", StartGame);
            EventManager.StopListening("QuestionCompleted", SetUpQuestion);
            GameEvents.current.pointGained -= AddPoints;

        } 

#endregion

     
    private int points = 0;
    private int questionNumber = 1;
    private int questionType;
    private void Start() 
    {
        points = 0;
        questionNumber = 0;
        // Intro stuff

        // Initiate stuff
        //SetUpQuestion();
    }

    private void StartGame()
    {
        points = 0;
        questionNumber = 0;
        SetUpQuestion();
    }

    private void SetUpQuestion()
    {

        Debug.Log("Current Points = " + points);
        

        questionNumber += 1;

        if(questionNumber <= maxQuestions)
        {
            EventManager.TriggerEvent("NewQuestion");

            // Spawn target objects
            questionController.SetUpNewQuestion();


            //  Activate "Pick conic buttons"
            circleButton.interactable = true;
            parabolaButton.interactable = true;
            
            //ellipseButton.interactable = true;
            //hyperbolaButton.interactable = true;
        }
        else
        {
            EndGame();
        }
    }

    private void AddPoints(int addedPoints)
    {
        points += addedPoints;
    }
    
    private void EndGame()
    {
        Debug.Log("GameCompleted");
        EventManager.TriggerEvent("EndingGame");
    }


}
