using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake() {
        current = this;
    }

    public event Action<int> pointGained;

    public void PointGained(int numberPoints)
    {
        
        if(pointGained != null)
        {
            pointGained(numberPoints);
        }
    }



}
