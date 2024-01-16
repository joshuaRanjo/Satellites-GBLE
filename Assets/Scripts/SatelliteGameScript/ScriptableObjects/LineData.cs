using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Line Data", menuName = "LineData")]
public class LineData : ScriptableObject
{
    public float a;
    public float b;
    public float h;
    public float k;

    public string type;
    public string orientation;
    public string changeType;

    public bool hasSpecifications = false;

    [System.NonSerialized]
    public UnityEvent dataChangeEvent = new UnityEvent();

    private void OnEnable()
    {
        // Initialize Values
        ResetValues();


    }

    
    public void SetA(float newA, string changeType){a = newA;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetB(float newB, string changeType){b = newB;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetH(float newH, string changeType){h = newH;  this.changeType = changeType; dataChangeEvent.Invoke();}
    public void SetK(float newK, string changeType){k = newK;  this.changeType = changeType; dataChangeEvent.Invoke();}

    public void SetA(float newA){a = newA;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetB(float newB){b = newB;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetH(float newH){h = newH;  this.changeType = "none"; dataChangeEvent.Invoke();}
    public void SetK(float newK){k = newK;  this.changeType = "none"; dataChangeEvent.Invoke();}

    public void SetType(string newType){ type = newType; ResetValues();  dataChangeEvent.Invoke();}
    public void SetOrientation(string newOrientation){ orientation = newOrientation;  dataChangeEvent.Invoke();}

    public void SetType(){
        Debug.Log("hi");
    }

    public void ResetValues()
    {
        a = 1f; b = 1f; h = 0f; k = 0f;  changeType = "none";
        if(type == "Parabola")
        {
            orientation = "Vertical";
        }
        else
        {
            orientation = "Horizontal";
        }
        if(hasSpecifications)
        {
            // Set values to specified 
        }
    }



}
