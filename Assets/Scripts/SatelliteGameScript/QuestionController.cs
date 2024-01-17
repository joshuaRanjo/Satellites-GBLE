using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TexDrawLib;
using UnityEngine.Events;

public class QuestionController : MonoBehaviour
{
    private float maxXvaluef = 10f;
    private float minXvaluef = -10f;
    private float maxYvaluef = 10f;
    private float minYvaluef = -10f;

    private int maxXvalue = 10;
    private int minXvalue = -10;
    private int maxYvalue = 10;
    private int minYvalue = -10;
    [Header("Question Box")]
    [SerializeField] private TEXDraw questionText;

    [Header("LocalSpace")]
    [SerializeField] private Transform localSpace;

    [Header("Prefabs")]
    //Prefabs
    [SerializeField] public GameObject vertexTarget;

    [SerializeField] private InputController inputController;

    private List<GameObject> obstaclePrefabs = new List<GameObject>();

    private List<GameObject> targetPrefabs = new List<GameObject>();

#region EVENT_LISTENERS

    private UnityAction resetQuestion;
    private UnityAction checkQuestion;

    private void Awake()
    {
        resetQuestion = new UnityAction(ResetQuestion);
        checkQuestion = new UnityAction(CheckQuestion);

    }

    private void OnEnable()
    {
        EventManager.StartListening("ResetQuestion", resetQuestion);
        EventManager.StartListening("CheckQuestion", checkQuestion);
        EventManager.StartListening("EndingGame", EndingGame);

    }

    private void OnDisable()
    {
        EventManager.StopListening("ResetQuestion", resetQuestion);
        EventManager.StopListening("CheckQuestion", checkQuestion);
        EventManager.StopListening("EndingGame", EndingGame);
    } 

#endregion

    public void SetUpNewQuestion()
    {
        //Destroy all objects im lists
        foreach(GameObject obj in obstaclePrefabs)
        {
            if(obj != null)
            {
                Destroy(obj);
            }
        }
        foreach(GameObject obj in targetPrefabs)
        {
            if(obj != null)
            {
                Destroy(obj);
            }
        }

        //Clear lists
        obstaclePrefabs.Clear();
        targetPrefabs.Clear();

        // Decide Question type

        Parabola1();
    }

    private void ResetQuestion()
    {
        // iterate through all targets and activate them
        foreach(GameObject obj in targetPrefabs)
        {
            if(obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    private void CheckQuestion()
    {
        // iterate through all targets and activate them

        bool active = false;
        foreach(GameObject obj in targetPrefabs)
        {
            if(obj != null)
            {
                if(obj.activeSelf)
                {
                    active = true;
                    break; // Exit if atleast 1 object is still active
                }
            }
        }

        if(active)
        {
            ResetQuestion();
        }
        else
        {
            Debug.Log("Next Question");
            // Event NewQuestion
            EventManager.TriggerEvent("QuestionCompleted");
        }


    }

    private void EndingGame()
    {
        questionText.text = "";
    }

    // Set UP functions for different question types

    //Basic Parabola, get object only at vertex spawns randomly
    void Parabola1()
    {
    
        Vector3 newTransform = localSpace.localPosition;

        newTransform.x = Random.Range(minXvalue, maxXvalue);
        newTransform.y = Random.Range(minYvalue, maxYvalue);

        //newTransform.x = 1f;
        //newTransform.y = 1f;

        //Change Question Text
        questionText.text = "Observation point is located at \n(" +newTransform.x +","+newTransform.y+")"; 

        GameObject newTarget = Instantiate(vertexTarget, localSpace);
        targetPrefabs.Add(newTarget);
        newTarget.transform.localPosition = newTransform;
        inputController.ChangeInteractableButtons(true,true,true,true);
    }
}
