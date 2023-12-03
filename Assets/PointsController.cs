using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{
    
    public int points  {get ;private set;}

    [SerializeField] private TextMeshProUGUI pointsText;

#region EVENT_LISTENERS


        private void OnEnable()
        {
            EventManager.StartListening("StartGame", StartGame);

            //Other Event Manager
            GameEvents.current.pointGained += AddPoints;

        }

            private void OnDisable()
        {
            EventManager.StopListening("StartGame", StartGame);

            GameEvents.current.pointGained -= AddPoints;

        } 

#endregion
    void Start()
    {
        points = 0;
    }
    void StartGame()
    {
        points = 0;
        pointsText.text = "0";
    }

    private void AddPoints(int addedPoints)
    {
        points  += addedPoints;
        pointsText.text = "" +points;
    }
}
