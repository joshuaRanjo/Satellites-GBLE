using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestInputs : MonoBehaviour
{

    public TMP_InputField input1;
    public TMP_InputField input2;
    public TMP_InputField input3;
    // Start is called before the first frame update

    public void Test()
    {
        Debug.Log(input1.text);
        Debug.Log(input2.text);
        Debug.Log(input3.text);
    }

}
